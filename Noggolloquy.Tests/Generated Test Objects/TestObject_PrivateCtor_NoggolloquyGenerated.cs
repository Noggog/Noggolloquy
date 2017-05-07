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
using Noggolloquy.Tests.Internals;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using Noggog.Xml;
using Noggolloquy.Xml;

namespace Noggolloquy.Tests
{
    #region Class
    public partial class TestObject_PrivateCtor : ITestObject_PrivateCtor, INoggolloquyObjectSetter, IEquatable<TestObject_PrivateCtor>
    {
        INoggolloquyRegistration INoggolloquyObject.Registration => TestObject_PrivateCtor_Registration.Instance;
        public static TestObject_PrivateCtor_Registration Registration => TestObject_PrivateCtor_Registration.Instance;

        protected TestObject_PrivateCtor()
        {
            CustomCtor();
        }
        partial void CustomCtor();
        #region BoolN
        public Boolean? BoolN { get; set; }
        #endregion

        #region Noggolloquy Getter Interface

        protected object GetNthObject(ushort index) => TestObject_PrivateCtorCommon.GetNthObject(index, this);
        object INoggolloquyObjectGetter.GetNthObject(ushort index) => this.GetNthObject(index);

        protected bool GetNthObjectHasBeenSet(ushort index) => TestObject_PrivateCtorCommon.GetNthObjectHasBeenSet(index, this);
        bool INoggolloquyObjectGetter.GetNthObjectHasBeenSet(ushort index) => this.GetNthObjectHasBeenSet(index);

        protected void UnsetNthObject(ushort index, NotifyingUnsetParameters? cmds) => TestObject_PrivateCtorCommon.UnsetNthObject(index, this, cmds);
        void INoggolloquyObjectSetter.UnsetNthObject(ushort index, NotifyingUnsetParameters? cmds) => this.UnsetNthObject(index, cmds);

        #endregion

        #region Noggolloquy Interface
        protected void SetNthObjectHasBeenSet(ushort index, bool on)
        {
            TestObject_PrivateCtorCommon.SetNthObjectHasBeenSet(index, on, this);
        }
        void INoggolloquyObjectSetter.SetNthObjectHasBeenSet(ushort index, bool on) => this.SetNthObjectHasBeenSet(index, on);

        public void CopyFieldsFrom(
            ITestObject_PrivateCtorGetter rhs,
            TestObject_PrivateCtor_CopyMask copyMask = null,
            ITestObject_PrivateCtorGetter def = null,
            NotifyingFireParameters? cmds = null)
        {
            TestObject_PrivateCtorCommon.CopyFieldsFrom(
                item: this,
                rhs: rhs,
                def: def,
                doErrorMask: false,
                errorMask: null,
                copyMask: copyMask,
                cmds: cmds);
        }

        public void CopyFieldsFrom(
            ITestObject_PrivateCtorGetter rhs,
            out TestObject_PrivateCtor_ErrorMask errorMask,
            TestObject_PrivateCtor_CopyMask copyMask = null,
            ITestObject_PrivateCtorGetter def = null,
            NotifyingFireParameters? cmds = null)
        {
            TestObject_PrivateCtor_ErrorMask retErrorMask = null;
            Func<TestObject_PrivateCtor_ErrorMask> maskGetter = () =>
            {
                if (retErrorMask == null)
                {
                    retErrorMask = new TestObject_PrivateCtor_ErrorMask();
                }
                return retErrorMask;
            };
            TestObject_PrivateCtorCommon.CopyFieldsFrom(
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
            if (!(obj is TestObject_PrivateCtor rhs)) return false;
            return Equals(rhs);
        }

        public bool Equals(TestObject_PrivateCtor rhs)
        {
            if (!object.Equals(this.BoolN, rhs.BoolN)) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return 
            HashHelper.GetHashCode(BoolN)
            ;
        }

        #endregion


        #region XML Translation
        public static TestObject_PrivateCtor Create_XML(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return Create_XML(XElement.Parse(reader.ReadToEnd()));
            }
        }

