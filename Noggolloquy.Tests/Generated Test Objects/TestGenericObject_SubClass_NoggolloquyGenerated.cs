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
    public partial class TestGenericObject_SubClass<S, T, R> : TestGenericObject<T, R>, ITestGenericObject_SubClass<S, T, R>, INoggolloquyObjectSetter, IEquatable<TestGenericObject_SubClass<S, T, R>>
        where S : ObjectToRef
        where T : INoggolloquyObject
        where R : ObjectToRef, INoggolloquyObject
    {
        INoggolloquyRegistration INoggolloquyObject.Registration => TestGenericObject_SubClass_Registration.Instance;
        public new static TestGenericObject_SubClass_Registration Registration => TestGenericObject_SubClass_Registration.Instance;

        public TestGenericObject_SubClass()
        {
            CustomCtor();
        }
        partial void CustomCtor();

        #region Noggolloquy Getter Interface

        protected override object GetNthObject(ushort index) => TestGenericObject_SubClassCommon<S, T, R>.GetNthObject(index, this);

        protected override bool GetNthObjectHasBeenSet(ushort index) => TestGenericObject_SubClassCommon<S, T, R>.GetNthObjectHasBeenSet(index, this);

        protected override void UnsetNthObject(ushort index, NotifyingUnsetParameters? cmds) => TestGenericObject_SubClassCommon<S, T, R>.UnsetNthObject(index, this, cmds);

        #endregion

        #region Noggolloquy Interface
        protected override void SetNthObjectHasBeenSet(ushort index, bool on)
        {
            TestGenericObject_SubClassCommon<S, T, R>.SetNthObjectHasBeenSet(index, on, this);
        }

        public void CopyFieldsFrom(
            ITestGenericObject_SubClassGetter<S, T, R> rhs,
            TestGenericObject_SubClass_CopyMask copyMask = null,
            ITestGenericObject_SubClassGetter<S, T, R> def = null,
            NotifyingFireParameters? cmds = null)
        {
            TestGenericObject_SubClassCommon<S, T, R>.CopyFieldsFrom(
                item: this,
                rhs: rhs,
                def: def,
                doErrorMask: false,
                errorMask: null,
                copyMask: copyMask,
                cmds: cmds);
        }

        public void CopyFieldsFrom(
            ITestGenericObject_SubClassGetter<S, T, R> rhs,
            out TestGenericObject_SubClass_ErrorMask errorMask,
            TestGenericObject_SubClass_CopyMask copyMask = null,
            ITestGenericObject_SubClassGetter<S, T, R> def = null,
            NotifyingFireParameters? cmds = null)
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
            TestGenericObject_SubClassCommon<S, T, R>.CopyFieldsFrom(
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
            if (!(obj is TestGenericObject_SubClass<S, T, R> rhs)) return false;
            return Equals(rhs);
        }

        public bool Equals(TestGenericObject_SubClass<S, T, R> rhs)
        {
            return base.Equals(rhs);
        }

        public override int GetHashCode()
        {
            return 
            base.GetHashCode()
            ;
        }

        #endregion

        #region XML Translation
        public new static TestGenericObject_SubClass<S, T, R> Create_XML(XElement root)
        {
            var ret = new TestGenericObject_SubClass<S, T, R>();
            NoggXmlTranslation<TestGenericObject_SubClass<S, T, R>, TestGenericObject_SubClass_ErrorMask>.Instance.CopyIn(
                root: root,
                item: ret,
                skipReadonly: false,
                doMasks: false,
                mask: out TestGenericObject_SubClass_ErrorMask errorMask,
                cmds: null);
            return ret;
        }

        public static TestGenericObject_SubClass<S, T, R> Create_XML(XElement root, out TestGenericObject_SubClass_ErrorMask errorMask)
        {
            var ret = new TestGenericObject_SubClass<S, T, R>();
            NoggXmlTranslation<TestGenericObject_SubClass<S, T, R>, TestGenericObject_SubClass_ErrorMask>.Instance.CopyIn(
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
            NoggXmlTranslation<TestGenericObject_SubClass<S, T, R>, TestGenericObject_SubClass_ErrorMask>.Instance.CopyIn(
                root: root,
                item: this,
                skipReadonly: true,
                doMasks: false,
                mask: out TestGenericObject_SubClass_ErrorMask errorMask,
                cmds: cmds);
        }

        public virtual void CopyIn_XML(XElement root, out TestGenericObject_SubClass_ErrorMask errorMask, NotifyingFireParameters? cmds = null)
        {
            NoggXmlTranslation<TestGenericObject_SubClass<S, T, R>, TestGenericObject_SubClass_ErrorMask>.Instance.CopyIn(
                root: root,
                item: this,
                skipReadonly: true,
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
            using (var writer = new XmlTextWriter(stream, Encoding.ASCII))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 3;
                Write_XML(writer, out errorMask);
            }
        }

        public void Write_XML(XmlWriter writer, out TestGenericObject_SubClass_ErrorMask errorMask, string name = null)
        {
            NoggXmlTranslation<TestGenericObject_SubClass<S, T, R>, TestGenericObject_SubClass_ErrorMask>.Instance.Write(
                writer: writer,
                name: name,
                item: this,
                doMasks: true,
                mask: out errorMask);
        }

        #endregion
        #region Mask
        #endregion
        public TestGenericObject_SubClass<S, T, R> Copy(ITestGenericObject_SubClassGetter<S, T, R> def = null)
        {
            return Copy(this, def: def);
        }

        public static TestGenericObject_SubClass<S, T, R> Copy(
            ITestGenericObject_SubClassGetter<S, T, R> item,
            TestGenericObject_SubClass_CopyMask copyMask = null,
            ITestGenericObject_SubClassGetter<S, T, R> def = null)
        {
            var ret = new TestGenericObject_SubClass<S, T, R>();
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
                default:
                    base.SetNthObject(index, obj, cmds);
                    break;
            }
        }

        public override void Clear(NotifyingUnsetParameters? cmds = null)
        {
            base.Clear(cmds);
        }

        public new static TestGenericObject_SubClass<S, T, R> Create(IEnumerable<KeyValuePair<ushort, object>> fields)
        {
            var ret = new TestGenericObject_SubClass<S, T, R>();
            INoggolloquyObjectExt.CopyFieldsIn(ret, fields, def: null, skipReadonly: false, cmds: null);
            return ret;
        }

        public static void CopyIn(IEnumerable<KeyValuePair<ushort, object>> fields, TestGenericObject_SubClass<S, T, R> obj)
        {
            INoggolloquyObjectExt.CopyFieldsIn(obj, fields, def: null, skipReadonly: false, cmds: null);
        }

    }
    #endregion

    #region Interface
    public interface ITestGenericObject_SubClass<S, T, R> : ITestGenericObject_SubClassGetter<S, T, R>, ITestGenericObject<T, R>, INoggolloquyClass<ITestGenericObject_SubClass<S, T, R>, ITestGenericObject_SubClassGetter<S, T, R>>, INoggolloquyClass<TestGenericObject_SubClass<S, T, R>, ITestGenericObject_SubClassGetter<S, T, R>>
        where S : ObjectToRef
        where T : INoggolloquyObject
        where R : ObjectToRef, INoggolloquyObject
    {
    }

    public interface ITestGenericObject_SubClassGetter<S, T, R> : ITestGenericObjectGetter<T, R>
        where S : ObjectToRef
        where T : INoggolloquyObject
        where R : ObjectToRef, INoggolloquyObject
    {

        #region XML Translation
        #endregion
        #region Mask
        #endregion
    }

    #endregion

    #region Registration
    public class TestGenericObject_SubClass_Registration : INoggolloquyRegistration
    {
        public static readonly TestGenericObject_SubClass_Registration Instance = new TestGenericObject_SubClass_Registration();

        public static ProtocolDefinition ProtocolDefinition => ProtocolDefinition_NoggolloquyTests.Definition;

        public static readonly ObjectKey ObjectKey = new ObjectKey(
            protocolKey: ProtocolDefinition_NoggolloquyTests.ProtocolKey,
            msgID: 6,
            version: 0);

        public const string GUID = "dfe7e700-e6c2-4772-9965-80cd875e7a21";

        public const ushort FieldCount = 0;

        public static readonly Type MaskType = typeof(TestGenericObject_SubClass_Mask<>);

        public static readonly Type ErrorMaskType = typeof(TestGenericObject_SubClass_ErrorMask);

        public static readonly Type ClassType = typeof(TestGenericObject_SubClass<,,>);

        public const string FullName = "Noggolloquy.Tests.TestGenericObject_SubClass";

        public const string Name = "TestGenericObject_SubClass";

        public const byte GenericCount = 3;

        public static readonly Type GenericRegistrationType = typeof(TestGenericObject_SubClass_Registration<,,>);

        public static ushort? GetNameIndex(StringCaseAgnostic str)
        {
            switch (str.Upper)
            {
                default:
                    throw new ArgumentException($"Queried unknown field: {str}");
            }
        }

        public static bool GetNthIsEnumerable(ushort index)
        {
            switch (index)
            {
                default:
                    return TestGenericObject_Registration.GetNthIsEnumerable(index);
            }
        }

        public static bool GetNthIsNoggolloquy(ushort index)
        {
            switch (index)
            {
                default:
                    return TestGenericObject_Registration.GetNthIsNoggolloquy(index);
            }
        }

        public static bool GetNthIsSingleton(ushort index)
        {
            switch (index)
            {
                default:
                    return TestGenericObject_Registration.GetNthIsSingleton(index);
            }
        }

        public static string GetNthName(ushort index)
        {
            switch (index)
            {
                default:
                    return TestGenericObject_Registration.GetNthName(index);
            }
        }

        public static bool IsNthDerivative(ushort index)
        {
            switch (index)
            {
                default:
                    return TestGenericObject_Registration.IsNthDerivative(index);
            }
        }

        public static bool IsReadOnly(ushort index)
        {
            switch (index)
            {
                default:
                    return TestGenericObject_Registration.IsReadOnly(index);
            }
        }

        public static Type GetNthType(ushort index) => throw new ArgumentException("Cannot get nth type for a generic object here.  Use generic registration instead.");

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

    public class TestGenericObject_SubClass_Registration<S, T, R> : TestGenericObject_SubClass_Registration
        where S : ObjectToRef
        where T : INoggolloquyObject
        where R : ObjectToRef, INoggolloquyObject
    {
        public static readonly TestGenericObject_SubClass_Registration<S, T, R> GenericInstance = new TestGenericObject_SubClass_Registration<S, T, R>();

        public new static Type GetNthType(ushort index)
        {
            switch (index)
            {
                default:
                    return TestGenericObject_Registration.GetNthType(index);
            }
        }

    }
    #endregion
    #region Extensions
    public static class TestGenericObject_SubClassCommon<S, T, R>
        where S : ObjectToRef
        where T : INoggolloquyObject
        where R : ObjectToRef, INoggolloquyObject
    {
        #region Copy Fields From
        public static void CopyFieldsFrom(
            this ITestGenericObject_SubClass<S, T, R> item,
            ITestGenericObject_SubClassGetter<S, T, R> rhs,
            ITestGenericObject_SubClassGetter<S, T, R> def,
            bool doErrorMask,
            Func<TestGenericObject_SubClass_ErrorMask> errorMask,
            TestGenericObject_SubClass_CopyMask copyMask,
            NotifyingFireParameters? cmds)
        {
            TestGenericObjectCommon<T, R>.CopyFieldsFrom(
                item,
                rhs,
                def,
                doErrorMask,
                errorMask,
                copyMask,
                cmds);
        }

        #endregion

        public static void SetNthObjectHasBeenSet(ushort index, bool on, ITestGenericObject_SubClass<S, T, R> obj, NotifyingFireParameters? cmds = null)
        {
            switch (index)
            {
                default:
                    TestGenericObjectCommon<T, R>.SetNthObjectHasBeenSet(index, on, obj);
                    break;
            }
        }

        public static void UnsetNthObject(ushort index, ITestGenericObject_SubClass<S, T, R> obj, NotifyingUnsetParameters? cmds = null)
        {
            switch (index)
            {
                default:
                    TestGenericObjectCommon<T, R>.UnsetNthObject(index, obj);
                    break;
            }
        }

        public static bool GetNthObjectHasBeenSet(ushort index, ITestGenericObject_SubClass<S, T, R> obj)
        {
            switch (index)
            {
                default:
                    return TestGenericObjectCommon<T, R>.GetNthObjectHasBeenSet(index, obj);
            }
        }

        public static object GetNthObject(ushort index, ITestGenericObject_SubClassGetter<S, T, R> obj)
        {
            switch (index)
            {
                default:
                    return TestGenericObjectCommon<T, R>.GetNthObject(index, obj);
            }
        }

    }
    #endregion

    #region Modules
    #region XML Translation
    #endregion

    #region Mask
    public class TestGenericObject_SubClass_Mask<T>  : TestGenericObject_Mask<T>
    {
    }

    public class TestGenericObject_SubClass_ErrorMask : TestGenericObject_ErrorMask
    {

        public override void SetNthException(ushort index, Exception ex)
        {
            switch (index)
            {
                default:
                    base.SetNthException(index, ex);
                    break;
            }
        }

        public override void SetNthMask(ushort index, object obj)
        {
            switch (index)
            {
                default:
                    base.SetNthMask(index, obj);
                    break;
            }
        }
    }
    public class TestGenericObject_SubClass_CopyMask : TestGenericObject_CopyMask
    {

    }
    #endregion

    #endregion

    #region Noggolloquy Interfaces
    #endregion

}