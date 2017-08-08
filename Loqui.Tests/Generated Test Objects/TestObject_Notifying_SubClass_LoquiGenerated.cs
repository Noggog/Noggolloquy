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
    public partial class TestObject_Notifying_SubClass : TestObject_Notifying, ITestObject_Notifying_SubClass, ILoquiObjectSetter, IEquatable<TestObject_Notifying_SubClass>
    {
        ILoquiRegistration ILoquiObject.Registration => TestObject_Notifying_SubClass_Registration.Instance;
        public new static TestObject_Notifying_SubClass_Registration Registration => TestObject_Notifying_SubClass_Registration.Instance;

        #region Ctor
        public TestObject_Notifying_SubClass()
        {
            CustomCtor();
        }
        partial void CustomCtor();
        #endregion

        #region NewField
        protected readonly INotifyingItem<Boolean> _NewField = NotifyingItem.Factory<Boolean>(markAsSet: false);
        public INotifyingItem<Boolean> NewField_Property => _NewField;
        public Boolean NewField
        {
            get => this._NewField.Item;
            set => this._NewField.Set(value);
        }
        INotifyingItem<Boolean> ITestObject_Notifying_SubClass.NewField_Property => this.NewField_Property;
        INotifyingItemGetter<Boolean> ITestObject_Notifying_SubClassGetter.NewField_Property => this.NewField_Property;
        #endregion

        #region Loqui Getter Interface

        protected override object GetNthObject(ushort index) => TestObject_Notifying_SubClassCommon.GetNthObject(index, this);

        protected override bool GetNthObjectHasBeenSet(ushort index) => TestObject_Notifying_SubClassCommon.GetNthObjectHasBeenSet(index, this);

        protected override void UnsetNthObject(ushort index, NotifyingUnsetParameters? cmds) => TestObject_Notifying_SubClassCommon.UnsetNthObject(index, this, cmds);

        #endregion

        #region Loqui Interface
        protected override void SetNthObjectHasBeenSet(ushort index, bool on)
        {
            TestObject_Notifying_SubClassCommon.SetNthObjectHasBeenSet(index, on, this);
        }

        #endregion

        #region To String
        public override string ToString()
        {
            return TestObject_Notifying_SubClassCommon.ToString(this, printMask: null);
        }

        public string ToString(
            string name = null,
            TestObject_Notifying_SubClass_Mask<bool> printMask = null)
        {
            return TestObject_Notifying_SubClassCommon.ToString(this, name: name, printMask: printMask);
        }

        public void ToString(
            FileGeneration fg,
            string name = null)
        {
            TestObject_Notifying_SubClassCommon.ToString(this, fg, name: name, printMask: null);
        }

        #endregion

        public TestObject_Notifying_SubClass_Mask<bool> GetHasBeenSetMask()
        {
            return TestObject_Notifying_SubClassCommon.GetHasBeenSetMask(this);
        }
        #region Equals and Hash
        public override bool Equals(object obj)
        {
            if (!(obj is TestObject_Notifying_SubClass rhs)) return false;
            return Equals(rhs);
        }

        public bool Equals(TestObject_Notifying_SubClass rhs)
        {
            if (rhs == null) return false;
            if (NewField_Property.HasBeenSet != rhs.NewField_Property.HasBeenSet) return false;
            if (NewField_Property.HasBeenSet)
            {
                if (NewField != rhs.NewField) return false;
            }
            return base.Equals(rhs);
        }

        public override int GetHashCode()
        {
            int ret = 0;
            if (NewField_Property.HasBeenSet)
            {
                ret = HashHelper.GetHashCode(NewField).CombineHashCode(ret);
            }
            ret = ret.CombineHashCode(base.GetHashCode());
            return ret;
        }

        #endregion


        #region XML Translation
        public new static TestObject_Notifying_SubClass Create_XML(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return Create_XML(XElement.Parse(reader.ReadToEnd()));
            }
        }

        public new static TestObject_Notifying_SubClass Create_XML(XElement root)
        {
            return Create_XML(
                root: root,
                doMasks: false,
                errorMask: out var errorMask);
        }

        public static TestObject_Notifying_SubClass Create_XML(
            XElement root,
            out TestObject_Notifying_SubClass_ErrorMask errorMask)
        {
            return Create_XML(
                root: root,
                doMasks: true,
                errorMask: out errorMask);
        }

        public static TestObject_Notifying_SubClass Create_XML(
            XElement root,
            bool doMasks,
            out TestObject_Notifying_SubClass_ErrorMask errorMask)
        {
            TestObject_Notifying_SubClass_ErrorMask errMaskRet = null;
            var ret = Create_XML_Internal(
                root: root,
                doMasks: doMasks,
                errorMask: doMasks ? () => errMaskRet ?? (errMaskRet = new TestObject_Notifying_SubClass_ErrorMask()) : default(Func<TestObject_Notifying_SubClass_ErrorMask>));
            errorMask = errMaskRet;
            return ret;
        }

        private static TestObject_Notifying_SubClass Create_XML_Internal(
            XElement root,
            bool doMasks,
            Func<TestObject_Notifying_SubClass_ErrorMask> errorMask)
        {
            var ret = new TestObject_Notifying_SubClass();
            try
            {
                foreach (var elem in root.Elements())
                {
                    Fill_XML_Internal(
                        item: ret,
                        root: elem,
                        name: elem.Name.LocalName,
                        doMasks: doMasks,
                        errorMask: errorMask);
                }
            }
            catch (Exception ex)
            when (doMasks)
            {
                errorMask().Overall = ex;
            }
            return ret;
        }

        protected static void Fill_XML_Internal(
            TestObject_Notifying_SubClass item,
            XElement root,
            string name,
            bool doMasks,
            Func<TestObject_Notifying_SubClass_ErrorMask> errorMask)
        {
            switch (name)
            {
                case "NewField":
                    {
                        Exception subMask;
                        var tryGet = BooleanXmlTranslation.Instance.Parse(
                            root,
                            nullable: false,
                            doMasks: doMasks,
                            errorMask: out subMask);
                        if (tryGet.Succeeded)
                        {
                            item._NewField.Item = tryGet.Value.Value;
                        }
                        if (doMasks && subMask != null)
                        {
                            errorMask().NewField = subMask;
                        }
                    }
                    break;
                default:
                    TestObject_Notifying.Fill_XML_Internal(
                        item: item,
                        root: root,
                        name: name,
                        doMasks: doMasks,
                        errorMask: errorMask);
                    break;
            }
        }

        public override void CopyIn_XML(XElement root, NotifyingFireParameters? cmds = null)
        {
            LoquiXmlTranslation<TestObject_Notifying_SubClass, TestObject_Notifying_SubClass_ErrorMask>.Instance.CopyIn(
                root: root,
                item: this,
                skipProtected: true,
                doMasks: false,
                mask: out TestObject_Notifying_SubClass_ErrorMask errorMask,
                cmds: cmds);
        }

        public virtual void CopyIn_XML(XElement root, out TestObject_Notifying_SubClass_ErrorMask errorMask, NotifyingFireParameters? cmds = null)
        {
            LoquiXmlTranslation<TestObject_Notifying_SubClass, TestObject_Notifying_SubClass_ErrorMask>.Instance.CopyIn(
                root: root,
                item: this,
                skipProtected: true,
                doMasks: true,
                mask: out errorMask,
                cmds: cmds);
        }

        public override void CopyIn_XML(XElement root, out TestObject_Notifying_ErrorMask errorMask, NotifyingFireParameters? cmds = null)
        {
            CopyIn_XML(root, out TestObject_Notifying_SubClass_ErrorMask errMask, cmds: cmds);
            errorMask = errMask;
        }

        public void Write_XML(Stream stream, out TestObject_Notifying_SubClass_ErrorMask errorMask)
        {
            TestObject_Notifying_SubClassCommon.Write_XML(
                this,
                stream,
                out errorMask);
        }

        public void Write_XML(XmlWriter writer, out TestObject_Notifying_SubClass_ErrorMask errorMask, string name = null)
        {
            TestObject_Notifying_SubClassCommon.Write_XML(
                writer: writer,
                name: name,
                item: this,
                doMasks: true,
                errorMask: out errorMask);
        }

        #endregion

        public TestObject_Notifying_SubClass Copy(
            TestObject_Notifying_SubClass_CopyMask copyMask = null,
            ITestObject_Notifying_SubClassGetter def = null)
        {
            return TestObject_Notifying_SubClass.Copy(
                this,
                copyMask: copyMask,
                def: def);
        }

        public static TestObject_Notifying_SubClass Copy(
            ITestObject_Notifying_SubClass item,
            TestObject_Notifying_SubClass_CopyMask copyMask = null,
            ITestObject_Notifying_SubClassGetter def = null)
        {
            TestObject_Notifying_SubClass ret;
            if (item.GetType().Equals(typeof(TestObject_Notifying_SubClass)))
            {
                ret = new TestObject_Notifying_SubClass();
            }
            else
            {
                ret = (TestObject_Notifying_SubClass)Activator.CreateInstance(item.GetType());
            }
            ret.CopyFieldsFrom(
                item,
                copyMask: copyMask,
                def: def);
            return ret;
        }

        public static CopyType CopyGeneric<CopyType>(
            CopyType item,
            TestObject_Notifying_SubClass_CopyMask copyMask = null,
            ITestObject_Notifying_SubClassGetter def = null)
            where CopyType : class, ITestObject_Notifying_SubClass
        {
            CopyType ret;
            if (item.GetType().Equals(typeof(TestObject_Notifying_SubClass)))
            {
                ret = new TestObject_Notifying_SubClass() as CopyType;
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

        public static TestObject_Notifying_SubClass Copy_ToLoqui(
            ITestObject_Notifying_SubClassGetter item,
            TestObject_Notifying_SubClass_CopyMask copyMask = null,
            ITestObject_Notifying_SubClassGetter def = null)
        {
            var ret = new TestObject_Notifying_SubClass();
            ret.CopyFieldsFrom(
                item,
                copyMask: copyMask,
                def: def);
            return ret;
        }

        protected override void SetNthObject(ushort index, object obj, NotifyingFireParameters? cmds = null)
        {
            TestObject_Notifying_SubClass_FieldIndex enu = (TestObject_Notifying_SubClass_FieldIndex)index;
            switch (enu)
            {
                case TestObject_Notifying_SubClass_FieldIndex.NewField:
                    this._NewField.Set(
                        (Boolean)obj,
                        cmds);
                    break;
                default:
                    base.SetNthObject(index, obj, cmds);
                    break;
            }
        }

        public override void Clear(NotifyingUnsetParameters? cmds = null)
        {
            CallClearPartial_Internal(cmds);
            TestObject_Notifying_SubClassCommon.Clear(this, cmds);
        }


        public new static TestObject_Notifying_SubClass Create(IEnumerable<KeyValuePair<ushort, object>> fields)
        {
            var ret = new TestObject_Notifying_SubClass();
            foreach (var pair in fields)
            {
                CopyInInternal_TestObject_Notifying_SubClass(ret, pair);
            }
            return ret;
        }

        protected new static void CopyInInternal_TestObject_Notifying_SubClass(TestObject_Notifying_SubClass obj, KeyValuePair<ushort, object> pair)
        {
            if (!EnumExt.TryParse(pair.Key, out TestObject_Notifying_SubClass_FieldIndex enu))
            {
                CopyInInternal_TestObject_Notifying(obj, pair);
            }
            switch (enu)
            {
                case TestObject_Notifying_SubClass_FieldIndex.NewField:
                    obj._NewField.Set(
                        (Boolean)pair.Value,
                        null);
                    break;
                default:
                    throw new ArgumentException($"Unknown enum type: {enu}");
            }
        }
        public static void CopyIn(IEnumerable<KeyValuePair<ushort, object>> fields, TestObject_Notifying_SubClass obj)
        {
            ILoquiObjectExt.CopyFieldsIn(obj, fields, def: null, skipProtected: false, cmds: null);
        }

    }
    #endregion

    #region Interface
    public interface ITestObject_Notifying_SubClass : ITestObject_Notifying_SubClassGetter, ITestObject_Notifying, ILoquiClass<ITestObject_Notifying_SubClass, ITestObject_Notifying_SubClassGetter>, ILoquiClass<TestObject_Notifying_SubClass, ITestObject_Notifying_SubClassGetter>
    {
        new Boolean NewField { get; set; }
        new INotifyingItem<Boolean> NewField_Property { get; }

    }

    public interface ITestObject_Notifying_SubClassGetter : ITestObject_NotifyingGetter
    {
        #region NewField
        Boolean NewField { get; }
        INotifyingItemGetter<Boolean> NewField_Property { get; }

        #endregion

    }

    #endregion

}

