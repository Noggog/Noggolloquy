﻿using Noggog;
using Noggolloquy.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Noggolloquy
{
    public static class NoggolloquyRegistration
    {
        static Dictionary<ObjectKey, INoggolloquyRegistration> Registers = new Dictionary<ObjectKey, INoggolloquyRegistration>();
        static Dictionary<string, INoggolloquyRegistration> NameRegisters = new Dictionary<string, INoggolloquyRegistration>();
        static Dictionary<Type, INoggolloquyRegistration> TypeRegister = new Dictionary<Type, INoggolloquyRegistration>();
        static Dictionary<Type, Type> GenericRegisters = new Dictionary<Type, Type>();
        static Dictionary<Type, Delegate> CreateFuncRegister = new Dictionary<Type, Delegate>();
        static Dictionary<Type, Delegate> CopyInFuncRegister = new Dictionary<Type, Delegate>();
        static Dictionary<string, Type> cache = new Dictionary<string, Type>();

        static NoggolloquyRegistration()
        {
            foreach (var interf in TypeExt.GetInheritingFromInterface<IProtocolRegistration>(
                loadAssemblies: false))
            {
                IProtocolRegistration regis = Activator.CreateInstance(interf) as IProtocolRegistration;
                regis.Register();
            }
        }

        public static TryGet<Type> TryGetType(string name)
        {
            if (!cache.TryGetValue(name, out Type t))
            {
                try
                {
                    TypeStringNode node = Parse(name);
                    throw new NotImplementedException();
                }
                catch
                {
                    cache[name] = null;
                    return TryGet<Type>.Failure;
                }
            }

            return TryGet<Type>.Create(t != null, t);
        }

        public static TryGet<object> Instantiate(string name, object[] args)
        {
            TryGet<Type> tGet = TryGetType(name);
            if (tGet.Failed) return tGet.BubbleFailure<object>();
            return TryGet<object>.Succeed(Activator.CreateInstance(tGet.Value, args));
        }

        class TypeStringNode
        {
            public string Name;
            public TypeStringNode[] Children;
        }
        static char[] searchChars = new char[] { '<', '>', ',' };
        static char[] search2Chars = new char[] { '>', ',' };
        private static TypeStringNode Parse(string str)
        {
            str = str.Trim();
            int openIndex = str.IndexOfAny(searchChars);
            if (openIndex == -1)
            {
                return new TypeStringNode() { Name = str };
            }

            if (str[openIndex] != '<')
            { // Opened with something weird
                throw new ArgumentException($"{str} was malformed.");
            }

            int closeIndex = str.LastIndexOf('>', openIndex);
            if (closeIndex == -1)
            { // No closing index
                throw new ArgumentException($"{str} was malformed.");
            }

            if (closeIndex != str.Length - 1)
            { // Closing index had things afterwards
                throw new ArgumentException($"{str} was malformed.");
            }

            string content = str.Substring(openIndex + 1, closeIndex - openIndex - 1);
            if (content.Length == 0)
            { // No generic content
                throw new ArgumentException($"{str} was malformed.");
            }

            string[] commaSplit = content.Split(',');

            var ret = new TypeStringNode()
            {
                Name = str.Substring(0, openIndex).Trim()
            };
            if (commaSplit.Length > 0)
            {
                ret.Children = commaSplit.Select((t) => Parse(str)).ToArray();
            }
            return ret;
        }

        private static Type Parse(TypeStringNode nodes)
        {
            string typeStr = nodes.Name;
            if (nodes.Children.Length == 0) return Type.GetType(typeStr);

            typeStr += "`" + nodes.Children.Length;
            Type mainType = Type.GetType(typeStr);
            if (mainType == null) return null;

            Type[] subTypes = nodes.Children.Select((child) => Parse(child)).ToArray();
            if (subTypes.Any((t) => t == null)) return null;

            return mainType.MakeGenericType(subTypes);
        }

        public static void Register(INoggolloquyRegistration reg)
        {
            Registers.Add(reg.ObjectKey, reg);
            NameRegisters.Add(reg.FullName, reg);
            TypeRegister.Add(reg.ClassType, reg);
            if (reg.GenericRegistrationType != null
                && !reg.GenericRegistrationType.Equals(reg.GetType()))
            {
                GenericRegisters[reg.ClassType] = reg.GenericRegistrationType;
            }
        }

        public static bool IsNoggType(Type t)
        {
            return TypeRegister.ContainsKey(t);
        }

        public static INoggolloquyRegistration GetRegister(Type t)
        {
            if (TryGetRegister(t, out var regis)) return regis;
            throw new ArgumentException("Type was not a Noggolloquy type: " + t);
        }

        public static bool TryGetRegister(Type t, out INoggolloquyRegistration regis)
        {
            if (TypeRegister.TryGetValue(t, out regis))
            {
                return regis != null;
            }
            if (!t.IsGenericType) return false;
            Type genType = t.GetGenericTypeDefinition();
            if (!GenericRegisters.TryGetValue(genType, out var genRegisterType) || genRegisterType == null) return false;
            regis = GetGenericRegistration(genRegisterType, t.GetGenericArguments());
            TypeRegister[t] = regis;
            return regis != null;
        }

        private static INoggolloquyRegistration GetGenericRegistration(Type genRegisterType, Type[] subTypes)
        {
            var customGenRegisterType = genRegisterType.MakeGenericType(subTypes);
            var instanceProp = customGenRegisterType.GetField("GenericInstance", BindingFlags.Static | BindingFlags.Public);
            return instanceProp.GetValue(null) as INoggolloquyRegistration;
        }

        public static INoggolloquyRegistration GetRegister(ObjectKey key)
        {
            if (TryGetRegister(key, out var regis)) return regis;
            throw new ArgumentException("Object Key was not a defined Noggolloquy type: " + key);
        }

        public static bool TryGetRegister(ObjectKey key, out INoggolloquyRegistration regis)
        {
            return Registers.TryGetValue(key, out regis);
        }

        public static INoggolloquyRegistration GetRegisterByFullName(string str)
        {
            if (TryGetRegisterByFullName(str, out var regis)) return regis;
            throw new ArgumentException("Full Name did not match a defined Noggolloquy type: " + str);
        }

        public static bool TryGetRegisterByFullName(string str, out INoggolloquyRegistration regis)
        {
            if (NameRegisters.TryGetValue(str, out regis))
            {
                return regis != null;
            }
            if (str == null) return false;
            int genIndex = str.IndexOf("<");
            int genEndIndex = str.LastIndexOf(">");
            if (genIndex == -1 || genEndIndex == -1) return false;
            if (!TryGetRegisterByFullName(str.Substring(0, genIndex), out var baseReg)) return false;
            var genRegisterType = baseReg.GenericRegistrationType;
            str = str.Substring(genIndex + 1, genEndIndex - genIndex - 1);
            var subTypeStrings = str.Split(',');
            var subTypes = subTypeStrings.Select((tStr) => TypeExt.FindType(tStr.Trim())).ToArray();
            if (subTypes.Any((t) => t == null))
            {
                NameRegisters[str] = null;
                return false;
            }
            regis = GetGenericRegistration(genRegisterType, subTypes);
            if (regis != null)
            {
                TypeRegister[regis.ClassType] = regis;
            }
            NameRegisters[str] = regis;
            return regis != null;
        }

        public static Func<IEnumerable<KeyValuePair<ushort, object>>, T> GetCreateFunc<T>()
        {
            var t = typeof(T);
            if (CreateFuncRegister.TryGetValue(t, out var createFunc))
            {
                return createFunc as Func<IEnumerable<KeyValuePair<ushort, object>>, T>;
            }
            var register = GetRegister(t);
            var methodInfo = t.GetMethod(
                Constants.CREATE_FUNC_NAME,
                Constants.CREATE_FUNC_PARAM_ARRAY);
            var param = Expression.Parameter(Constants.CREATE_FUNC_PARAM, "fields");
            var tArgs = new List<Type>();
            foreach (var p in methodInfo.GetParameters())
            {
                tArgs.Add(p.ParameterType);
            }
            tArgs.Add(methodInfo.ReturnType);
            var del = Expression.Lambda(
                delegateType: Expression.GetDelegateType(tArgs.ToArray()),
                body: Expression.Call(methodInfo, param),
                parameters: param).Compile();
            CreateFuncRegister[t] = del;
            return del as Func<IEnumerable<KeyValuePair<ushort, object>>, T>;
        }

        public static Action<IEnumerable<KeyValuePair<ushort, object>>, T> GetCopyInFunc<T>()
        {
            var t = typeof(T);
            if (CopyInFuncRegister.TryGetValue(t, out var createFunc))
            {
                return createFunc as Action<IEnumerable<KeyValuePair<ushort, object>>, T>;
            }
            var register = GetRegister(t);
            var methodInfo = t.GetMethod(
                Constants.COPYIN_FUNC_NAME,
                new Type[]
                {
                    Constants.CREATE_FUNC_PARAM,
                    t,
                });
            var fields = Expression.Parameter(Constants.CREATE_FUNC_PARAM, "fields");
            var obj = Expression.Parameter(t, "obj");
            var tArgs = new List<Type>();
            foreach (var p in methodInfo.GetParameters())
            {
                tArgs.Add(p.ParameterType);
            }
            tArgs.Add(methodInfo.ReturnType);
            var del = Expression.Lambda(
                delegateType: Expression.GetDelegateType(tArgs.ToArray()),
                body: Expression.Call(methodInfo, fields, obj),
                parameters: new ParameterExpression[] { fields, obj }).Compile();
            CopyInFuncRegister[t] = del;
            return del as Action<IEnumerable<KeyValuePair<ushort, object>>, T>;
        }
    }
}