        public static TestObject_PrivateCtor Create_XML(XElement root)
        {
            var ret = new TestObject_PrivateCtor();
            NoggXmlTranslation<TestObject_PrivateCtor, TestObject_PrivateCtor_ErrorMask>.Instance.CopyIn(
                root: root,
                item: ret,
                skipProtected: false,
                doMasks: false,
                mask: out TestObject_PrivateCtor_ErrorMask errorMask,
                cmds: null);
            return ret;
        }

        public static TestObject_PrivateCtor Create_XML(XElement root, out TestObject_PrivateCtor_ErrorMask errorMask)
        {
            var ret = new TestObject_PrivateCtor();
            NoggXmlTranslation<TestObject_PrivateCtor, TestObject_PrivateCtor_ErrorMask>.Instance.CopyIn(
                root: root,
                item: ret,
                skipProtected: false,
                doMasks: true,
                mask: out errorMask,
                cmds: null);
            return ret;
        }

        public void CopyIn_XML(XElement root, NotifyingFireParameters? cmds = null)
        {
            NoggXmlTranslation<TestObject_PrivateCtor, TestObject_PrivateCtor_ErrorMask>.Instance.CopyIn(
                root: root,
                item: this,
                skipProtected: true,
                doMasks: false,
                mask: out TestObject_PrivateCtor_ErrorMask errorMask,
                cmds: cmds);
        }

        public virtual void CopyIn_XML(XElement root, out TestObject_PrivateCtor_ErrorMask errorMask, NotifyingFireParameters? cmds = null)
        {
            NoggXmlTranslation<TestObject_PrivateCtor, TestObject_PrivateCtor_ErrorMask>.Instance.CopyIn(
                root: root,
                item: this,
                skipProtected: true,
                doMasks: true,
                mask: out errorMask,
                cmds: cmds);
        }

        public void Write_XML(Stream stream)
        {
            using (var writer = new XmlTextWriter(stream, Encoding.ASCII))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 3;
                Write_XML(writer);
            }
        }