namespace Loqui.Tests.Internals
{
    #region Field Index
    public enum TestObject_Notifying_SubClass_FieldIndex
    {
        NewField = 103,
    }
    #endregion

    #region Registration
    public class TestObject_Notifying_SubClass_Registration : ILoquiRegistration
    {
        public static readonly TestObject_Notifying_SubClass_Registration Instance = new TestObject_Notifying_SubClass_Registration();

        public static ProtocolKey ProtocolKey => ProtocolDefinition_LoquiTests.ProtocolKey;

        public static readonly ObjectKey ObjectKey = new ObjectKey(
            protocolKey: ProtocolDefinition_LoquiTests.ProtocolKey,
            msgID: 4,
            version: 0);

        public const string GUID = "3c0cceee-3747-449d-ae3e-617f5c366ef7";

        public const ushort FieldCount = 1;

        public static readonly Type MaskType = typeof(TestObject_Notifying_SubClass_Mask<>);

        public static readonly Type ErrorMaskType = typeof(TestObject_Notifying_SubClass_ErrorMask);

        public static readonly Type ClassType = typeof(TestObject_Notifying_SubClass);

        public static readonly Type GetterType = typeof(ITestObject_Notifying_SubClassGetter);

        public static readonly Type SetterType = typeof(ITestObject_Notifying_SubClass);

