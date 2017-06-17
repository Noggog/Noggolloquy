﻿using Loqui.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;

namespace Loqui.Tests.XML
{
    public abstract class TypicalXmlTranslation_Test<T, M, S>
        where S : new()
    {
        public abstract string ExpectedName { get; }
        public abstract IXmlTranslation<T, M> GetTranslation();
        public static readonly S Instance = new S();
        public abstract T TypicalValue { get; }

        public virtual XElement GetTypicalElement(T value, string name = null)
        {
            var elem = XmlUtility.GetElementNoValue(ExpectedName, name);
            elem.SetAttributeValue(XName.Get(XmlConstants.VALUE_ATTRIBUTE), StringConverter(value));
            return elem;
        }

        public virtual XElement GetTypicalElement(string name = null)
        {
            return GetTypicalElement(TypicalValue, name);
        }

        public virtual XElement GetBadElement(string name = null)
        {
            var elem = XmlUtility.GetElementNoValue(ExpectedName, name);
            elem.SetAttributeValue(XName.Get(XmlConstants.VALUE_ATTRIBUTE), "Gibberish");
            return elem;
        }

        public XElement GetElementNoValue()
        {
            return XmlUtility.GetElementNoValue(this.ExpectedName);
        }

        public virtual string StringConverter(T item)
        {
            return item.ToString();
        }

        public virtual T GetDefault()
        {
            return default(T);
        }

        [Fact]
        public virtual void Parse_BadElement_Mask()
        {
            var transl = GetTranslation();
            var elem = this.GetBadElement();
            Assert.Throws(
                typeof(ArgumentException),
                () => transl.Parse(
                    elem,
                    doMasks: true,
                    maskObj: out M maskObj));
        }

        [Fact]
        public virtual void Parse_BadElement_NoMask()
        {
            var transl = GetTranslation();
            var elem = this.GetBadElement();
            Assert.Throws(
                typeof(ArgumentException),
                () => transl.Parse(
                    elem,
                    doMasks: false,
                    maskObj: out M maskObj));
        }

        [Fact]
        public void ElementName()
        {
            var transl = GetTranslation();
            Assert.Equal(ExpectedName, transl.ElementName);
        }

        [Fact]
        public virtual void Write_NodeName()
        {
            var name = "AName";
            var transl = GetTranslation();
            var writer = XmlUtility.GetWriteBundle();
            transl.Write(
                writer: writer.Writer,
                name: name,
                item: GetDefault(),
                doMasks: false,
                maskObj: out M maskObj);
            Assert.Null(maskObj);
            XElement elem = writer.Resolve();
            var nameAttr = elem.Attribute(XName.Get(XmlConstants.NAME_ATTRIBUTE));
            Assert.NotNull(nameAttr);
            Assert.Equal(name, nameAttr.Value);
        }
    }
}