        public void Write_XML(Stream stream, out TestObject_PrivateCtor_ErrorMask errorMask)
        {
            using (var writer = new XmlTextWriter(stream, Encoding.ASCII))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 3;
                Write_XML(writer, out errorMask);
            }
        }

        public void Write_XML(XmlWriter writer, out TestObject_PrivateCtor_ErrorMask errorMask, string name = null)
        {
            NoggXmlTranslation<TestObject_PrivateCtor, TestObject_PrivateCtor_ErrorMask>.Instance.Write(
                writer: writer,
                name: name,
                item: this,
                doMasks: true,
                mask: out errorMask);
        }

        public void Write_XML(XmlWriter writer, string name)
        {
            NoggXmlTranslation<TestObject_PrivateCtor, TestObject_PrivateCtor_ErrorMask>.Instance.Write(
                writer: writer,
                name: name,
                item: this,
                doMasks: false,
                mask: out TestObject_PrivateCtor_ErrorMask errorMask);
        }

        public void Write_XML(XmlWriter writer)
        {
            NoggXmlTranslation<TestObject_PrivateCtor, TestObject_PrivateCtor_ErrorMask>.Instance.Write(
                writer: writer,
                name: null,
                item: this,
                doMasks: false,
                mask: out TestObject_PrivateCtor_ErrorMask errorMask);
        }

        #endregion

        public TestObject_PrivateCtor Copy(
            TestObject_PrivateCtor_CopyMask copyMask = null,
            ITestObject_PrivateCtorGetter def = null)
        {
            return TestObject_PrivateCtor.Copy(
                this,
                copyMask: copyMask,
                def: def);
        }

        public static TestObject_PrivateCtor Copy(
            ITestObject_PrivateCtor item,
            TestObject_PrivateCtor_CopyMask copyMask = null,
            ITestObject_PrivateCtorGetter def = null)
        {
            TestObject_PrivateCtor ret;
            if (item.GetType().Equals(typeof(TestObject_PrivateCtor)))
            {
                ret = new TestObject_PrivateCtor();
            }
            else
            {
                ret = (TestObject_PrivateCtor)Activator.CreateInstance(item.GetType());
            }
            ret.CopyFieldsFrom(
                item,
                copyMask: copyMask,
                def: def);
            return ret;
        }

        public static CopyType Copy<CopyType>(
            CopyType item,
            TestObject_PrivateCtor_CopyMask copyMask = null,
            ITestObject_PrivateCtorGetter def = null)
            where CopyType : class, ITestObject_PrivateCtor
        {
            CopyType ret;
            if (item.GetType().Equals(typeof(TestObject_PrivateCtor)))
            {
                ret = new TestObject_PrivateCtor() as CopyType;
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

        public static TestObject_PrivateCtor Copy_ToNoggolloquy(
            ITestObject_PrivateCtorGetter item,
            TestObject_PrivateCtor_CopyMask copyMask = null,
            ITestObject_PrivateCtorGetter def = null)
        {
            var ret = new TestObject_PrivateCtor();
            ret.CopyFieldsFrom(
                item,
                copyMask: copyMask,
                def: def);
            return ret;
        }

        void INoggolloquyObjectSetter.SetNthObject(ushort index, object obj, NotifyingFireParameters? cmds) => this.SetNthObject(index, obj, cmds);
        protected void SetNthObject(ushort index, object obj, NotifyingFireParameters? cmds = null)
        {
            TestObject_PrivateCtor_FieldIndex enu = (TestObject_PrivateCtor_FieldIndex)index;
            switch (enu)
            {
                case TestObject_PrivateCtor_FieldIndex.BoolN:
                    this.BoolN = (Boolean?)obj;
                    break;
                default:
                    throw new ArgumentException($"Index is out of range: {index}");
            }
        }

        partial void ClearPartial(NotifyingUnsetParameters? cmds);

        protected void CallClearPartial_Internal(NotifyingUnsetParameters? cmds)
        {
            ClearPartial(cmds);
        }

        public void Clear(NotifyingUnsetParameters? cmds = null)
        {
            CallClearPartial_Internal(cmds);
            TestObject_PrivateCtorCommon.Clear(this, cmds);
        }

        public static TestObject_PrivateCtor Create(IEnumerable<KeyValuePair<ushort, object>> fields)
        {
            var ret = new TestObject_PrivateCtor();
            INoggolloquyObjectExt.CopyFieldsIn(ret, fields, def: null, skipProtected: false, cmds: null);
            return ret;
        }

        public static void CopyIn(IEnumerable<KeyValuePair<ushort, object>> fields, TestObject_PrivateCtor obj)
        {
            INoggolloquyObjectExt.CopyFieldsIn(obj, fields, def: null, skipProtected: false, cmds: null);
        }

    }
    #endregion

    #region Interface
    public interface ITestObject_PrivateCtor : ITestObject_PrivateCtorGetter, INoggolloquyClass<ITestObject_PrivateCtor, ITestObject_PrivateCtorGetter>, INoggolloquyClass<TestObject_PrivateCtor, ITestObject_PrivateCtorGetter>
    {
        new Boolean? BoolN { get; set; }

    }

    public interface ITestObject_PrivateCtorGetter : INoggolloquyObject
    {
        #region BoolN
        Boolean? BoolN { get; }

        #endregion

    }

    #endregion

}

namespace Noggolloquy.Tests.Internals
{
    #region Field Index
    public enum TestObject_PrivateCtor_FieldIndex
    {
        BoolN = 0,
    }
    #endregion

    #region Registration
    public class TestObject_PrivateCtor_Registration : INoggolloquyRegistration
    {
        public static readonly TestObject_PrivateCtor_Registration Instance = new TestObject_PrivateCtor_Registration();

        public static ProtocolDefinition ProtocolDefinition => ProtocolDefinition_NoggolloquyTests.Definition;

        public static readonly ObjectKey ObjectKey = new ObjectKey(
            protocolKey: ProtocolDefinition_NoggolloquyTests.ProtocolKey,
            msgID: 15,
            version: 0);