        public static readonly Type CommonType = typeof(TestObject_Notifying_SubClassCommon);

        public const string FullName = "Loqui.Tests.TestObject_Notifying_SubClass";

        public const string Name = "TestObject_Notifying_SubClass";

        public const string Namespace = "Loqui.Tests";

        public const byte GenericCount = 0;

        public static readonly Type GenericRegistrationType = null;

        public static ushort? GetNameIndex(StringCaseAgnostic str)
        {
            switch (str.Upper)
            {
                case "NEWFIELD":
                    return (ushort)TestObject_Notifying_SubClass_FieldIndex.NewField;
                default:
                    return null;
            }
        }

        public static bool GetNthIsEnumerable(ushort index)
        {
            TestObject_Notifying_SubClass_FieldIndex enu = (TestObject_Notifying_SubClass_FieldIndex)index;
            switch (enu)
            {
                case TestObject_Notifying_SubClass_FieldIndex.NewField:
                    return false;
                default:
                    return TestObject_Notifying_Registration.GetNthIsEnumerable(index);
            }
        }

        public static bool GetNthIsLoqui(ushort index)
        {
            TestObject_Notifying_SubClass_FieldIndex enu = (TestObject_Notifying_SubClass_FieldIndex)index;
            switch (enu)
            {
                case TestObject_Notifying_SubClass_FieldIndex.NewField:
                    return false;
                default:
                    return TestObject_Notifying_Registration.GetNthIsLoqui(index);
            }
        }

