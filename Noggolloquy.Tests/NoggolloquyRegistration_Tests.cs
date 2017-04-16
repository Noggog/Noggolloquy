﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Noggolloquy.Tests
{
    public class NoggolloquyRegistration_Tests
    {
        public static ObjectKey TestObjectKey = new ObjectKey(
            protocolKey: new ProtocolKey(1),
            msgID: 2,
            version: 0);

        public static ObjectKey GenericTestObjectKey = new ObjectKey(
            protocolKey: new ProtocolKey(1),
            msgID: 1,
            version: 0);

        [Fact]
        public void GetRegistration_ByType()
        {
            var registration = NoggolloquyRegistration.GetRegister(typeof(TestObject));
            Assert.NotNull(registration);
        }

        [Fact]
        public void GetRegistration_Key()
        {
            var registration = NoggolloquyRegistration.GetRegister(TestObjectKey);
            Assert.NotNull(registration);
        }

        [Fact]
        public void GetRegistration_FullName()
        {
            var registration = NoggolloquyRegistration.GetRegisterByFullName("Noggolloquy.Tests.TestObject");
            Assert.NotNull(registration);
        }

        [Fact]
        public void GetRegistration_Generic_ByType()
        {
            var registration = NoggolloquyRegistration.GetRegister(typeof(TestGenericObject<,>));
            Assert.NotNull(registration);
        }

        [Fact]
        public void GetRegistration_Generic_Key()
        {
            var registration = NoggolloquyRegistration.GetRegister(GenericTestObjectKey);
            Assert.NotNull(registration);
        }

        [Fact]
        public void GetRegistration_Generic_FullName()
        {
            var registration = NoggolloquyRegistration.GetRegisterByFullName("Noggolloquy.Tests.TestGenericObject");
            Assert.NotNull(registration);
        }

        [Fact]
        public void GetRegistration_GenericTyped_ByType()
        {
            var registration = NoggolloquyRegistration.GetRegister(typeof(TestGenericObject<bool, ObjectToRef>));
            Assert.NotNull(registration);
        }

        [Fact]
        public void GetRegistration_GenericTyped_FullName()
        {
            var registration = NoggolloquyRegistration.GetRegisterByFullName("Noggolloquy.Tests.TestGenericObject<System.Boolean, Noggolloquy.Tests.ObjectToRef>");
            Assert.NotNull(registration);
        }

        [Fact]
        public void GetCreateFunc()
        {
            var func = NoggolloquyRegistration.GetCreateFunc<TestObject>();
            Assert.NotNull(func);
            Assert.IsType(typeof(Func<IEnumerable<KeyValuePair<ushort, object>>, TestObject>), func);
        }

        [Fact]
        public void GetCreateFunc_Generic()
        {
            var func = NoggolloquyRegistration.GetCreateFunc<TestGenericObject<bool, ObjectToRef>>();
            Assert.NotNull(func);
            Assert.IsType(typeof(Func<IEnumerable<KeyValuePair<ushort, object>>, TestGenericObject<bool, ObjectToRef>>), func);
        }

        [Fact]
        public void GetCopyInFunc()
        {
            var func = NoggolloquyRegistration.GetCopyInFunc<TestObject>();
            Assert.NotNull(func);
            Assert.IsType(typeof(Action<IEnumerable<KeyValuePair<ushort, object>>, TestObject>), func);
        }

        [Fact]
        public void GetCopyInFunc_Generic()
        {
            var func = NoggolloquyRegistration.GetCopyInFunc<TestGenericObject<bool, ObjectToRef>>();
            Assert.NotNull(func);
            Assert.IsType(typeof(Action<IEnumerable<KeyValuePair<ushort, object>>, TestGenericObject<bool, ObjectToRef>>), func);
        }
    }
}