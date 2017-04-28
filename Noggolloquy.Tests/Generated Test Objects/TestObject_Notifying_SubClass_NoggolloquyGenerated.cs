/*
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Autogenerated by Noggolloquy.  Do not manually change.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Noggolloquy;
using Noggog;
using Noggog.Notifying;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using Noggog.Xml;
using Noggolloquy.Xml;

namespace Noggolloquy.Tests
{
    #region Class
    public partial class TestObject_Notifying_SubClass : TestObject_Notifying, ITestObject_Notifying_SubClass, INoggolloquyObjectSetter, IEquatable<TestObject_Notifying_SubClass>
    {
        INoggolloquyRegistration INoggolloquyObject.Registration => TestObject_Notifying_SubClass_Registration.Instance;
        public new static TestObject_Notifying_SubClass_Registration Registration => TestObject_Notifying_SubClass_Registration.Instance;

        public TestObject_Notifying_SubClass()
        {
            CustomCtor();
        }
        partial void CustomCtor();
        #region NewField
        protected readonly INotifyingItem<Boolean> _NewField = new NotifyingItem<Boolean>(
            default(Boolean),
            markAsSet: false);
        public INotifyingItem<Boolean> NewField_Property => _NewField;
        public Boolean NewField
        {
            get => this._NewField.Value;
            set => this._NewField.Set(value);
        }
        INotifyingItem<Boolean> ITestObject_Notifying_SubClass.NewField_Property => this.NewField_Property;
        INotifyingItemGetter<Boolean> ITestObject_Notifying_SubClassGetter.NewField_Property => this.NewField_Property;
        #endregion

        #region Noggolloquy Getter Interface

        protected override object GetNthObject(ushort index) => TestObject_Notifying_SubClassCommon.GetNthObject(index, this);

        protected override bool GetNthObjectHasBeenSet(ushort index) => TestObject_Notifying_SubClassCommon.GetNthObjectHasBeenSet(index, this);

        protected override void UnsetNthObject(ushort index, NotifyingUnsetParameters? cmds) => TestObject_Notifying_SubClassCommon.UnsetNthObject(index, this, cmds);

        #endregion

        #region Noggolloquy Interface
        protected override void SetNthObjectHasBeenSet(ushort index, bool on)
        {
            TestObject_Notifying_SubClassCommon.SetNthObjectHasBeenSet(index, on, this);
        }

        public void CopyFieldsFrom(
            ITestObject_Notifying_SubClassGetter rhs,
            TestObject_Notifying_SubClass_CopyMask copyMask = null,
            ITestObject_Notifying_SubClassGetter def = null,
            NotifyingFireParameters? cmds = null)
        {
            TestObject_Notifying_SubClassCommon.CopyFieldsFrom(
                item: this,
                rhs: rhs,
                def: def,
                doErrorMask: false,
                errorMask: null,
                copyMask: copyMask,
                cmds: cmds);
        }

        public void CopyFieldsFrom(
            ITestObject_Notifying_SubClassGetter rhs,
            out TestObject_Notifying_SubClass_ErrorMask errorMask,
            TestObject_Notifying_SubClass_CopyMask copyMask = null,
            ITestObject_Notifying_SubClassGetter def = null,
            NotifyingFireParameters? cmds = null)
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
            TestObject_Notifying_SubClassCommon.CopyFieldsFrom(
                item: this,
                rhs: rhs,
                def: def,
                doErrorMask: false,
                errorMask: maskGetter,
                copyMask: copyMask,
                cmds: cmds);
            errorMask = retErrorMask;
        }

        #endregion

        #region To String
        public override string ToString()
        {
            return INoggolloquyObjectExt.PrintPretty(this);
        }
        #endregion


        #region Equals and Hash
        public override bool Equals(object obj)
        {
            if (!(obj is TestObject_Notifying_SubClass rhs)) return false;
            return Equals(rhs);
        }

        public bool Equals(TestObject_Notifying_SubClass rhs)
        {
            if (!object.Equals(this.NewField, rhs.NewField)) return false;
            return base.Equals(rhs);
        }

        public override int GetHashCode()
        {
            
            return 
            HashHelper.GetHashCode(NewField)
            .CombineHashCode(base.GetHashCode())
            ;
        }

        #endregion


        #region XML Translation
        public new static TestObject_Notifying_SubClass Create_XML(XElement root)
        {
            var ret = new TestObject_Notifying_SubClass();
            NoggXmlTranslation<TestObject_Notifying_SubClass, TestObject_Notifying_SubClass_ErrorMask>.Instance.CopyIn(
                root: root,
                item: ret,
                skipReadonly: false,
                doMasks: false,
                mask: out TestObject_Notifying_SubClass_ErrorMask errorMask,
                cmds: null);
            return ret;
        }

        public static TestObject_Notifying_SubClass Create_XML(XElement root, out TestObject_Notifying_SubClass_ErrorMask errorMask)
        {
            var ret = new TestObject_Notifying_SubClass();
            NoggXmlTranslation<TestObject_Notifying_SubClass, TestObject_Notifying_SubClass_ErrorMask>.Instance.CopyIn(
                root: root,
                item: ret,
                skipReadonly: false,
                doMasks: true,
                mask: out errorMask,
                cmds: null);
            return ret;
        }

        public override void CopyIn_XML(XElement root, NotifyingFireParameters? cmds = null)
        {
            NoggXmlTranslation<TestObject_Notifying_SubClass, TestObject_Notifying_SubClass_ErrorMask>.Instance.CopyIn(
                root: root,
                item: this,
                skipReadonly: true,
                doMasks: false,
                mask: out TestObject_Notifying_SubClass_ErrorMask errorMask,
                cmds: cmds);
        }

        public virtual void CopyIn_XML(XElement root, out TestObject_Notifying_SubClass_ErrorMask errorMask, NotifyingFireParameters? cmds = null)
        {
            NoggXmlTranslation<TestObject_Notifying_SubClass, TestObject_Notifying_SubClass_ErrorMask>.Instance.CopyIn(
                root: root,
                item: this,
                skipReadonly: true,
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
            using (var writer = new XmlTextWriter(stream, Encoding.ASCII))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 3;
                Write_XML(writer, out errorMask);
            }
        }

        public void Write_XML(XmlWriter writer, out TestObject_Notifying_SubClass_ErrorMask errorMask, string name = null)
        {
            NoggXmlTranslation<TestObject_Notifying_SubClass, TestObject_Notifying_SubClass_ErrorMask>.Instance.Write(
                writer: writer,
                name: name,
                item: this,
                doMasks: true,
                mask: out errorMask);
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

        public static CopyType Copy<CopyType>(
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

        public static TestObject_Notifying_SubClass Copy_ToNoggolloquy(
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
            switch (index)
            {
                case 48:
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
            base.Clear(cmds);
            this.NewField_Property.Unset(cmds.ToUnsetParams());
        }

        public new static TestObject_Notifying_SubClass Create(IEnumerable<KeyValuePair<ushort, object>> fields)
        {
            var ret = new TestObject_Notifying_SubClass();
            INoggolloquyObjectExt.CopyFieldsIn(ret, fields, def: null, skipReadonly: false, cmds: null);
            return ret;
        }

        public static void CopyIn(IEnumerable<KeyValuePair<ushort, object>> fields, TestObject_Notifying_SubClass obj)
        {
            INoggolloquyObjectExt.CopyFieldsIn(obj, fields, def: null, skipReadonly: false, cmds: null);
        }

    }
    #endregion

    #region Interface
    public interface ITestObject_Notifying_SubClass : ITestObject_Notifying_SubClassGetter, ITestObject_Notifying, INoggolloquyClass<ITestObject_Notifying_SubClass, ITestObject_Notifying_SubClassGetter>, INoggolloquyClass<TestObject_Notifying_SubClass, ITestObject_Notifying_SubClassGetter>
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

    #region Registration
    public class TestObject_Notifying_SubClass_Registration : INoggolloquyRegistration
    {
        public static readonly TestObject_Notifying_SubClass_Registration Instance = new TestObject_Notifying_SubClass_Registration();

        public static ProtocolDefinition ProtocolDefinition => ProtocolDefinition_NoggolloquyTests.Definition;

        public static readonly ObjectKey ObjectKey = new ObjectKey(
            protocolKey: ProtocolDefinition_NoggolloquyTests.ProtocolKey,
            msgID: 4,
            version: 0);

        public const string GUID = "3c0cceee-3747-449d-ae3e-617f5c366ef7";

        public const ushort FieldCount = 1;

        public static readonly Type MaskType = typeof(TestObject_Notifying_SubClass_Mask<>);

        public static readonly Type ErrorMaskType = typeof(TestObject_Notifying_SubClass_ErrorMask);

        public static readonly Type ClassType = typeof(TestObject_Notifying_SubClass);

        public const string FullName = "Noggolloquy.Tests.TestObject_Notifying_SubClass";

        public const string Name = "TestObject_Notifying_SubClass";

        public const byte GenericCount = 0;

        public static readonly Type GenericRegistrationType = null;

        public static ushort? GetNameIndex(StringCaseAgnostic str)
        {
            switch (str.Upper)
            {
                case "NEWFIELD":
                    return 0;
                default:
                    throw new ArgumentException($"Queried unknown field: {str}");
            }
        }

        public static bool GetNthIsEnumerable(ushort index)
        {
            switch (index)
            {
                case 48:
                    return false;
                default:
                    return TestObject_Notifying_Registration.GetNthIsEnumerable(index);
            }
        }

        public static bool GetNthIsNoggolloquy(ushort index)
        {
            switch (index)
            {
                case 48:
                    return false;
                default:
                    return TestObject_Notifying_Registration.GetNthIsNoggolloquy(index);
            }
        }

        public static bool GetNthIsSingleton(ushort index)
        {
            switch (index)
            {
                case 48:
                    return false;
                default:
                    return TestObject_Notifying_Registration.GetNthIsSingleton(index);
            }
        }

        public static string GetNthName(ushort index)
        {
            switch (index)
            {
                case 48:
                    return "NewField";
                default:
                    return TestObject_Notifying_Registration.GetNthName(index);
            }
        }

        public static bool IsNthDerivative(ushort index)
        {
            switch (index)
            {
                case 48:
                    return false;
                default:
                    return TestObject_Notifying_Registration.IsNthDerivative(index);
            }
        }

        public static bool IsReadOnly(ushort index)
        {
            switch (index)
            {
                case 48:
                    return false;
                default:
                    return TestObject_Notifying_Registration.IsReadOnly(index);
            }
        }

        public static Type GetNthType(ushort index)
        {
            switch (index)
            {
                case 48:
                    return typeof(Boolean);
                default:
                    return TestObject_Notifying_Registration.GetNthType(index);
            }
        }

        #region Interface
        ProtocolDefinition INoggolloquyRegistration.ProtocolDefinition => ProtocolDefinition;
        ObjectKey INoggolloquyRegistration.ObjectKey => ObjectKey;
        string INoggolloquyRegistration.GUID => GUID;
        int INoggolloquyRegistration.FieldCount => FieldCount;
        Type INoggolloquyRegistration.MaskType => MaskType;
        Type INoggolloquyRegistration.ErrorMaskType => ErrorMaskType;
        Type INoggolloquyRegistration.ClassType => ClassType;
        string INoggolloquyRegistration.FullName => FullName;
        string INoggolloquyRegistration.Name => Name;
        byte INoggolloquyRegistration.GenericCount => GenericCount;
        Type INoggolloquyRegistration.GenericRegistrationType => GenericRegistrationType;
        ushort? INoggolloquyRegistration.GetNameIndex(StringCaseAgnostic name) => GetNameIndex(name);
        bool INoggolloquyRegistration.GetNthIsEnumerable(ushort index) => GetNthIsEnumerable(index);
        bool INoggolloquyRegistration.GetNthIsNoggolloquy(ushort index) => GetNthIsNoggolloquy(index);
        bool INoggolloquyRegistration.GetNthIsSingleton(ushort index) => GetNthIsSingleton(index);
        string INoggolloquyRegistration.GetNthName(ushort index) => GetNthName(index);
        bool INoggolloquyRegistration.IsNthDerivative(ushort index) => IsNthDerivative(index);
        bool INoggolloquyRegistration.IsReadOnly(ushort index) => IsReadOnly(index);
        Type INoggolloquyRegistration.GetNthType(ushort index) => GetNthType(index);
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
                {
                    if (doErrorMask)
                    {
                        errorMask().SetNthException(48, ex);
                    }
                    else
                    {
                        throw ex;
                    }
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
            switch (index)
            {
                case 48:
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
            switch (index)
            {
                case 48:
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
            switch (index)
            {
                case 48:
                    return obj.NewField_Property.HasBeenSet;
                default:
                    return TestObject_NotifyingCommon.GetNthObjectHasBeenSet(index, obj);
            }
        }

        public static object GetNthObject(
            ushort index,
            ITestObject_Notifying_SubClassGetter obj)
        {
            switch (index)
            {
                case 48:
                    return obj.NewField;
                default:
                    return TestObject_NotifyingCommon.GetNthObject(index, obj);
            }
        }

    }
    #endregion

    #region Modules

    #region Mask
    public class TestObject_Notifying_SubClass_Mask<T>  : TestObject_Notifying_Mask<T>
    {
        public T NewField;
    }

    public class TestObject_Notifying_SubClass_ErrorMask : TestObject_Notifying_ErrorMask
    {
        public Exception NewField;

        public override void SetNthException(ushort index, Exception ex)
        {
            switch (index)
            {
                case 48:
                    this.NewField = ex;
                    break;
                default:
                    base.SetNthException(index, ex);
                    break;
            }
        }

        public override void SetNthMask(ushort index, object obj)
        {
            switch (index)
            {
                case 48:
                    this.NewField = (Exception)obj;
                    break;
                default:
                    base.SetNthMask(index, obj);
                    break;
            }
        }
    }
    public class TestObject_Notifying_SubClass_CopyMask : TestObject_Notifying_CopyMask
    {
        public bool NewField;

    }
    #endregion


    #endregion

}