        public static bool GetNthIsSingleton(ushort index)
        {
            TestObject_Notifying_SubClass_FieldIndex enu = (TestObject_Notifying_SubClass_FieldIndex)index;
            switch (enu)
            {
                case TestObject_Notifying_SubClass_FieldIndex.NewField:
                    return false;
                default:
                    return TestObject_Notifying_Registration.GetNthIsSingleton(index);
            }
        }

        public static string GetNthName(ushort index)
        {
            TestObject_Notifying_SubClass_FieldIndex enu = (TestObject_Notifying_SubClass_FieldIndex)index;
            switch (enu)
            {
                case TestObject_Notifying_SubClass_FieldIndex.NewField:
                    return "NewField";
                default:
                    return TestObject_Notifying_Registration.GetNthName(index);
            }
        }

        public static bool IsNthDerivative(ushort index)
        {
            TestObject_Notifying_SubClass_FieldIndex enu = (TestObject_Notifying_SubClass_FieldIndex)index;
            switch (enu)
            {
                case TestObject_Notifying_SubClass_FieldIndex.NewField:
                    return false;
                default:
                    return TestObject_Notifying_Registration.IsNthDerivative(index);
            }
        }

        public static bool IsProtected(ushort index)
        {
            TestObject_Notifying_SubClass_FieldIndex enu = (TestObject_Notifying_SubClass_FieldIndex)index;
            switch (enu)
            {
                case TestObject_Notifying_SubClass_FieldIndex.NewField:
                    return false;
                default:
                    return TestObject_Notifying_Registration.IsProtected(index);
            }
        }