        public const string GUID = "0df474a0-c6b4-4df1-9b98-311fbbe8414d";

        public const ushort FieldCount = 1;

        public static readonly Type MaskType = typeof(TestObject_PrivateCtor_Mask<>);

        public static readonly Type ErrorMaskType = typeof(TestObject_PrivateCtor_ErrorMask);

        public static readonly Type ClassType = typeof(TestObject_PrivateCtor);

        public const string FullName = "Noggolloquy.Tests.TestObject_PrivateCtor";

        public const string Name = "TestObject_PrivateCtor";

        public const byte GenericCount = 0;

        public static readonly Type GenericRegistrationType = null;

        public static ushort? GetNameIndex(StringCaseAgnostic str)
        {
            switch (str.Upper)
            {
                case "BOOLN":
                    return (ushort)TestObject_PrivateCtor_FieldIndex.BoolN;
                default:
                    return null;
            }
        }

        public static bool GetNthIsEnumerable(ushort index)
        {
            TestObject_PrivateCtor_FieldIndex enu = (TestObject_PrivateCtor_FieldIndex)index;
            switch (enu)
            {
                case TestObject_PrivateCtor_FieldIndex.BoolN:
                    return false;
                default:
                    throw new ArgumentException($"Index is out of range: {index}");
            }
        }

        public static bool GetNthIsNoggolloquy(ushort index)
        {
            TestObject_PrivateCtor_FieldIndex enu = (TestObject_PrivateCtor_FieldIndex)index;
            switch (enu)
            {
                case TestObject_PrivateCtor_FieldIndex.BoolN:
                    return false;
                default:
                    throw new ArgumentException($"Index is out of range: {index}");
            }
        }

        public static bool GetNthIsSingleton(ushort index)
        {
            TestObject_PrivateCtor_FieldIndex enu = (TestObject_PrivateCtor_FieldIndex)index;
            switch (enu)
            {
                case TestObject_PrivateCtor_FieldIndex.BoolN:
                    return false;
                default:
                    throw new ArgumentException($"Index is out of range: {index}");
            }
        }

        public static string GetNthName(ushort index)
        {
            TestObject_PrivateCtor_FieldIndex enu = (TestObject_PrivateCtor_FieldIndex)index;
            switch (enu)
            {
                case TestObject_PrivateCtor_FieldIndex.BoolN:
                    return "BoolN";
                default:
                    throw new ArgumentException($"Index is out of range: {index}");
            }
        }

        public static bool IsNthDerivative(ushort index)
        {
            TestObject_PrivateCtor_FieldIndex enu = (TestObject_PrivateCtor_FieldIndex)index;
            switch (enu)
            {
                case TestObject_PrivateCtor_FieldIndex.BoolN:
                    return false;
                default:
                    throw new ArgumentException($"Index is out of range: {index}");
            }
        }

        public static bool IsProtected(ushort index)
        {
            TestObject_PrivateCtor_FieldIndex enu = (TestObject_PrivateCtor_FieldIndex)index;
            switch (enu)
            {
                case TestObject_PrivateCtor_FieldIndex.BoolN:
                    return false;
                default:
                    throw new ArgumentException($"Index is out of range: {index}");
            }
        }

