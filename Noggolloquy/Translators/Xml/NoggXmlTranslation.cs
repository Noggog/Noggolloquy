﻿using Noggog;
using Noggog.Notifying;
using Noggog.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Noggolloquy.Xml
{
    public delegate void NoggXmlCopyInFunction(XElement root, object item, NotifyingFireParameters? cmds, bool doMasks, out object mask);
    public delegate void NoggXmlWriteFunction(XmlWriter writer, string name, object item, bool doMasks, out object mask);

    public class NoggXmlTranslation<T, M> : IXmlTranslation<T>
        where T : INoggolloquyObjectGetter
        where M : IErrorMask, new()
    {
        public readonly static NoggXmlTranslation<T, M> Instance = new NoggXmlTranslation<T, M>();
        private static readonly string _elementName = NoggolloquyRegistration.GetRegister(typeof(T)).FullName;
        public string ElementName => _elementName;
        private static Dictionary<Type, NoggXmlCopyInFunction> _readerDict = new Dictionary<Type, NoggXmlCopyInFunction>();
        private static Dictionary<Type, NoggXmlWriteFunction> _writerDict = new Dictionary<Type, NoggXmlWriteFunction>();
        
        public void CopyIn<C>(
            XElement root,
            C item,
            bool skipReadonly,
            bool doMasks,
            out M mask,
            NotifyingFireParameters? cmds)
            where C : T, INoggolloquyObjectSetter
        {
            mask = default(M);
            HashSet<ushort> readIndices = new HashSet<ushort>();

            try
            {
                foreach (var elem in root.Elements())
                {
                    if (!elem.TryGetAttribute("name", out XAttribute name))
                    {
                        if (doMasks)
                        {
                            if (mask == null)
                            {
                                mask = new M();
                            }
                            mask.Warnings.Add("Skipping field that did not have name");
                        }
                        continue;
                    }

                    var i = item.Registration.GetNameIndex(name.Value);
                    if (!i.HasValue)
                    {
                        if (doMasks)
                        {
                            if (mask == null)
                            {
                                mask = new M();
                            }
                            mask.Warnings.Add("Skipping field that did not exist anymore with name: " + name);
                        }
                        continue;
                    }

                    var readOnly = item.Registration.IsReadOnly(i.Value);
                    if (readOnly && skipReadonly) continue;

                    try
                    {
                        var type = item.GetNthType(i.Value);
                        if (!XmlTranslator.TryGetTranslator(type, out IXmlTranslation<object> translator))
                        {
                            throw new ArgumentException($"No XML Translator found for {type}");
                        }
                        var objGet = translator.Parse(elem, doMasks, out var subMaskObj);
                        if (objGet.Succeeded)
                        {
                            item.SetNthObject(i.Value, objGet.Value, cmds);
                            readIndices.Add(i.Value);
                        }
                        if (doMasks && subMaskObj != null)
                        {
                            if (mask == null)
                            {
                                mask = new M();
                            }
                            mask.SetNthMask(i.Value, subMaskObj);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (doMasks)
                        {
                            if (mask == null)
                            {
                                mask = new M();
                            }
                            mask.SetNthException(i.Value, ex);
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                for (ushort i = 0; i < item.Registration.FieldCount; i++)
                {
                    if (item.Registration.IsNthDerivative(i)) continue;
                    if (!readIndices.Contains(i))
                    {
                        item.UnsetNthObject(i, cmds.ToUnsetParams());
                    }
                }
            }
            catch (Exception ex)
            {
                if (doMasks)
                {
                    if (mask == null)
                    {
                        mask = new M();
                    }
                    mask.Overall = ex;
                }
                else
                {
                    throw;
                }
            }
        }

        public TryGet<T> Parse(XElement root, bool doMasks, out object maskObj)
        {
            throw new NotImplementedException();
            //foreach (var elem in root)
            //{

            //}
        }

        public bool Write(XmlWriter writer, string name, T item, bool doMasks, out M mask)
        {
            using (new ElementWrapper(writer, item.Registration.Name))
            {
                if (!string.IsNullOrEmpty(name))
                {
                    writer.WriteAttributeString("name", name);
                }
                mask = default(M);

                try
                {
                    for (ushort i = 0; i < item.Registration.FieldCount; i++)
                    {
                        try
                        {
                            if (!item.GetNthObjectHasBeenSet(i)) continue;

                            var type = item.GetNthType(i);
                            object subMaskObj;
                            if (item.Registration.GetNthIsNoggolloquy(i))
                            {
                                if (TryGetWriteFunction(type, out NoggXmlWriteFunction writeFunc))
                                {
                                    writeFunc(writer, item.Registration.GetNthName(i), item.GetNthObject(i), doMasks, out subMaskObj);
                                }
                                else
                                {
                                    throw new ArgumentException($"No XML Translator found for {type}");
                                }
                            }
                            else
                            {
                                if (!XmlTranslator.TryGetTranslator(type, out IXmlTranslation<object> translator))
                                {
                                    throw new ArgumentException($"No XML Translator found for {type}");
                                }
                                translator.Write(writer, item.Registration.GetNthName(i), item.GetNthObject(i), doMasks, out subMaskObj);
                            }

                            if (subMaskObj != null)
                            {
                                if (mask == null)
                                {
                                    mask = new M();
                                }
                                mask.SetNthMask(i, subMaskObj);
                            }
                        }
                        catch (Exception ex)
                        {
                            if (doMasks)
                            {
                                if (mask == null)
                                {
                                    mask = new M();
                                }
                                mask.SetNthException(i, ex);
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (doMasks)
                    {
                        if (mask == null)
                        {
                            mask = new M();
                        }
                        mask.Overall = ex;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return mask == null;
        }

        private bool TryGetWriteFunction(Type t, out NoggXmlWriteFunction writeFunc)
        {
            if (_writerDict.TryGetValue(t, out writeFunc))
            {
                return writeFunc != null;
            }
            throw new NotImplementedException();
        }

        private bool TryGetCopyInFunction(Type t, out NoggXmlCopyInFunction copyInFunc)
        {
            if (_readerDict.TryGetValue(t, out copyInFunc))
            {
                return copyInFunc != null;
            }
            throw new NotImplementedException();
        }

        public bool Write(XmlWriter writer, string name, T item, bool doMasks, out object maskObj)
        {
            if (this.Write(writer, name, item, doMasks, out M mask))
            {
                maskObj = mask;
                return true;
            }
            maskObj = null;
            return false;
        }
    }
}