        public static Type GetNthType(ushort index)
        {
            TestObject_Notifying_SubClass_FieldIndex enu = (TestObject_Notifying_SubClass_FieldIndex)index;
            switch (enu)
            {
                case TestObject_Notifying_SubClass_FieldIndex.NewField:
                    return typeof(Boolean);
                default:
                    return TestObject_Notifying_Registration.GetNthType(index);
            }
        }

        #region Interface
        ProtocolKey ILoquiRegistration.ProtocolKey => ProtocolKey;
        ObjectKey ILoquiRegistration.ObjectKey => ObjectKey;
        string ILoquiRegistration.GUID => GUID;
        int ILoquiRegistration.FieldCount => FieldCount;
        Type ILoquiRegistration.MaskType => MaskType;
        Type ILoquiRegistration.ErrorMaskType => ErrorMaskType;
        Type ILoquiRegistration.ClassType => ClassType;
        Type ILoquiRegistration.SetterType => SetterType;
        Type ILoquiRegistration.GetterType => GetterType;
        Type ILoquiRegistration.CommonType => CommonType;
        string ILoquiRegistration.FullName => FullName;
        string ILoquiRegistration.Name => Name;
        string ILoquiRegistration.Namespace => Namespace;
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
    #endregion

    #region Extensions
    public static class TestObject_Notifying_SubClassCommon
    {
        #region Copy Fields From
        public static void CopyFieldsFrom(
            this ITestObject_Notifying_SubClass item,
            ITestObject_Notifying_SubClassGetter rhs,
            TestObject_Notifying_SubClass_CopyMask copyMask = null,
            ITestObject_Notifying_SubClassGetter def = null,
            NotifyingFireParameters? cmds = null)
        {
            TestObject_Notifying_SubClassCommon.CopyFieldsFrom(
                item: item,
                rhs: rhs,
                def: def,
                doErrorMask: false,
                errorMask: null,
                copyMask: copyMask,
                cmds: cmds);
        }

        public static void CopyFieldsFrom(
            this ITestObject_Notifying_SubClass item,
            ITestObject_Notifying_SubClassGetter rhs,
            out TestObject_Notifying_SubClass_ErrorMask errorMask,
            TestObject_Notifying_SubClass_CopyMask copyMask = null,
            ITestObject_Notifying_SubClassGetter def = null,
            NotifyingFireParameters? cmds = null)
        {
            TestObject_Notifying_SubClassCommon.CopyFieldsFrom(
                item: item,
                rhs: rhs,
                def: def,
                doErrorMask: true,
                errorMask: out errorMask,
                copyMask: copyMask,
                cmds: cmds);
        }

        public static void CopyFieldsFrom(
            this ITestObject_Notifying_SubClass item,
            ITestObject_Notifying_SubClassGetter rhs,
            ITestObject_Notifying_SubClassGetter def,
            bool doErrorMask,
            out TestObject_Notifying_SubClass_ErrorMask errorMask,
            TestObject_Notifying_SubClass_CopyMask copyMask,
            NotifyingFireParameters? cmds)
        {
            TestObject_Notifying_SubClass_ErrorMask retErrorMask = null;
            Func<TestObject_Notifying_SubClass_ErrorMask> maskGetter = () =>
            {
                if (retErrorMask == null)
                {
                    retErrorMask = new TestObject_Notifying_SubClass_ErrorMask();
                }
                return retErrorMask;
            };
            CopyFieldsFrom(
                item: item,
                rhs: rhs,
                def: def,
                doErrorMask: true,
                errorMask: maskGetter,
                copyMask: copyMask,
                cmds: cmds);
            errorMask = retErrorMask;
        }