        public static Type GetNthType(ushort index)
        {
            TestObject_PrivateCtor_FieldIndex enu = (TestObject_PrivateCtor_FieldIndex)index;
            switch (enu)
            {
                case TestObject_PrivateCtor_FieldIndex.BoolN:
                    return typeof(Boolean?);
                default:
                    throw new ArgumentException($"Index is out of range: {index}");
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
        bool INoggolloquyRegistration.IsProtected(ushort index) => IsProtected(index);
        Type INoggolloquyRegistration.GetNthType(ushort index) => GetNthType(index);
        #endregion

    }
    #endregion

    #region Extensions
    public static class TestObject_PrivateCtorCommon
    {
        #region Copy Fields From
        public static void CopyFieldsFrom(
            this ITestObject_PrivateCtor item,
            ITestObject_PrivateCtorGetter rhs,
            ITestObject_PrivateCtorGetter def,
            bool doErrorMask,
            Func<TestObject_PrivateCtor_ErrorMask> errorMask,
            TestObject_PrivateCtor_CopyMask copyMask,
            NotifyingFireParameters? cmds)
        {
            if (copyMask?.BoolN ?? true)
            {
                item.BoolN = rhs.BoolN;
            }
        }

        #endregion

        public static void SetNthObjectHasBeenSet(
            ushort index,
            bool on,
            ITestObject_PrivateCtor obj,
            NotifyingFireParameters? cmds = null)
        {
            TestObject_PrivateCtor_FieldIndex enu = (TestObject_PrivateCtor_FieldIndex)index;
            switch (enu)
            {
                case TestObject_PrivateCtor_FieldIndex.BoolN:
                    break;
                default:
                    throw new ArgumentException($"Index is out of range: {index}");
            }
        }

        public static void UnsetNthObject(
            ushort index,
            ITestObject_PrivateCtor obj,
            NotifyingUnsetParameters? cmds = null)
        {
            TestObject_PrivateCtor_FieldIndex enu = (TestObject_PrivateCtor_FieldIndex)index;
            switch (enu)
            {
                case TestObject_PrivateCtor_FieldIndex.BoolN:
                    obj.BoolN = default(Boolean?);
                    break;
                default:
                    throw new ArgumentException($"Index is out of range: {index}");
            }
        }

        public static bool GetNthObjectHasBeenSet(
            ushort index,
            ITestObject_PrivateCtor obj)
        {
            TestObject_PrivateCtor_FieldIndex enu = (TestObject_PrivateCtor_FieldIndex)index;
            switch (enu)
            {
                case TestObject_PrivateCtor_FieldIndex.BoolN:
                    return true;
                default:
                    throw new ArgumentException($"Index is out of range: {index}");
            }
        }

        public static object GetNthObject(
            ushort index,
            ITestObject_PrivateCtorGetter obj)
        {
            TestObject_PrivateCtor_FieldIndex enu = (TestObject_PrivateCtor_FieldIndex)index;
            switch (enu)
            {
                case TestObject_PrivateCtor_FieldIndex.BoolN:
                    return obj.BoolN;
                default:
                    throw new ArgumentException($"Index is out of range: {index}");
            }
        }

        public static void Clear(
            ITestObject_PrivateCtor item,
            NotifyingUnsetParameters? cmds = null)
        {
            item.BoolN = default(Boolean?);
        }
    }
    #endregion

    #region Modules

    #region Mask
    public class TestObject_PrivateCtor_Mask<T> 
    {
        public T BoolN;
    }

    public class TestObject_PrivateCtor_ErrorMask : IErrorMask
    {
        public Exception Overall { get; set; }
        private List<string> _warnings;
        public List<string> Warnings
        {
            get
            {
                if (_warnings == null)
                {
                    _warnings = new List<string>();
                }
                return _warnings;
            }
        }
        public Exception BoolN;

        public void SetNthException(ushort index, Exception ex)
        {
            TestObject_PrivateCtor_FieldIndex enu = (TestObject_PrivateCtor_FieldIndex)index;
            switch (enu)
            {
                case TestObject_PrivateCtor_FieldIndex.BoolN:
                    this.BoolN = ex;
                    break;
                default:
                    throw new ArgumentException($"Index is out of range: {index}");
            }
        }

        public void SetNthMask(ushort index, object obj)
        {
            TestObject_PrivateCtor_FieldIndex enu = (TestObject_PrivateCtor_FieldIndex)index;
            switch (enu)
            {
                case TestObject_PrivateCtor_FieldIndex.BoolN:
                    this.BoolN = (Exception)obj;
                    break;
                default:
                    throw new ArgumentException($"Index is out of range: {index}");
            }
        }

    }
    public class TestObject_PrivateCtor_CopyMask
    {
        public bool BoolN;

    }
    #endregion


    #endregion

}
