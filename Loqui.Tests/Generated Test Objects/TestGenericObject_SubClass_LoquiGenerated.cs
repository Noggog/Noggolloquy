/*
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Autogenerated by Loqui.  Do not manually change.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Loqui;
using Noggog;
using Noggog.Notifying;
using Loqui.Tests.Internals;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using Noggog.Xml;
using Loqui.Xml;

namespace Loqui.Tests
{
    #region Class
    public partial class TestGenericObject_SubClass<S, T, RBase, R> : TestGenericObject<T, RBase, R>, ITestGenericObject_SubClass<S, T, RBase, R>, ILoquiObjectSetter, IEquatable<TestGenericObject_SubClass<S, T, RBase, R>>
        where S : ObjectToRef
        where T : ILoquiObject
        where RBase : ObjectToRef, ILoquiObject, ILoquiObjectGetter
        where R : ILoquiObject, ILoquiObjectGetter
    {
        ILoquiRegistration ILoquiObject.Registration => TestGenericObject_SubClass_Registration.Instance;
        public new static TestGenericObject_SubClass_Registration Registration => TestGenericObject_SubClass_Registration.Instance;

        public TestGenericObject_SubClass()
        {
            CustomCtor();
        }
        partial void CustomCtor();

        #region Loqui Getter Interface

        protected override object GetNthObject(ushort index) => TestGenericObject_SubClassCommon.GetNthObject<S, T, RBase, R>(index, this);

        protected override bool GetNthObjectHasBeenSet(ushort index) => TestGenericObject_SubClassCommon.GetNthObjectHasBeenSet<S, T, RBase, R>(index, this);

        protected override void UnsetNthObject(ushort index, NotifyingUnsetParameters? cmds) => TestGenericObject_SubClassCommon.UnsetNthObject<S, T, RBase, R>(index, this, cmds);

        #endregion

        #region Loqui Interface
        protected override void SetNthObjectHasBeenSet(ushort index, bool on)
        {
            TestGenericObject_SubClassCommon.SetNthObjectHasBeenSet<S, T, RBase, R>(index, on, this);
        }

        #endregion

        #region To String
        public override string ToString()
        {
            return TestGenericObject_SubClassCommon.ToString(this, printMask: null);
        }

        public string ToString(
            string name = null,
            TestGenericObject_SubClass_Mask<bool> printMask = null)
        {
            return TestGenericObject_SubClassCommon.ToString(this, name: name, printMask: printMask);
        }

        public void ToString(
            FileGeneration fg,
            string name = null)
        {
            TestGenericObject_SubClassCommon.ToString(this, fg, name: name, printMask: null);
        }

        #endregion

        public TestGenericObject_SubClass_Mask<bool> GetHasBeenSetMask()
        {
            return TestGenericObject_SubClassCommon.GetHasBeenSetMask(this);
        }
        #region Equals and Hash
        public override bool Equals(object obj)
        {
            if (!(obj is TestGenericObject_SubClass<S, T, RBase, R> rhs)) return false;
            return Equals(rhs);
        }

        public bool Equals(TestGenericObject_SubClass<S, T, RBase, R> rhs)
        {
            return base.Equals(rhs);
        }

        public override int GetHashCode()
        {
            int ret = 0;
            ret = ret.CombineHashCode(base.GetHashCode());
            return ret;
        }

        #endregion


        #region XML Translation
        public new static TestGenericObject_SubClass<S, T, RBase, R> Create_XML(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return Create_XML(XElement.Parse(reader.ReadToEnd()));
            }
        }

        public new static TestGenericObject_SubClass<S, T, RBase, R> Create_XML(XElement root)
        {
            return Create_XML(
                root: root,
                doMasks: false,
                errorMask: out var errorMask);
        }

        public static TestGenericObject_SubClass<S, T, RBase, R> Create_XML(
            XElement root,
            out TestGenericObject_SubClass_ErrorMask errorMask)
        {
            return Create_XML(
                root: root,
                doMasks: true,
                errorMask: out errorMask);
        }

        public static TestGenericObject_SubClass<S, T, RBase, R> Create_XML(
            XElement root,
            bool doMasks,
            out TestGenericObject_SubClass_ErrorMask errorMask)
        {
            TestGenericObject_SubClass_ErrorMask errMaskRet = null;
            var ret = Create_XML_Internal(
                root: root,
                doMasks: doMasks,
                errorMask: doMasks ? () => errMaskRet ?? (errMaskRet = new TestGenericObject_SubClass_ErrorMask()) : default(Func<TestGenericObject_SubClass_ErrorMask>));
            errorMask = errMaskRet;
            return ret;
        }

        private static TestGenericObject_SubClass<S, T, RBase, R> Create_XML_Internal(
            XElement root,
            bool doMasks,
            Func<TestGenericObject_SubClass_ErrorMask> errorMask)
        {
            if (!root.Name.LocalName.Equals("Loqui.Tests.TestGenericObject_SubClass"))
            {
                var ex = new ArgumentException($"Skipping field that did not match proper type. Type: {root.Name.LocalName}, expected: Loqui.Tests.TestGenericObject_SubClass.");
                if (!doMasks) throw ex;
                errorMask().Overall = ex;
                return null;
            }
            var ret = new TestGenericObject_SubClass<S, T, RBase, R>();
            try
            {
                foreach (var elem in root.Elements())
                {
                    if (!elem.TryGetAttribute("name", out XAttribute name)) continue;
                    Fill_XML_Internal(
                        item: ret,
                        root: elem,
                        name: name.Value,
                        typeName: elem.Name.LocalName,
                        doMasks: doMasks,
                        errorMask: errorMask);
                }
            }
            catch (Exception ex)
            {
                if (!doMasks) throw;
                errorMask().Overall = ex;
            }
            return ret;
        }

        protected static void Fill_XML_Internal<S, T, RBase, R>(
            TestGenericObject_SubClass<S, T, RBase, R> item,
            XElement root,
            string name,
            string typeName,
            bool doMasks,
            Func<TestGenericObject_SubClass_ErrorMask> errorMask)
            where S : ObjectToRef
            where T : ILoquiObject
            where RBase : ObjectToRef, ILoquiObject, ILoquiObjectGetter
            where R : ILoquiObject, ILoquiObjectGetter
        {
            switch (name)
            {
                default:
                    TestGenericObject<T, RBase, R>.Fill_XML_Internal(
                        item: item,
                        root: root,
                        name: name,
                        typeName: typeName,
                        doMasks: doMasks,
                        errorMask: errorMask);
                    break;
            }
        }

        public override void CopyIn_XML(XElement root, NotifyingFireParameters? cmds = null)
        {
            LoquiXmlTranslation<TestGenericObject_SubClass<S, T, RBase, R>, TestGenericObject_SubClass_ErrorMask>.Instance.CopyIn(
                root: root,
                item: this,
                skipProtected: true,
                doMasks: false,
                mask: out TestGenericObject_SubClass_ErrorMask errorMask,
                cmds: cmds);
        }

        public virtual void CopyIn_XML(XElement root, out TestGenericObject_SubClass_ErrorMask errorMask, NotifyingFireParameters? cmds = null)
        {
            LoquiXmlTranslation<TestGenericObject_SubClass<S, T, RBase, R>, TestGenericObject_SubClass_ErrorMask>.Instance.CopyIn(
                root: root,
                item: this,
                skipProtected: true,
                doMasks: true,
                mask: out errorMask,
                cmds: cmds);
        }

        public override void CopyIn_XML(XElement root, out TestGenericObject_ErrorMask errorMask, NotifyingFireParameters? cmds = null)
        {
            CopyIn_XML(root, out TestGenericObject_SubClass_ErrorMask errMask, cmds: cmds);
            errorMask = errMask;
        }

        public void Write_XML(Stream stream, out TestGenericObject_SubClass_ErrorMask errorMask)
        {
            TestGenericObject_SubClassCommon.Write_XML(
                this,
                stream,
                out errorMask);
        }

        public void Write_XML(XmlWriter writer, out TestGenericObject_SubClass_ErrorMask errorMask, string name = null)
        {
            TestGenericObject_SubClassCommon.Write_XML(
                writer: writer,
                name: name,
                item: this,
                doMasks: true,
                errorMask: out errorMask);
        }

        #endregion

        public TestGenericObject_SubClass<S, T, RBase, R> Copy(
            TestGenericObject_SubClass_CopyMask copyMask = null,
            ITestGenericObject_SubClassGetter<S, T, RBase, R> def = null)
        {
            return TestGenericObject_SubClass<S, T, RBase, R>.Copy(
                this,
                copyMask: copyMask,
                def: def);
        }

        public static TestGenericObject_SubClass<S, T, RBase, R> Copy(
            ITestGenericObject_SubClass<S, T, RBase, R> item,
            TestGenericObject_SubClass_CopyMask copyMask = null,
            ITestGenericObject_SubClassGetter<S, T, RBase, R> def = null)
        {
            TestGenericObject_SubClass<S, T, RBase, R> ret;
            if (item.GetType().Equals(typeof(TestGenericObject_SubClass<S, T, RBase, R>)))
            {
                ret = new TestGenericObject_SubClass<S, T, RBase, R>();
            }
            else
            {
                ret = (TestGenericObject_SubClass<S, T, RBase, R>)Activator.CreateInstance(item.GetType());
            }
            ret.CopyFieldsFrom(
                item,
                copyMask: copyMask,
                def: def);
            return ret;
        }

        public static CopyType Copy<CopyType>(
            CopyType item,
            TestGenericObject_SubClass_CopyMask copyMask = null,
            ITestGenericObject_SubClassGetter<S, T, RBase, R> def = null)
            where CopyType : class, ITestGenericObject_SubClass<S, T, RBase, R>
        {
            CopyType ret;
            if (item.GetType().Equals(typeof(TestGenericObject_SubClass<S, T, RBase, R>)))
            {
                ret = new TestGenericObject_SubClass<S, T, RBase, R>() as CopyType;
            }
            else
            {
                ret = (CopyType)Activator.CreateInstance(item.GetType());
            }
            ret.CopyFieldsFrom(
                item,
                copyMask: copyMask,
                doErrorMask: false,
                errorMask: null,
                cmds: null,
                def: def);
            return ret;
        }

        public static TestGenericObject_SubClass<S, T, RBase, R> Copy_ToLoqui(
            ITestGenericObject_SubClassGetter<S, T, RBase, R> item,
            TestGenericObject_SubClass_CopyMask copyMask = null,
            ITestGenericObject_SubClassGetter<S, T, RBase, R> def = null)
        {
            var ret = new TestGenericObject_SubClass<S, T, RBase, R>();
            ret.CopyFieldsFrom(
                item,
                copyMask: copyMask,
                def: def);
            return ret;
        }

        protected override void SetNthObject(ushort index, object obj, NotifyingFireParameters? cmds = null)
        {
            TestGenericObject_SubClass_FieldIndex enu = (TestGenericObject_SubClass_FieldIndex)index;
            switch (enu)
            {
                default:
                    base.SetNthObject(index, obj, cmds);
                    break;
            }
        }

        public override void Clear(NotifyingUnsetParameters? cmds = null)
        {
            CallClearPartial_Internal(cmds);
            TestGenericObject_SubClassCommon.Clear(this, cmds);
        }


        public new static TestGenericObject_SubClass<S, T, RBase, R> Create(IEnumerable<KeyValuePair<ushort, object>> fields)
        {
            var ret = new TestGenericObject_SubClass<S, T, RBase, R>();
            ILoquiObjectExt.CopyFieldsIn(ret, fields, def: null, skipProtected: false, cmds: null);
            return ret;
        }

        public static void CopyIn(IEnumerable<KeyValuePair<ushort, object>> fields, TestGenericObject_SubClass<S, T, RBase, R> obj)
        {
            ILoquiObjectExt.CopyFieldsIn(obj, fields, def: null, skipProtected: false, cmds: null);
        }

    }
    #endregion

    #region Interface
    public interface ITestGenericObject_SubClass<S, T, RBase, R> : ITestGenericObject_SubClassGetter<S, T, RBase, R>, ITestGenericObject<T, RBase, R>, ILoquiClass<ITestGenericObject_SubClass<S, T, RBase, R>, ITestGenericObject_SubClassGetter<S, T, RBase, R>>, ILoquiClass<TestGenericObject_SubClass<S, T, RBase, R>, ITestGenericObject_SubClassGetter<S, T, RBase, R>>
        where S : ObjectToRef
        where T : ILoquiObject
        where RBase : ObjectToRef, ILoquiObject, ILoquiObjectGetter
        where R : ILoquiObject, ILoquiObjectGetter
    {
    }

    public interface ITestGenericObject_SubClassGetter<S, T, RBase, R> : ITestGenericObjectGetter<T, RBase, R>
        where S : ObjectToRef
        where T : ILoquiObject
        where RBase : ObjectToRef, ILoquiObject, ILoquiObjectGetter
        where R : ILoquiObject, ILoquiObjectGetter
    {

    }

    #endregion

}

namespace Loqui.Tests.Internals
{
    #region Field Index
    public enum TestGenericObject_SubClass_FieldIndex
    {
    }
    #endregion

    #region Registration
    public class TestGenericObject_SubClass_Registration : ILoquiRegistration
    {
        public static readonly TestGenericObject_SubClass_Registration Instance = new TestGenericObject_SubClass_Registration();

        public static ProtocolDefinition ProtocolDefinition => ProtocolDefinition_LoquiTests.Definition;

        public static readonly ObjectKey ObjectKey = new ObjectKey(
            protocolKey: ProtocolDefinition_LoquiTests.ProtocolKey,
            msgID: 6,
            version: 0);

        public const string GUID = "dfe7e700-e6c2-4772-9965-80cd875e7a21";

        public const ushort FieldCount = 0;

        public static readonly Type MaskType = typeof(TestGenericObject_SubClass_Mask<>);

        public static readonly Type ErrorMaskType = typeof(TestGenericObject_SubClass_ErrorMask);

        public static readonly Type ClassType = typeof(TestGenericObject_SubClass<,,,>);

        public static readonly Type CommonType = typeof(TestGenericObject_SubClassCommon);

        public const string FullName = "Loqui.Tests.TestGenericObject_SubClass";

        public const string Name = "TestGenericObject_SubClass";

        public const byte GenericCount = 4;

        public static readonly Type GenericRegistrationType = typeof(TestGenericObject_SubClass_Registration<,,,>);

        public static ushort? GetNameIndex(StringCaseAgnostic str)
        {
            switch (str.Upper)
            {
                default:
                    return null;
            }
        }

        public static bool GetNthIsEnumerable(ushort index)
        {
            TestGenericObject_SubClass_FieldIndex enu = (TestGenericObject_SubClass_FieldIndex)index;
            switch (enu)
            {
                default:
                    return TestGenericObject_Registration.GetNthIsEnumerable(index);
            }
        }

        public static bool GetNthIsLoqui(ushort index)
        {
            TestGenericObject_SubClass_FieldIndex enu = (TestGenericObject_SubClass_FieldIndex)index;
            switch (enu)
            {
                default:
                    return TestGenericObject_Registration.GetNthIsLoqui(index);
            }
        }

        public static bool GetNthIsSingleton(ushort index)
        {
            TestGenericObject_SubClass_FieldIndex enu = (TestGenericObject_SubClass_FieldIndex)index;
            switch (enu)
            {
                default:
                    return TestGenericObject_Registration.GetNthIsSingleton(index);
            }
        }

        public static string GetNthName(ushort index)
        {
            TestGenericObject_SubClass_FieldIndex enu = (TestGenericObject_SubClass_FieldIndex)index;
            switch (enu)
            {
                default:
                    return TestGenericObject_Registration.GetNthName(index);
            }
        }

        public static bool IsNthDerivative(ushort index)
        {
            TestGenericObject_SubClass_FieldIndex enu = (TestGenericObject_SubClass_FieldIndex)index;
            switch (enu)
            {
                default:
                    return TestGenericObject_Registration.IsNthDerivative(index);
            }
        }

        public static bool IsProtected(ushort index)
        {
            TestGenericObject_SubClass_FieldIndex enu = (TestGenericObject_SubClass_FieldIndex)index;
            switch (enu)
            {
                default:
                    return TestGenericObject_Registration.IsProtected(index);
            }
        }

        public static Type GetNthType(ushort index) => throw new ArgumentException("Cannot get nth type for a generic object here.  Use generic registration instead.");

        #region Interface
        ProtocolDefinition ILoquiRegistration.ProtocolDefinition => ProtocolDefinition;
        ObjectKey ILoquiRegistration.ObjectKey => ObjectKey;
        string ILoquiRegistration.GUID => GUID;
        int ILoquiRegistration.FieldCount => FieldCount;
        Type ILoquiRegistration.MaskType => MaskType;
        Type ILoquiRegistration.ErrorMaskType => ErrorMaskType;
        Type ILoquiRegistration.ClassType => ClassType;
        Type ILoquiRegistration.CommonType => CommonType;
        string ILoquiRegistration.FullName => FullName;
        string ILoquiRegistration.Name => Name;
        byte ILoquiRegistration.GenericCount => GenericCount;
        Type ILoquiRegistration.GenericRegistrationType => GenericRegistrationType;
        ushort? ILoquiRegistration.GetNameIndex(StringCaseAgnostic name) => GetNameIndex(name);
        bool ILoquiRegistration.GetNthIsEnumerable(ushort index) => GetNthIsEnumerable(index);
        bool ILoquiRegistration.GetNthIsLoqui(ushort index) => GetNthIsLoqui(index);
        bool ILoquiRegistration.GetNthIsSingleton(ushort index) => GetNthIsSingleton(index);
        string ILoquiRegistration.GetNthName(ushort index) => GetNthName(index);
        bool ILoquiRegistration.IsNthDerivative(ushort index) => IsNthDerivative(index);
        bool ILoquiRegistration.IsProtected(ushort index) => IsProtected(index);
        Type ILoquiRegistration.GetNthType(ushort index) => GetNthType(index);
        #endregion

    }

    public class TestGenericObject_SubClass_Registration<S, T, RBase, R> : TestGenericObject_SubClass_Registration
        where S : ObjectToRef
        where T : ILoquiObject
        where RBase : ObjectToRef, ILoquiObject, ILoquiObjectGetter
        where R : ILoquiObject, ILoquiObjectGetter
    {
        public static readonly TestGenericObject_SubClass_Registration<S, T, RBase, R> GenericInstance = new TestGenericObject_SubClass_Registration<S, T, RBase, R>();

        public new static Type GetNthType(ushort index)
        {
            TestGenericObject_SubClass_FieldIndex enu = (TestGenericObject_SubClass_FieldIndex)index;
            switch (enu)
            {
                default:
                    return TestGenericObject_Registration.GetNthType(index);
            }
        }

    }
    #endregion

    #region Extensions
    public static class TestGenericObject_SubClassCommon
    {
        #region Copy Fields From
        public static void CopyFieldsFrom<S, T, RBase, R>(
            this ITestGenericObject_SubClass<S, T, RBase, R> item,
            ITestGenericObject_SubClassGetter<S, T, RBase, R> rhs,
            TestGenericObject_SubClass_CopyMask copyMask = null,
            ITestGenericObject_SubClassGetter<S, T, RBase, R> def = null,
            NotifyingFireParameters? cmds = null)
            where S : ObjectToRef
            where T : ILoquiObject
            where RBase : ObjectToRef, ILoquiObject, ILoquiObjectGetter
            where R : ILoquiObject, ILoquiObjectGetter
        {
            TestGenericObject_SubClassCommon.CopyFieldsFrom<S, T, RBase, R>(
                item: item,
                rhs: rhs,
                def: def,
                doErrorMask: false,
                errorMask: null,
                copyMask: copyMask,
                cmds: cmds);
        }

        public static void CopyFieldsFrom<S, T, RBase, R>(
            this ITestGenericObject_SubClass<S, T, RBase, R> item,
            ITestGenericObject_SubClassGetter<S, T, RBase, R> rhs,
            out TestGenericObject_SubClass_ErrorMask errorMask,
            TestGenericObject_SubClass_CopyMask copyMask = null,
            ITestGenericObject_SubClassGetter<S, T, RBase, R> def = null,
            NotifyingFireParameters? cmds = null)
            where S : ObjectToRef
            where T : ILoquiObject
            where RBase : ObjectToRef, ILoquiObject, ILoquiObjectGetter
            where R : ILoquiObject, ILoquiObjectGetter
        {
            TestGenericObject_SubClassCommon.CopyFieldsFrom<S, T, RBase, R>(
                item: item,
                rhs: rhs,
                def: def,
                doErrorMask: true,
                errorMask: out errorMask,
                copyMask: copyMask,
                cmds: cmds);
        }

        public static void CopyFieldsFrom<S, T, RBase, R>(
            this ITestGenericObject_SubClass<S, T, RBase, R> item,
            ITestGenericObject_SubClassGetter<S, T, RBase, R> rhs,
            ITestGenericObject_SubClassGetter<S, T, RBase, R> def,
            bool doErrorMask,
            out TestGenericObject_SubClass_ErrorMask errorMask,
            TestGenericObject_SubClass_CopyMask copyMask,
            NotifyingFireParameters? cmds)
            where S : ObjectToRef
            where T : ILoquiObject
            where RBase : ObjectToRef, ILoquiObject, ILoquiObjectGetter
            where R : ILoquiObject, ILoquiObjectGetter
        {
            TestGenericObject_SubClass_ErrorMask retErrorMask = null;
            Func<TestGenericObject_SubClass_ErrorMask> maskGetter = () =>
            {
                if (retErrorMask == null)
                {
                    retErrorMask = new TestGenericObject_SubClass_ErrorMask();
                }
                return retErrorMask;
            };
            CopyFieldsFrom<S, T, RBase, R>(
                item: item,
                rhs: rhs,
                def: def,
                doErrorMask: true,
                errorMask: maskGetter,
                copyMask: copyMask,
                cmds: cmds);
            errorMask = retErrorMask;
        }

        public static void CopyFieldsFrom<S, T, RBase, R>(
            this ITestGenericObject_SubClass<S, T, RBase, R> item,
            ITestGenericObject_SubClassGetter<S, T, RBase, R> rhs,
            ITestGenericObject_SubClassGetter<S, T, RBase, R> def,
            bool doErrorMask,
            Func<TestGenericObject_SubClass_ErrorMask> errorMask,
            TestGenericObject_SubClass_CopyMask copyMask,
            NotifyingFireParameters? cmds)
            where S : ObjectToRef
            where T : ILoquiObject
            where RBase : ObjectToRef, ILoquiObject, ILoquiObjectGetter
            where R : ILoquiObject, ILoquiObjectGetter
        {
            TestGenericObjectCommon.CopyFieldsFrom<T, RBase, R>(
                item,
                rhs,
                def,
                doErrorMask,
                errorMask,
                copyMask,
                cmds);
        }

        #endregion

        public static void SetNthObjectHasBeenSet<S, T, RBase, R>(
            ushort index,
            bool on,
            ITestGenericObject_SubClass<S, T, RBase, R> obj,
            NotifyingFireParameters? cmds = null)
            where S : ObjectToRef
            where T : ILoquiObject
            where RBase : ObjectToRef, ILoquiObject, ILoquiObjectGetter
            where R : ILoquiObject, ILoquiObjectGetter
        {
            TestGenericObject_SubClass_FieldIndex enu = (TestGenericObject_SubClass_FieldIndex)index;
            switch (enu)
            {
                default:
                    TestGenericObjectCommon.SetNthObjectHasBeenSet<T, RBase, R>(index, on, obj);
                    break;
            }
        }

        public static void UnsetNthObject<S, T, RBase, R>(
            ushort index,
            ITestGenericObject_SubClass<S, T, RBase, R> obj,
            NotifyingUnsetParameters? cmds = null)
            where S : ObjectToRef
            where T : ILoquiObject
            where RBase : ObjectToRef, ILoquiObject, ILoquiObjectGetter
            where R : ILoquiObject, ILoquiObjectGetter
        {
            TestGenericObject_SubClass_FieldIndex enu = (TestGenericObject_SubClass_FieldIndex)index;
            switch (enu)
            {
                default:
                    TestGenericObjectCommon.UnsetNthObject<T, RBase, R>(index, obj);
                    break;
            }
        }

        public static bool GetNthObjectHasBeenSet<S, T, RBase, R>(
            ushort index,
            ITestGenericObject_SubClass<S, T, RBase, R> obj)
            where S : ObjectToRef
            where T : ILoquiObject
            where RBase : ObjectToRef, ILoquiObject, ILoquiObjectGetter
            where R : ILoquiObject, ILoquiObjectGetter
        {
            TestGenericObject_SubClass_FieldIndex enu = (TestGenericObject_SubClass_FieldIndex)index;
            switch (enu)
            {
                default:
                    return TestGenericObjectCommon.GetNthObjectHasBeenSet<T, RBase, R>(index, obj);
            }
        }

        public static object GetNthObject<S, T, RBase, R>(
            ushort index,
            ITestGenericObject_SubClassGetter<S, T, RBase, R> obj)
            where S : ObjectToRef
            where T : ILoquiObject
            where RBase : ObjectToRef, ILoquiObject, ILoquiObjectGetter
            where R : ILoquiObject, ILoquiObjectGetter
        {
            TestGenericObject_SubClass_FieldIndex enu = (TestGenericObject_SubClass_FieldIndex)index;
            switch (enu)
            {
                default:
                    return TestGenericObjectCommon.GetNthObject<T, RBase, R>(index, obj);
            }
        }

        public static void Clear<S, T, RBase, R>(
            ITestGenericObject_SubClass<S, T, RBase, R> item,
            NotifyingUnsetParameters? cmds = null)
            where S : ObjectToRef
            where T : ILoquiObject
            where RBase : ObjectToRef, ILoquiObject, ILoquiObjectGetter
            where R : ILoquiObject, ILoquiObjectGetter
        {
        }

        public static TestGenericObject_SubClass_Mask<bool> GetEqualsMask<S, T, RBase, R>(
            this ITestGenericObject_SubClassGetter<S, T, RBase, R> item,
            ITestGenericObject_SubClassGetter<S, T, RBase, R> rhs)
            where S : ObjectToRef
            where T : ILoquiObject
            where RBase : ObjectToRef, ILoquiObject, ILoquiObjectGetter
            where R : ILoquiObject, ILoquiObjectGetter
        {
            var ret = new TestGenericObject_SubClass_Mask<bool>();
            FillEqualsMask(item, rhs, ret);
            return ret;
        }

        public static void FillEqualsMask<S, T, RBase, R>(
            ITestGenericObject_SubClassGetter<S, T, RBase, R> item,
            ITestGenericObject_SubClassGetter<S, T, RBase, R> rhs,
            TestGenericObject_SubClass_Mask<bool> ret)
            where S : ObjectToRef
            where T : ILoquiObject
            where RBase : ObjectToRef, ILoquiObject, ILoquiObjectGetter
            where R : ILoquiObject, ILoquiObjectGetter
        {
            TestGenericObjectCommon.FillEqualsMask(item, rhs, ret);
        }

        public static string ToString<S, T, RBase, R>(
            this ITestGenericObject_SubClassGetter<S, T, RBase, R> item,
            string name = null,
            TestGenericObject_SubClass_Mask<bool> printMask = null)
            where S : ObjectToRef
            where T : ILoquiObject
            where RBase : ObjectToRef, ILoquiObject, ILoquiObjectGetter
            where R : ILoquiObject, ILoquiObjectGetter
        {
            var fg = new FileGeneration();
            item.ToString(fg, name, printMask);
            return fg.ToString();
        }

        public static void ToString<S, T, RBase, R>(
            this ITestGenericObject_SubClassGetter<S, T, RBase, R> item,
            FileGeneration fg,
            string name = null,
            TestGenericObject_SubClass_Mask<bool> printMask = null)
            where S : ObjectToRef
            where T : ILoquiObject
            where RBase : ObjectToRef, ILoquiObject, ILoquiObjectGetter
            where R : ILoquiObject, ILoquiObjectGetter
        {
            if (name == null)
            {
                fg.AppendLine($"{nameof(TestGenericObject_SubClass<S, T, RBase, R>)} =>");
            }
            else
            {
                fg.AppendLine($"{name} ({nameof(TestGenericObject_SubClass<S, T, RBase, R>)}) =>");
            }
            fg.AppendLine("[");
            using (new DepthWrapper(fg))
            {
            }
            fg.AppendLine("]");
        }

        public static bool HasBeenSet<S, T, RBase, R>(
            this ITestGenericObject_SubClassGetter<S, T, RBase, R> item,
            TestGenericObject_SubClass_Mask<bool?> checkMask)
            where S : ObjectToRef
            where T : ILoquiObject
            where RBase : ObjectToRef, ILoquiObject, ILoquiObjectGetter
            where R : ILoquiObject, ILoquiObjectGetter
        {
            return true;
        }

        public static TestGenericObject_SubClass_Mask<bool> GetHasBeenSetMask<S, T, RBase, R>(ITestGenericObject_SubClassGetter<S, T, RBase, R> item)
            where S : ObjectToRef
            where T : ILoquiObject
            where RBase : ObjectToRef, ILoquiObject, ILoquiObjectGetter
            where R : ILoquiObject, ILoquiObjectGetter
        {
            var ret = new TestGenericObject_SubClass_Mask<bool>();
            return ret;
        }

        #region XML Translation
        #region XML Write
        public static void Write_XML<S, T, RBase, R>(
            ITestGenericObject_SubClassGetter<S, T, RBase, R> item,
            Stream stream)
            where S : ObjectToRef
            where T : ILoquiObject
            where RBase : ObjectToRef, ILoquiObject, ILoquiObjectGetter
            where R : ILoquiObject, ILoquiObjectGetter
        {
            using (var writer = new XmlTextWriter(stream, Encoding.ASCII))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 3;
                Write_XML(
                    writer: writer,
                    name: null,
                    item: item,
                    doMasks: false,
                    errorMask: out TestGenericObject_SubClass_ErrorMask errorMask);
            }
        }

        public static void Write_XML<S, T, RBase, R>(
            ITestGenericObject_SubClassGetter<S, T, RBase, R> item,
            Stream stream,
            out TestGenericObject_SubClass_ErrorMask errorMask)
            where S : ObjectToRef
            where T : ILoquiObject
            where RBase : ObjectToRef, ILoquiObject, ILoquiObjectGetter
            where R : ILoquiObject, ILoquiObjectGetter
        {
            using (var writer = new XmlTextWriter(stream, Encoding.ASCII))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 3;
                Write_XML(
                    writer: writer,
                    name: null,
                    item: item,
                    doMasks: true,
                    errorMask: out errorMask);
            }
        }

        public static void Write_XML<S, T, RBase, R>(
            XmlWriter writer,
            string name,
            ITestGenericObject_SubClassGetter<S, T, RBase, R> item,
            bool doMasks,
            out TestGenericObject_SubClass_ErrorMask errorMask)
            where S : ObjectToRef
            where T : ILoquiObject
            where RBase : ObjectToRef, ILoquiObject, ILoquiObjectGetter
            where R : ILoquiObject, ILoquiObjectGetter
        {
            TestGenericObject_SubClass_ErrorMask errMaskRet = null;
            Write_XML_Internal(
                writer: writer,
                name: name,
                item: item,
                doMasks: doMasks,
                errorMask: doMasks ? () => errMaskRet ?? (errMaskRet = new TestGenericObject_SubClass_ErrorMask()) : default(Func<TestGenericObject_SubClass_ErrorMask>));
            errorMask = errMaskRet;
        }

        private static void Write_XML_Internal<S, T, RBase, R>(
            XmlWriter writer,
            string name,
            ITestGenericObject_SubClassGetter<S, T, RBase, R> item,
            bool doMasks,
            Func<TestGenericObject_SubClass_ErrorMask> errorMask)
            where S : ObjectToRef
            where T : ILoquiObject
            where RBase : ObjectToRef, ILoquiObject, ILoquiObjectGetter
            where R : ILoquiObject, ILoquiObjectGetter
        {
            try
            {
                using (new ElementWrapper(writer, "Loqui.Tests.TestGenericObject_SubClass"))
                {
                    if (!string.IsNullOrEmpty(name))
                    {
                        writer.WriteAttributeString("name", name);
                    }
                }
            }
            catch (Exception ex)
            {
                if (!doMasks) throw;
                errorMask().Overall = ex;
            }
        }
        #endregion

        #endregion

    }
    #endregion

    #region Modules

    #region Mask
    public class TestGenericObject_SubClass_Mask<T> : TestGenericObject_Mask<T>, IMask<T>
    {
        #region Ctors
        public TestGenericObject_SubClass_Mask()
        {
        }

        public TestGenericObject_SubClass_Mask(T initialValue)
        {
        }
        #endregion

        #region All Equal
        public bool AllEqual(Func<T, bool> eval)
        {
            return true;
        }
        #endregion

        #region Translate
        public TestGenericObject_SubClass_Mask<R> Translate<R>(Func<T, R> eval)
        {
            var ret = new TestGenericObject_SubClass_Mask<R>();
            return ret;
        }
        #endregion

        #region To String
        public override string ToString()
        {
            return ToString(printMask: null);
        }

        public string ToString(TestGenericObject_SubClass_Mask<bool> printMask = null)
        {
            var fg = new FileGeneration();
            ToString(fg, printMask);
            return fg.ToString();
        }

        public void ToString(FileGeneration fg, TestGenericObject_SubClass_Mask<bool> printMask = null)
        {
            fg.AppendLine($"{nameof(TestGenericObject_SubClass_Mask<T>)} =>");
            fg.AppendLine("[");
            using (new DepthWrapper(fg))
            {
            }
            fg.AppendLine("]");
        }
        #endregion

    }

    public class TestGenericObject_SubClass_ErrorMask : TestGenericObject_ErrorMask
    {
        #region IErrorMask
        public override void SetNthException(ushort index, Exception ex)
        {
            TestGenericObject_SubClass_FieldIndex enu = (TestGenericObject_SubClass_FieldIndex)index;
            switch (enu)
            {
                default:
                    base.SetNthException(index, ex);
                    break;
            }
        }

        public override void SetNthMask(ushort index, object obj)
        {
            TestGenericObject_SubClass_FieldIndex enu = (TestGenericObject_SubClass_FieldIndex)index;
            switch (enu)
            {
                default:
                    base.SetNthMask(index, obj);
                    break;
            }
        }
        #endregion

        #region To String
        public override string ToString()
        {
            var fg = new FileGeneration();
            ToString(fg);
            return fg.ToString();
        }

        public void ToString(FileGeneration fg)
        {
            fg.AppendLine("TestGenericObject_SubClass_ErrorMask =>");
            fg.AppendLine("[");
            using (new DepthWrapper(fg))
            {
            }
            fg.AppendLine("]");
        }
        #endregion

        #region Combine
        public TestGenericObject_SubClass_ErrorMask Combine(TestGenericObject_SubClass_ErrorMask rhs)
        {
            var ret = new TestGenericObject_SubClass_ErrorMask();
            return ret;
        }
        public static TestGenericObject_SubClass_ErrorMask Combine(TestGenericObject_SubClass_ErrorMask lhs, TestGenericObject_SubClass_ErrorMask rhs)
        {
            if (lhs != null && rhs != null) return lhs.Combine(rhs);
            return lhs ?? rhs;
        }
        #endregion

    }
    public class TestGenericObject_SubClass_CopyMask : TestGenericObject_CopyMask
    {
    }
    #endregion


    #endregion

}