        public static void CopyFieldsFrom(
            this ITestObject_Notifying_SubClass item,
            ITestObject_Notifying_SubClassGetter rhs,
            ITestObject_Notifying_SubClassGetter def,
            bool doErrorMask,
            Func<TestObject_Notifying_SubClass_ErrorMask> errorMask,
            TestObject_Notifying_SubClass_CopyMask copyMask,
            NotifyingFireParameters? cmds)
        {
            TestObject_NotifyingCommon.CopyFieldsFrom(
                item,
                rhs,
                def,
                doErrorMask,
                errorMask,
                copyMask,
                cmds);
            if (copyMask?.NewField ?? true)
            {
                try
                {
                    item.NewField_Property.SetToWithDefault(
                        rhs.NewField_Property,
                        def?.NewField_Property,
                        cmds);
                }
                catch (Exception ex)
                when (doErrorMask)
                {
                    errorMask().SetNthException((ushort)TestObject_Notifying_SubClass_FieldIndex.NewField, ex);
                }
            }
        }

        #endregion

        public static void SetNthObjectHasBeenSet(
            ushort index,
            bool on,
            ITestObject_Notifying_SubClass obj,
            NotifyingFireParameters? cmds = null)
        {
            TestObject_Notifying_SubClass_FieldIndex enu = (TestObject_Notifying_SubClass_FieldIndex)index;
            switch (enu)
            {
                case TestObject_Notifying_SubClass_FieldIndex.NewField:
                    obj.NewField_Property.HasBeenSet = on;
                    break;
                default:
                    TestObject_NotifyingCommon.SetNthObjectHasBeenSet(index, on, obj);
                    break;
            }
        }

        public static void UnsetNthObject(
            ushort index,
            ITestObject_Notifying_SubClass obj,
            NotifyingUnsetParameters? cmds = null)
        {
            TestObject_Notifying_SubClass_FieldIndex enu = (TestObject_Notifying_SubClass_FieldIndex)index;
            switch (enu)
            {
                case TestObject_Notifying_SubClass_FieldIndex.NewField:
                    obj.NewField_Property.Unset(cmds);
                    break;
                default:
                    TestObject_NotifyingCommon.UnsetNthObject(index, obj);
                    break;
            }
        }

        public static bool GetNthObjectHasBeenSet(
            ushort index,
            ITestObject_Notifying_SubClass obj)
        {
            TestObject_Notifying_SubClass_FieldIndex enu = (TestObject_Notifying_SubClass_FieldIndex)index;
            switch (enu)
            {
                case TestObject_Notifying_SubClass_FieldIndex.NewField:
                    return obj.NewField_Property.HasBeenSet;
                default:
                    return TestObject_NotifyingCommon.GetNthObjectHasBeenSet(index, obj);
            }
        }

        public static object GetNthObject(
            ushort index,
            ITestObject_Notifying_SubClassGetter obj)
        {
            TestObject_Notifying_SubClass_FieldIndex enu = (TestObject_Notifying_SubClass_FieldIndex)index;
            switch (enu)
            {
                case TestObject_Notifying_SubClass_FieldIndex.NewField:
                    return obj.NewField;
                default:
                    return TestObject_NotifyingCommon.GetNthObject(index, obj);
            }
        }

        public static void Clear(
            ITestObject_Notifying_SubClass item,
            NotifyingUnsetParameters? cmds = null)
        {
            item.NewField_Property.Unset(cmds.ToUnsetParams());
        }

        public static TestObject_Notifying_SubClass_Mask<bool> GetEqualsMask(
            this ITestObject_Notifying_SubClassGetter item,
            ITestObject_Notifying_SubClassGetter rhs)
        {
            var ret = new TestObject_Notifying_SubClass_Mask<bool>();
            FillEqualsMask(item, rhs, ret);
            return ret;
        }

        public static void FillEqualsMask(
            ITestObject_Notifying_SubClassGetter item,
            ITestObject_Notifying_SubClassGetter rhs,
            TestObject_Notifying_SubClass_Mask<bool> ret)
        {
            if (rhs == null) return;
            ret.NewField = item.NewField_Property.Equals(rhs.NewField_Property, (l, r) => l == r);
            TestObject_NotifyingCommon.FillEqualsMask(item, rhs, ret);
        }

        public static string ToString(
            this ITestObject_Notifying_SubClassGetter item,
            string name = null,
            TestObject_Notifying_SubClass_Mask<bool> printMask = null)
        {
            var fg = new FileGeneration();
            item.ToString(fg, name, printMask);
            return fg.ToString();
        }

        public static void ToString(
            this ITestObject_Notifying_SubClassGetter item,
            FileGeneration fg,
            string name = null,
            TestObject_Notifying_SubClass_Mask<bool> printMask = null)
        {
            if (name == null)
            {
                fg.AppendLine($"{nameof(TestObject_Notifying_SubClass)} =>");
            }
            else
            {
                fg.AppendLine($"{name} ({nameof(TestObject_Notifying_SubClass)}) =>");
            }
            fg.AppendLine("[");
            using (new DepthWrapper(fg))
            {
                if (printMask?.NewField ?? true)
                {
                    fg.AppendLine($"NewField => {item.NewField}");
                }
            }
            fg.AppendLine("]");
        }

        public static bool HasBeenSet(
            this ITestObject_Notifying_SubClassGetter item,
            TestObject_Notifying_SubClass_Mask<bool?> checkMask)
        {
            if (checkMask.NewField.HasValue && checkMask.NewField.Value != item.NewField_Property.HasBeenSet) return false;
            return true;
        }

        public static TestObject_Notifying_SubClass_Mask<bool> GetHasBeenSetMask(ITestObject_Notifying_SubClassGetter item)
        {
            var ret = new TestObject_Notifying_SubClass_Mask<bool>();
            ret.NewField = item.NewField_Property.HasBeenSet;
            return ret;
        }

        #region XML Translation
        #region XML Write
        public static void Write_XML(
            ITestObject_Notifying_SubClassGetter item,
            Stream stream)
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
                    errorMask: out TestObject_Notifying_SubClass_ErrorMask errorMask);
            }
        }

        public static void Write_XML(
            ITestObject_Notifying_SubClassGetter item,
            Stream stream,
            out TestObject_Notifying_SubClass_ErrorMask errorMask)
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

        public static void Write_XML(
            XmlWriter writer,
            string name,
            ITestObject_Notifying_SubClassGetter item,
            bool doMasks,
            out TestObject_Notifying_SubClass_ErrorMask errorMask)
        {
            TestObject_Notifying_SubClass_ErrorMask errMaskRet = null;
            Write_XML_Internal(
                writer: writer,
                name: name,
                item: item,
                doMasks: doMasks,
                errorMask: doMasks ? () => errMaskRet ?? (errMaskRet = new TestObject_Notifying_SubClass_ErrorMask()) : default(Func<TestObject_Notifying_SubClass_ErrorMask>));
            errorMask = errMaskRet;
        }

        private static void Write_XML_Internal(
            XmlWriter writer,
            string name,
            ITestObject_Notifying_SubClassGetter item,
            bool doMasks,
            Func<TestObject_Notifying_SubClass_ErrorMask> errorMask)
        {
            try
            {
                using (new ElementWrapper(writer, name ?? "Loqui.Tests.TestObject_Notifying_SubClass"))
                {
                    if (name != null)
                    {
                        writer.WriteAttributeString("type", "Loqui.Tests.TestObject_Notifying_SubClass");
                    }
                    if (item.NewField_Property.HasBeenSet)
                    {
                        Exception subMask;
                        BooleanXmlTranslation.Instance.Write(
                            writer,
                            nameof(item.NewField),
                            item.NewField,
                            doMasks: doMasks,
                            errorMask: out subMask);
                        if (doMasks && subMask != null)
                        {
                            errorMask().NewField = subMask;
                        }
                    }
                }
            }
            catch (Exception ex)
            when (doMasks)
            {
                errorMask().Overall = ex;
            }
        }
        #endregion

        #endregion

    }
    #endregion

    #region Modules

    #region Mask
    public class TestObject_Notifying_SubClass_Mask<T> : TestObject_Notifying_Mask<T>, IMask<T>, IEquatable<TestObject_Notifying_SubClass_Mask<T>>
    {
        #region Ctors
        public TestObject_Notifying_SubClass_Mask()
        {
        }

        public TestObject_Notifying_SubClass_Mask(T initialValue)
        {
            this.NewField = initialValue;
        }
        #endregion

        #region Members
        public T NewField;
        #endregion

        #region Equals
        public override bool Equals(object rhs)
        {
            if (rhs == null) return false;
            return Equals((TestObject_Notifying_SubClass_Mask<T>)rhs);
        }

        public bool Equals(TestObject_Notifying_SubClass_Mask<T> rhs)
        {
            if (!object.Equals(this.NewField, rhs.NewField)) return false;
            return true;
        }
        #endregion

        #region All Equal
        public bool AllEqual(Func<T, bool> eval)
        {
            if (!eval(this.NewField)) return false;
            return true;
        }
        #endregion

        #region Translate
        public TestObject_Notifying_SubClass_Mask<R> Translate<R>(Func<T, R> eval)
        {
            var ret = new TestObject_Notifying_SubClass_Mask<R>();
            ret.NewField = eval(this.NewField);
            return ret;
        }
        #endregion

        #region Clear Enumerables
        public void ClearEnumerables()
        {
        }
        #endregion

        #region To String
        public override string ToString()
        {
            return ToString(printMask: null);
        }

        public string ToString(TestObject_Notifying_SubClass_Mask<bool> printMask = null)
        {
            var fg = new FileGeneration();
            ToString(fg, printMask);
            return fg.ToString();
        }

        public void ToString(FileGeneration fg, TestObject_Notifying_SubClass_Mask<bool> printMask = null)
        {
            fg.AppendLine($"{nameof(TestObject_Notifying_SubClass_Mask<T>)} =>");
            fg.AppendLine("[");
            using (new DepthWrapper(fg))
            {
                if (printMask?.NewField ?? true)
                {
                    fg.AppendLine($"NewField => {NewField.ToStringSafe()}");
                }
            }
            fg.AppendLine("]");
        }
        #endregion

    }

    public class TestObject_Notifying_SubClass_ErrorMask : TestObject_Notifying_ErrorMask
    {
        #region Members
        public Exception NewField;
        #endregion

        #region IErrorMask
        public override void SetNthException(ushort index, Exception ex)
        {
            TestObject_Notifying_SubClass_FieldIndex enu = (TestObject_Notifying_SubClass_FieldIndex)index;
            switch (enu)
            {
                case TestObject_Notifying_SubClass_FieldIndex.NewField:
                    this.NewField = ex;
                    break;
                default:
                    base.SetNthException(index, ex);
                    break;
            }
        }

        public override void SetNthMask(ushort index, object obj)
        {
            TestObject_Notifying_SubClass_FieldIndex enu = (TestObject_Notifying_SubClass_FieldIndex)index;
            switch (enu)
            {
                case TestObject_Notifying_SubClass_FieldIndex.NewField:
                    this.NewField = (Exception)obj;
                    break;
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
            fg.AppendLine("TestObject_Notifying_SubClass_ErrorMask =>");
            fg.AppendLine("[");
            using (new DepthWrapper(fg))
            {
                if (NewField != null)
                {
                    fg.AppendLine($"NewField => {NewField.ToStringSafe()}");
                }
            }
            fg.AppendLine("]");
        }
        #endregion

        #region Combine
        public TestObject_Notifying_SubClass_ErrorMask Combine(TestObject_Notifying_SubClass_ErrorMask rhs)
        {
            var ret = new TestObject_Notifying_SubClass_ErrorMask();
            ret.NewField = this.NewField.Combine(rhs.NewField);
            return ret;
        }
        public static TestObject_Notifying_SubClass_ErrorMask Combine(TestObject_Notifying_SubClass_ErrorMask lhs, TestObject_Notifying_SubClass_ErrorMask rhs)
        {
            if (lhs != null && rhs != null) return lhs.Combine(rhs);
            return lhs ?? rhs;
        }
        #endregion

    }
    public class TestObject_Notifying_SubClass_CopyMask : TestObject_Notifying_CopyMask
    {
        #region Members
        public bool NewField;
        #endregion

    }
    #endregion


    #endregion

}
