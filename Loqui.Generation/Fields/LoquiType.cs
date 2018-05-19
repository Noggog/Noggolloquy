﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Loqui.Generation
{
    public class LoquiType : PrimitiveType
    {
        public override string TypeName
        {
            get
            {
                switch (RefType)
                {
                    case LoquiRefType.Direct:
                        switch (this.InterfaceType)
                        {
                            case LoquiInterfaceType.Direct:
                                return DirectTypeName;
                            case LoquiInterfaceType.IGetter:
                                return $"{this.Getter_InterfaceStr}{this.GenericTypes}";
                            case LoquiInterfaceType.ISetter:
                                return $"{this.InterfaceStr}{this.GenericTypes}";
                            default:
                                throw new NotImplementedException();
                        }
                    case LoquiRefType.Generic:
                        return _generic;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public string DirectTypeName => $"{this._TargetObjectGeneration.Name}{this.GenericTypes}";

        public override string ProtectedName
        {
            get
            {
                if (this.SingletonType == SingletonLevel.Singleton)
                {
                    return SingletonObjectName;
                }
                else
                {
                    return base.ProtectedName;
                }
            }
        }

        public string ObjectTypeName
        {
            get
            {
                switch (RefType)
                {
                    case LoquiRefType.Direct:
                        return this._TargetObjectGeneration.Name;
                    case LoquiRefType.Generic:
                        return _generic;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public string Getter_InterfaceStr
        {
            get
            {
                switch (RefType)
                {
                    case LoquiRefType.Direct:
                        return this._TargetObjectGeneration.Getter_InterfaceStr_NoGenerics + GenericTypes;
                    case LoquiRefType.Generic:
                        return _generic;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public string InterfaceStr
        {
            get
            {
                switch (RefType)
                {
                    case LoquiRefType.Direct:
                        return this._TargetObjectGeneration.Setter_InterfaceStr_NoGenerics + GenericTypes;
                    case LoquiRefType.Generic:
                        return _generic;
                    default:
                        throw new NotImplementedException();
                }
            }
        }
        public string GenericTypes => GetGenericTypes(MaskType.Normal);
        public SingletonLevel SingletonType;
        public LoquiRefType RefType { get; private set; }
        public LoquiInterfaceType InterfaceType = LoquiInterfaceType.Direct;
        protected string _generic;
        public GenericDefinition GenericDef;
        public GenericSpecification GenericSpecification;
        protected ObjectGeneration _TargetObjectGeneration;
        public ObjectGeneration TargetObjectGeneration
        {
            get
            {
                switch (RefType)
                {
                    case LoquiRefType.Direct:
                        return _TargetObjectGeneration;
                    case LoquiRefType.Generic:
                        return this.GenericDef.BaseObjectGeneration;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        public string SingletonObjectName => $"_{this.Name}_Object";
        public override Type Type => throw new NotImplementedException();
        public string RefName;
        public override bool Copy => base.Copy && !(this.InterfaceType == LoquiInterfaceType.IGetter && this.SingletonType == SingletonLevel.Singleton);

        public enum LoquiRefType
        {
            Direct,
            Generic
        }

        public override string SkipCheck(string copyMaskAccessor)
        {
            if (this.SingletonType == SingletonLevel.Singleton)
            {
                return $"{copyMaskAccessor}?.{this.Name}.Overall ?? true";
            }
            switch (this.RefType)
            {
                case LoquiRefType.Direct:
                    return $"{copyMaskAccessor}?.{this.Name}.Overall != {nameof(CopyOption)}.{nameof(CopyOption.Skip)}";
                case LoquiRefType.Generic:
                    if (this.TargetObjectGeneration == null)
                    {
                        return $"{copyMaskAccessor}?.{this.Name} != {nameof(GetterCopyOption)}.{nameof(GetterCopyOption.Skip)}";
                    }
                    else
                    {
                        return $"{copyMaskAccessor}?.{this.Name} != {nameof(CopyOption)}.{nameof(CopyOption.Skip)}";
                    }
                default:
                    throw new NotImplementedException();
            }
        }

        public virtual string GetMaskString(string str)
        {
            return this.TargetObjectGeneration?.GetMaskString(str) ?? $"IMask<{str}>";
        }

        public override string EqualsMaskAccessor(string accessor) => $"{accessor}.Overall";

        public string Mask(MaskType type)
        {
            if (this.GenericDef != null)
            {
                switch (type)
                {
                    case MaskType.Error:
                        return $"{GenericDef.Name}_{MaskModule.ErrMaskNickname}";
                    case MaskType.Copy:
                        return $"{GenericDef.Name}_{MaskModule.CopyMaskNickname}";
                    case MaskType.Normal:
                    default:
                        throw new NotImplementedException();
                }
            }
            else if (this.GenericSpecification != null)
            {
                return this.TargetObjectGeneration.Mask_Specified(type, this.GenericSpecification);
            }
            else if (this.TargetObjectGeneration != null)
            {
                return this.TargetObjectGeneration.Mask(type);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public override bool CopyNeedsTryCatch => true;

        public override void GenerateForCtor(FileGeneration fg)
        {
            base.GenerateForCtor(fg);

            if (!this.Bare)
            {
                if ((!this.TrueReadOnly
                    && this.RaisePropertyChanged)
                    || (!this.Bare
                        && this.SingletonType == SingletonLevel.Singleton))
                {
                    GenerateNotifyingConstruction(fg, $"_{this.Name}");
                }
            }
        }

        public override void GenerateForClass(FileGeneration fg)
        {
            if (this.Notifying == NotifyingType.NotifyingItem)
            {
                if (this.HasBeenSet)
                {
                    switch (this.SingletonType)
                    {
                        case SingletonLevel.None:
                            fg.AppendLine($"[DebuggerBrowsable(DebuggerBrowsableState.Never)]");
                            fg.AppendLine($"private readonly INotifyingSetItem<{TypeName}> {this.ProtectedProperty} = new NotifyingSetItem<{TypeName}>();");
                            break;
                        case SingletonLevel.NotNull:
                            fg.AppendLine($"[DebuggerBrowsable(DebuggerBrowsableState.Never)]");
                            fg.AppendLine($"private readonly INotifyingSetItem<{TypeName}> {this.ProtectedProperty} = new NotifyingSetItemConvertWrapper<{TypeName}>(");
                            using (new DepthWrapper(fg))
                            {
                                fg.AppendLine($"defaultVal: new {this._TargetObjectGeneration.Name}(),");
                                fg.AppendLine("incomingConverter: (change) =>");
                                using (new BraceWrapper(fg))
                                {
                                    fg.AppendLine("if (change.New == null)");
                                    using (new BraceWrapper(fg))
                                    {
                                        fg.AppendLine($"return TryGet<{this.TypeName}>.Succeed(new {this._TargetObjectGeneration.Name}());");
                                    }
                                    fg.AppendLine($"return TryGet<{this.TypeName}>.Succeed(change.New);");
                                }
                            }
                            fg.AppendLine(");");
                            break;
                        case SingletonLevel.Singleton:
                            fg.AppendLine($"[DebuggerBrowsable(DebuggerBrowsableState.Never)]");
                            fg.AppendLine($"private {this.ObjectTypeName} {this.SingletonObjectName} = new {this.ObjectTypeName}();");
                            fg.AppendLine(GetNotifyingProperty() + ";");
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                    fg.AppendLine($"public INotifyingSetItem{((ReadOnly || this.SingletonType == SingletonLevel.Singleton) ? "Getter" : string.Empty)}<{TypeName}> {this.Property} => this._{this.Name};");
                    fg.AppendLine($"[DebuggerBrowsable(DebuggerBrowsableState.Never)]");
                    fg.AppendLine($"{this.TypeName} {this.ObjectGen.Getter_InterfaceStr}.{this.Name} => this.{this.Name};");
                    fg.AppendLine($"[DebuggerBrowsable(DebuggerBrowsableState.Never)]");
                    fg.AppendLine($"public {TypeName} {this.Name} {{ get => _{this.Name}.Item; {(this.ReadOnly ? string.Empty : $"set => _{this.Name}.Item = value; ")}}}");
                    if (!this.ReadOnly && this.SingletonType != SingletonLevel.Singleton)
                    {
                        fg.AppendLine($"[DebuggerBrowsable(DebuggerBrowsableState.Never)]");
                        fg.AppendLine($"INotifyingSetItem<{this.TypeName}> {this.ObjectGen.InterfaceStr}.{this.Property} => this.{this.Property};");
                    }
                    fg.AppendLine($"[DebuggerBrowsable(DebuggerBrowsableState.Never)]");
                    fg.AppendLine($"INotifyingSetItemGetter<{this.TypeName}> {this.ObjectGen.Getter_InterfaceStr}.{this.Property} => this.{this.Property};");
                }
                else
                {
                    switch (this.SingletonType)
                    {
                        case SingletonLevel.None:
                            fg.AppendLine($"private readonly INotifyingItem<{TypeName}> {this.ProtectedProperty} = new NotifyingItem<{TypeName}>();");
                            break;
                        case SingletonLevel.NotNull:
                            fg.AppendLine($"private readonly INotifyingItem<{TypeName}> {this.ProtectedProperty} = new NotifyingItemConvertWrapper<{TypeName}>(");
                            using (new DepthWrapper(fg))
                            {
                                fg.AppendLine($"defaultVal: new {this._TargetObjectGeneration.Name}(),");
                                fg.AppendLine("incomingConverter: (change) =>");
                                using (new BraceWrapper(fg))
                                {
                                    fg.AppendLine("if (change.New == null)");
                                    using (new BraceWrapper(fg))
                                    {
                                        fg.AppendLine($"return TryGet<{this.TypeName}>.Succeed(new {this._TargetObjectGeneration.Name}());");
                                    }
                                    fg.AppendLine($"return TryGet<{this.TypeName}>.Succeed(change.New);");
                                }
                            }
                            fg.AppendLine(");");
                            break;
                        case SingletonLevel.Singleton:
                            fg.AppendLine($"private {this.ObjectTypeName} {this.SingletonObjectName} = new {this.ObjectTypeName}();");
                            fg.AppendLine(GetNotifyingProperty() + ";");
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                    fg.AppendLine($"[DebuggerBrowsable(DebuggerBrowsableState.Never)]");
                    fg.AppendLine($"public INotifyingItem{((ReadOnly || this.SingletonType == SingletonLevel.Singleton) ? "Getter" : string.Empty)}<{TypeName}> {this.Property} => this._{this.Name};");
                    fg.AppendLine($"[DebuggerBrowsable(DebuggerBrowsableState.Never)]");
                    fg.AppendLine($"{this.TypeName} {this.ObjectGen.Getter_InterfaceStr}.{this.Name} => this.{this.Name};");
                    fg.AppendLine($"public {TypeName} {this.Name} {{ get => _{this.Name}.Item; {(this.ReadOnly ? string.Empty : $"set => _{this.Name}.Item = value; ")}}}");
                    if (!this.ReadOnly && this.SingletonType != SingletonLevel.Singleton)
                    {
                        fg.AppendLine($"INotifyingItem<{this.TypeName}> {this.ObjectGen.InterfaceStr}.{this.Property} => this.{this.Property};");
                    }
                    fg.AppendLine($"INotifyingItemGetter<{this.TypeName}> {this.ObjectGen.Getter_InterfaceStr}.{this.Property} => this.{this.Property};");
                }
            }
            else if (this.Notifying == NotifyingType.ObjectCentralized)
            {
                if (HasDefault)
                {
                    throw new NotImplementedException();
                }
                if (this.SingletonType == SingletonLevel.Singleton)
                {
                    fg.AppendLine($"protected readonly {TypeName} _{this.Name} = new {this.ObjectTypeName}();");
                }
                else
                {
                    fg.AppendLine($"protected {TypeName} _{this.Name};");
                }
                fg.AppendLine($"protected PropertyForwarder<{this.ObjectGen.Name}, {TypeName}> _{this.Name}Forwarder;");
                fg.AppendLine($"public {(ReadOnly ? "INotifyingSetItemGetter" : "INotifyingSetItem")}<{TypeName}> {this.Property}");
                using (new BraceWrapper(fg))
                {
                    fg.AppendLine("get");
                    using (new BraceWrapper(fg))
                    {
                        fg.AppendLine($"if (_{this.Name}Forwarder == null) _{this.Name}Forwarder = new PropertyForwarder<{this.ObjectGen.Name}, {TypeName}>(this, (int){this.IndexEnumName});");
                        fg.AppendLine($"return _{this.Name}Forwarder;");
                    }
                }
                fg.AppendLine($"[DebuggerBrowsable(DebuggerBrowsableState.Never)]");
                fg.AppendLine($"public {this.TypeName} {this.Name}");
                using (new BraceWrapper(fg))
                {
                    fg.AppendLine($"get => this._{this.Name};");
                    fg.AppendLine($"{(ReadOnly ? "protected " : string.Empty)}set => this.Set{this.Name}(value);");
                }
                using (var args = new FunctionWrapper(fg,
                    $"protected void Set{this.Name}"))
                {
                    args.Add($"{this.TypeName} item");
                    args.Add($"bool hasBeenSet = true");
                    args.Add($"NotifyingFireParameters cmds = null");
                }
                using (new BraceWrapper(fg))
                {
                    fg.AppendLine($"if (object.Equals({this.Name}, item)) return;");
                    fg.AppendLine($"if (_{Utility.MemberNameSafety(this.TypeName)}_subscriptions != null)");
                    using (new BraceWrapper(fg))
                    {
                        fg.AppendLine($"var tmp = {this.Name};");
                        if (this.SingletonType == SingletonLevel.NotNull)
                        {
                            fg.AppendLine("if (item == null)");
                            using (new BraceWrapper(fg))
                            {
                                fg.AppendLine("item = new {this._TargetObjectGeneration.Name}();");
                            }
                        }
                        fg.AppendLine($"{this.Name} = item;");
                        using (var args = new ArgsWrapper(fg,
                            $"_{Utility.MemberNameSafety(this.TypeName)}_subscriptions.FireSubscriptions"))
                        {
                            args.Add($"index: (int){this.IndexEnumName}");
                            args.Add("hasBeenSet: _hasBeenSetTracker");
                            args.Add($"oldVal: tmp");
                            args.Add($"newVal: item");
                            args.Add($"cmds: cmds");
                        }
                    }
                    fg.AppendLine("else");
                    using (new BraceWrapper(fg))
                    {
                        fg.AppendLine($"_hasBeenSetTracker[(int){this.IndexEnumName}] = hasBeenSet;");
                        fg.AppendLine($"{this.Name} = item;");
                    }
                }
                if (!this.ReadOnly && this.SingletonType != SingletonLevel.Singleton)
                {
                    fg.AppendLine($"[DebuggerBrowsable(DebuggerBrowsableState.Never)]");
                    fg.AppendLine($"INotifyingSetItem<{this.TypeName}> {this.ObjectGen.InterfaceStr}.{this.Property} => this.{this.Property};");
                }
                fg.AppendLine($"[DebuggerBrowsable(DebuggerBrowsableState.Never)]");
                fg.AppendLine($"INotifyingSetItemGetter<{this.TypeName}> {this.ObjectGen.Getter_InterfaceStr}.{this.Property} => this.{this.Property};");
            }
            else
            {
                if (this.HasBeenSet)
                {
                    if (this.SingletonType == SingletonLevel.Singleton)
                    {
                        fg.AppendLine($"private {this.DirectTypeName} {this.SingletonObjectName} = new {this.DirectTypeName}();");
                    }
                    if (this.RaisePropertyChanged
                        || this.SingletonType == SingletonLevel.Singleton)
                    {
                        fg.AppendLine(GetNotifyingProperty() + ";");
                    }
                    else
                    {
                        GenerateNotifyingCtor(fg);
                    }
                    fg.AppendLine($"[DebuggerBrowsable(DebuggerBrowsableState.Never)]");
                    fg.AppendLine($"public {this.TypeName} {this.Name}");
                    using (new BraceWrapper(fg))
                    {
                        fg.AppendLine($"get => this.{this.ProtectedName};");
                        if (this.SingletonType != SingletonLevel.Singleton)
                        {
                            fg.AppendLine($"{(ReadOnly ? "protected " : string.Empty)}set => this.{this.ProtectedName} = value;");
                        }
                    }
                    if (this.ReadOnly)
                    {
                        fg.AppendLine($"public IHasBeenSetItemGetter<{this.TypeName}> {this.Property} => this.{this.ProtectedProperty};");
                    }
                    else
                    {
                        fg.AppendLine($"public IHasBeenSetItem<{this.TypeName}> {this.Property} => {this.ProtectedProperty};");
                    }
                    fg.AppendLine($"[DebuggerBrowsable(DebuggerBrowsableState.Never)]");
                    fg.AppendLine($"{this.TypeName} {this.ObjectGen.Getter_InterfaceStr}.{this.Name} => this.{this.ProtectedName};");
                    fg.AppendLine($"[DebuggerBrowsable(DebuggerBrowsableState.Never)]");
                    fg.AppendLine($"IHasBeenSetItemGetter<{this.TypeName}> {this.ObjectGen.Getter_InterfaceStr}.{this.Property} => this.{this.GetName(true, true)};");
                }
                else
                {
                    switch (this.SingletonType)
                    {
                        case SingletonLevel.None:
                            if (this.RaisePropertyChanged)
                            {
                                fg.AppendLine($"[DebuggerBrowsable(DebuggerBrowsableState.Never)]");
                                fg.AppendLine($"private {TypeName} _{this.Name};");
                                fg.AppendLine($"public {TypeName} {this.Name}");
                                using (new BraceWrapper(fg))
                                {
                                    fg.AppendLine($"get => _{this.Name};");
                                    fg.AppendLine($"{(this.ReadOnly ? "protected " : string.Empty)}set {{ this._{this.Name} = value; OnPropertyChanged(nameof({this.Name})); }}");
                                }
                            }
                            else
                            {
                                fg.AppendLine($"public {this.TypeName} {this.Name} {{ get; {(this.ReadOnly ? "protected " : string.Empty)}set; }}");
                            }
                            break;
                        case SingletonLevel.NotNull:
                            fg.AppendLine($"[DebuggerBrowsable(DebuggerBrowsableState.Never)]");
                            fg.AppendLine($"private {this.TypeName} _{this.Name} = new {this.DirectTypeName}();");
                            fg.AppendLine($"public {this.TypeName} {this.Name}");
                            using (new BraceWrapper(fg))
                            {
                                fg.AppendLine($"get => _{this.Name};");
                                fg.AppendLine($"{(this.ReadOnly ? "protected " : string.Empty)}set => _{this.Name} = value ?? new {this.DirectTypeName}();");
                            }
                            break;
                        case SingletonLevel.Singleton:
                            fg.AppendLine($"[DebuggerBrowsable(DebuggerBrowsableState.Never)]");
                            fg.AppendLine($"private {this.DirectTypeName} {this.SingletonObjectName} = new {this.DirectTypeName}();");
                            fg.AppendLine($"public {this.TypeName} {this.Name} => {this.SingletonObjectName};");
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }
        }

        protected override void GenerateNotifyingConstruction(FileGeneration fg, string prepend)
        {
            string item;
            if (this.Notifying == NotifyingType.NotifyingItem)
            {
                if (this.HasBeenSet)
                {
                    item = $"NotifyingSetItem";
                }
                else
                {
                    item = $"NotifyingItem";
                }
            }
            else
            {
                if (this.HasBeenSet)
                {
                    item = $"HasBeenSetItem";
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            using (var args = new ArgsWrapper(fg,
                $"{prepend} = {item}.Factory{(this.SingletonType == SingletonLevel.NotNull && this.InterfaceType == LoquiInterfaceType.Direct ? "NoNull" : string.Empty)}<{TypeName}>"))
            {
                switch (this.SingletonType)
                {
                    case SingletonLevel.None:
                    case SingletonLevel.NotNull:
                        break;
                    case SingletonLevel.Singleton:
                        args.Add($"defaultVal: {this.SingletonObjectName}");
                        break;
                    default:
                        throw new NotImplementedException();
                }
                if (this.RaisePropertyChanged)
                {
                    args.Add($"onSet: (i) => this.OnPropertyChanged(nameof({this.Name}))");
                }
                if (this.SingletonType == SingletonLevel.NotNull
                    && this.InterfaceType != LoquiInterfaceType.Direct)
                {
                    args.Add($"noNullFallback: () => new {this.ObjectTypeName}()");
                }
                if (this.HasBeenSet)
                {
                    args.Add($"markAsSet: {(this.SingletonType == SingletonLevel.Singleton ? "true" : "false")}");
                }
            }
        }

        protected virtual XElement GetRefNode(XElement node)
        {
            if (node.Name.LocalName.Equals(Constants.REF_DIRECT)
                || node.Name.LocalName.Equals(Constants.REF_LIST))
            {
                return node;
            }
            else
            {
                var ret = node.Element(XName.Get(Constants.DIRECT, LoquiGenerator.Namespace));
                if (ret != null) return ret;
            }
            return node;
        }

        public override async Task Load(XElement node, bool requireName = true)
        {
            await base.Load(node, requireName);
            this.SingletonType = node.GetAttribute(Constants.SINGLETON, SingletonLevel.None);

            XElement refNode = GetRefNode(node);
            var genericNode = node.Element(XName.Get(Constants.GENERIC, LoquiGenerator.Namespace));

            if (this.RefName == null)
            {
                this.RefName = refNode?.GetAttribute(Constants.REF_NAME);
            }

            if (this.RefName != null
                && genericNode != null
                && !string.IsNullOrWhiteSpace(genericNode.Value))
            {
                throw new ArgumentException("Cannot both be generic and have specific object specified.");
            }

            var genericName = genericNode?.Value;

            if (!ParseRefNode(refNode))
            {
                if (!string.IsNullOrWhiteSpace(genericName))
                {
                    this.RefType = LoquiRefType.Generic;
                    this._generic = genericName;
                    this.GenericDef = this.ObjectGen.Generics[this._generic];
                    this.GenericDef.Loqui = true;
                    if (this.SingletonType == SingletonLevel.Singleton)
                    {
                        throw new ArgumentException("Cannot be a generic and singleton.");
                    }
                }
                else
                {
                    throw new ArgumentException("Ref type needs a target.");
                }
            }

            this.ReadOnly = this.ReadOnly || this.SingletonType == SingletonLevel.Singleton;
        }

        public bool ParseRefNode(XElement refNode)
        {
            if (string.IsNullOrWhiteSpace(this.RefName)) return false;

            this.InterfaceType = refNode.GetAttribute<LoquiInterfaceType>(Constants.INTERFACE_TYPE, this.ObjectGen.InterfaceTypeDefault);

            this.RefType = LoquiRefType.Direct;
            if (!ObjectNamedKey.TryFactory(this.RefName, this.ProtoGen.Protocol, out var namedKey)
                || !this.ProtoGen.Gen.ObjectGenerationsByObjectNameKey.TryGetValue(namedKey, out _TargetObjectGeneration))
            {
                throw new ArgumentException("Loqui type cannot be found: " + this.RefName);
            }

            this.GenericSpecification = new GenericSpecification();
            foreach (var specNode in refNode.Elements(XName.Get(Constants.GENERIC_SPECIFICATION, LoquiGenerator.Namespace)))
            {
                this.GenericSpecification.Specifications.Add(
                    specNode.Attribute(Constants.TYPE_TO_SPECIFY).Value,
                    specNode.Attribute(Constants.DEFINITION).Value);
            }
            foreach (var mapNode in refNode.Elements(XName.Get(Constants.GENERIC_MAPPING, LoquiGenerator.Namespace)))
            {
                this.GenericSpecification.Mappings.Add(
                    mapNode.Attribute(Constants.TYPE_ON_REF).Value,
                    mapNode.Attribute(Constants.TYPE_ON_OBJECT).Value);
            }
            foreach (var generic in this.TargetObjectGeneration.Generics)
            {
                if (this.GenericSpecification.Specifications.ContainsKey(generic.Key)) continue;
                if (this.GenericSpecification.Mappings.ContainsKey(generic.Key)) continue;
                this.GenericSpecification.Mappings.Add(
                    generic.Key,
                    generic.Key);
            }
            return true;
        }

        public override void GenerateForCopy(
            FileGeneration fg,
            Accessor accessor,
            string rhsAccessorPrefix,
            string copyMaskAccessor,
            string defaultFallbackAccessor,
            string cmdsAccessor,
            bool protectedMembers)
        {

            if (_TargetObjectGeneration is StructGeneration)
            {
                fg.AppendLine($"{accessor.DirectAccess} = new {this.TargetObjectGeneration.Name}({rhsAccessorPrefix}.{this.Name});");
                return;
            }

            if (this.SingletonType == SingletonLevel.Singleton)
            {
                this.GenerateCopyFieldsFrom(fg);
                return;
            }

            if (!this.HasBeenSet)
            {
                fg.AppendLine($"switch ({copyMaskAccessor}?.Overall ?? {nameof(CopyOption)}.{nameof(CopyOption.Reference)})");
                using (new BraceWrapper(fg))
                {
                    fg.AppendLine($"case {nameof(CopyOption)}.{nameof(CopyOption.Reference)}:");
                    using (new DepthWrapper(fg))
                    {
                        fg.AppendLine($"{accessor.DirectAccess} = {rhsAccessorPrefix}.{this.Name};");
                        fg.AppendLine("break;");
                    }
                    fg.AppendLine($"case {nameof(CopyOption)}.{nameof(CopyOption.CopyIn)}:");
                    if (this.InterfaceType != LoquiInterfaceType.IGetter)
                    {
                        using (new DepthWrapper(fg))
                        {
                            this.GenerateCopyFieldsFrom(fg);
                            fg.AppendLine("break;");
                        }
                    }
                    fg.AppendLine($"case {nameof(CopyOption)}.{nameof(CopyOption.MakeCopy)}:");
                    using (new DepthWrapper(fg))
                    {
                        fg.AppendLine($"if ({rhsAccessorPrefix}.{this.Name} == null)");
                        using (new BraceWrapper(fg))
                        {
                            fg.AppendLine($"{accessor.DirectAccess} = null;");
                        }
                        fg.AppendLine($"else");
                        using (new BraceWrapper(fg))
                        {
                            using (var args = new ArgsWrapper(fg,
                                $"{accessor.DirectAccess} = {this.ObjectTypeName}{this.GenericTypes}.Copy{(this.InterfaceType == LoquiInterfaceType.IGetter ? "_ToLoqui" : string.Empty)}"))
                            {
                                args.Add($"{rhsAccessorPrefix}.{this.Name}");
                                args.Add($"{copyMaskAccessor}?.Specific");
                                args.Add($"{defaultFallbackAccessor}?.{this.Name}");
                            }
                        }
                        fg.AppendLine("break;");
                    }
                    fg.AppendLine($"default:");
                    using (new DepthWrapper(fg))
                    {
                        fg.AppendLine($"throw new NotImplementedException($\"Unknown {nameof(CopyOption)} {{{copyMaskAccessor}{(this.RefType == LoquiRefType.Direct ? $"?.Overall" : this.Name)}}}. Cannot execute copy.\");");
                    }
                }
                return;
            }

            using (var args = new ArgsWrapper(fg,
                $"{accessor.PropertyAccess}.SetToWithDefault"))
            {
                args.Add($"{rhsAccessorPrefix}.{this.Property}");
                args.Add($"{defaultFallbackAccessor}?.{this.Property}");
                if (this.Notifying != NotifyingType.None)
                {
                    args.Add($"cmds");
                }
                args.Add((gen) =>
                {
                    gen.AppendLine($"(r, d) =>");
                    using (new BraceWrapper(gen))
                    {
                        if (this.RefType == LoquiRefType.Generic
                            && this.GenericDef?.BaseObjectGeneration == null)
                        {
                            gen.AppendLine($"switch ({copyMaskAccessor}{(this.RefType == LoquiRefType.Generic ? string.Empty : ".Overall")} ?? {nameof(GetterCopyOption)}.{nameof(GetterCopyOption.Reference)})");
                            using (new BraceWrapper(gen))
                            {
                                gen.AppendLine($"case {nameof(GetterCopyOption)}.{nameof(CopyOption.Reference)}:");
                                using (new DepthWrapper(gen))
                                {
                                    gen.AppendLine("return r;");
                                }
                                gen.AppendLine($"case {nameof(GetterCopyOption)}.{nameof(GetterCopyOption.MakeCopy)}:");
                                using (new DepthWrapper(gen))
                                {
                                    gen.AppendLine($"if (r == null) return default({this.TypeName});");
                                    if (this.RefType == LoquiRefType.Direct)
                                    {
                                        using (var args2 = new ArgsWrapper(gen,
                                            $"return {this.ObjectTypeName}.Copy{(this.InterfaceType == LoquiInterfaceType.IGetter ? "_ToLoqui" : string.Empty)}"))
                                        {
                                            args2.Add($"r");
                                            if (this.RefType == LoquiRefType.Direct)
                                            {
                                                args2.Add($"{copyMaskAccessor}?.Specific");
                                            }
                                            args2.Add($"def: d");
                                        }
                                    }
                                    else
                                    {
                                        gen.AppendLine($"var copyFunc = {nameof(LoquiRegistration)}.GetCopyFunc<{_generic}>();");
                                        gen.AppendLine($"return copyFunc(r, null, d);");
                                    }
                                }
                                gen.AppendLine($"default:");
                                using (new DepthWrapper(gen))
                                {
                                    gen.AppendLine($"throw new NotImplementedException($\"Unknown {nameof(GetterCopyOption)} {{{copyMaskAccessor}{(this.RefType == LoquiRefType.Direct ? $"?.Overall" : string.Empty)}}}. Cannot execute copy.\");");
                                }
                            }
                        }
                        else
                        {
                            GenerateTypicalCopyFieldsFrom(
                                gen,
                                copyMaskAccessor: copyMaskAccessor);
                        }
                    }
                });
            }
        }

        public void GenerateTypicalCopyFieldsFrom(
            FileGeneration fg,
            string copyMaskAccessor)
        {
            fg.AppendLine($"switch ({copyMaskAccessor}{(this.RefType == LoquiRefType.Generic ? string.Empty : ".Overall")} ?? {nameof(CopyOption)}.{nameof(CopyOption.Reference)})");
            using (new BraceWrapper(fg))
            {
                fg.AppendLine($"case {nameof(CopyOption)}.{nameof(CopyOption.Reference)}:");
                using (new DepthWrapper(fg))
                {
                    fg.AppendLine("return r;");
                }
                fg.AppendLine($"case {nameof(CopyOption)}.{nameof(CopyOption.CopyIn)}:");
                if (this.InterfaceType != LoquiInterfaceType.IGetter)
                {
                    using (new DepthWrapper(fg))
                    {
                        this.GenerateCopyFieldsFrom(fg);
                        fg.AppendLine("return r;");
                    }
                }
                fg.AppendLine($"case {nameof(CopyOption)}.{nameof(CopyOption.MakeCopy)}:");
                using (new DepthWrapper(fg))
                {
                    GenerateTypicalMakeCopy(fg, copyMaskAccessor);
                }
                fg.AppendLine($"default:");
                using (new DepthWrapper(fg))
                {
                    fg.AppendLine($"throw new NotImplementedException($\"Unknown {nameof(CopyOption)} {{{copyMaskAccessor}{(this.RefType == LoquiRefType.Direct ? $"?.Overall" : string.Empty)}}}. Cannot execute copy.\");");
                }
            }
        }

        public void GenerateTypicalMakeCopy(
            FileGeneration fg,
            string copyMaskAccessor)
        {
            fg.AppendLine($"if (r == null) return default({this.TypeName});");
            if (this.RefType == LoquiRefType.Direct)
            {
                using (var args2 = new ArgsWrapper(fg,
                    $"return {this.ObjectTypeName}{this.GenericTypes}.Copy{(this.InterfaceType == LoquiInterfaceType.IGetter ? "_ToLoqui" : string.Empty)}"))
                {
                    args2.Add($"r");
                    if (this.RefType == LoquiRefType.Direct)
                    {
                        args2.Add($"{copyMaskAccessor}?.Specific");
                    }
                    args2.Add($"def: d");
                }
            }
            else
            {
                fg.AppendLine($"var copyFunc = {nameof(LoquiRegistration)}.GetCopyFunc<{_generic}>();");
                fg.AppendLine($"return copyFunc(r, null, d);");
            }
        }

        private void GenerateCopyFieldsFrom(FileGeneration fg)
        {
            if (this.RefType == LoquiRefType.Direct)
            {
                using (var args = new ArgsWrapper(fg,
                    $"{this._TargetObjectGeneration?.ExtCommonName}.CopyFieldsFrom"))
                {
                    args.Add($"item: item.{this.Name}");
                    args.Add($"rhs: rhs.{this.Name}");
                    args.Add($"def: def?.{this.Name}");
                    if (this.RefType == LoquiRefType.Direct)
                    {
                        args.Add($"doMasks: doMasks");
                        args.Add((gen) =>
                        {
                            gen.AppendLine($"errorMask: (doMasks ? new Func<{this.Mask(MaskType.Error)}>(() =>");
                            using (new BraceWrapper(gen))
                            {
                                gen.AppendLine($"var baseMask = errorMask();");
                                gen.AppendLine($"var mask = new {this.Mask(MaskType.Error)}();");
                                gen.AppendLine($"baseMask.SetNthMask((int){this.IndexEnumName}, mask);");
                                gen.AppendLine($"return mask;");
                            }
                            gen.Append($") : null)");
                        });
                        args.Add($"copyMask: copyMask?.{this.Name}.Specific");
                    }
                    else
                    {
                        args.Add($"doErrorMask: false");
                        args.Add($"errorMask: null");
                        args.Add($"copyMask: null");
                    }
                    args.Add($"cmds: cmds");
                }
            }
            else
            {
                using (var args = new ArgsWrapper(fg,
                    $"ILoquiObjectExt.CopyFieldsIn"))
                {
                    args.Add("obj: r");
                    args.Add($"rhs: item.{this.Name}");
                    args.Add($"def: def == null ? default({this.TypeName}) : def.{this.Name}");
                    args.Add("skipProtected: true");
                    args.Add("cmds: cmds");
                }
            }
        }

        public override void GenerateUnsetNth(FileGeneration fg, string identifier, string cmdsAccessor)
        {
            if (this.SingletonType != SingletonLevel.Singleton)
            {
                base.GenerateUnsetNth(fg, identifier, cmdsAccessor);
                return;
            }
            if (this.InterfaceType == LoquiInterfaceType.IGetter)
            {
                fg.AppendLine($"throw new ArgumentException(\"Cannot unset a get only singleton: {this.Name}\");");
            }
            else
            {
                fg.AppendLine($"{this.TargetObjectGeneration.ExtCommonName}.Clear({identifier}.{this.Name}, cmds.ToUnsetParams());");
                fg.AppendLine("break;");
            }
        }

        public override void GenerateSetNthHasBeenSet(FileGeneration fg, string identifier, string onIdentifier)
        {
            if (this.SingletonType != SingletonLevel.Singleton)
            {
                base.GenerateSetNthHasBeenSet(fg, identifier, onIdentifier);
                return;
            }
            fg.AppendLine($"throw new ArgumentException(\"Cannot mark set status of a singleton: {this.Name}\");");
        }

        public override void GenerateForGetterInterface(FileGeneration fg)
        {
            fg.AppendLine($"{this.TypeName} {this.Name} {{ get; }}");
            switch (this.Notifying)
            {
                case NotifyingType.None:
                    if (this.HasBeenSet)
                    {
                        fg.AppendLine($"IHasBeenSetItemGetter<{TypeName}> {this.Property} {{ get; }}");
                    }
                    else
                    {
                        return;
                    }
                    break;
                case NotifyingType.NotifyingItem:
                case NotifyingType.ObjectCentralized:
                    if (this.HasBeenSet)
                    {
                        fg.AppendLine($"INotifyingSetItemGetter<{TypeName}> {this.Property} {{ get; }}");
                    }
                    else
                    {
                        fg.AppendLine($"INotifyingItemGetter<{TypeName}> {this.Property} {{ get; }}");
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
            fg.AppendLine();
        }

        public override void GenerateSetNth(FileGeneration fg, string accessorPrefix, string rhsAccessorPrefix, string cmdsAccessor, bool internalUse)
        {
            if (this.SingletonType == SingletonLevel.Singleton)
            {
                if (!internalUse && this.InterfaceType == LoquiInterfaceType.IGetter)
                {
                    fg.AppendLine($"throw new ArgumentException(\"Cannot set singleton member {this.Name}\");");
                }
                else
                {
                    fg.AppendLine($"{accessorPrefix}.{this.ProtectedName}.CopyFieldsFrom{this.GetGenericTypes(MaskType.Copy)}(rhs: {rhsAccessorPrefix});");
                    fg.AppendLine("break;");
                }
            }
            else
            {
                base.GenerateSetNth(fg, accessorPrefix, rhsAccessorPrefix, cmdsAccessor, internalUse);
            }
        }

        public override void GenerateForInterface(FileGeneration fg)
        {
            if (this.SingletonType != SingletonLevel.Singleton)
            {
                base.GenerateForInterface(fg);
            }
        }

        public override IEnumerable<string> GetRequiredNamespaces()
        {
            if (TargetObjectGeneration != null)
            {
                yield return TargetObjectGeneration.Namespace;
                yield return TargetObjectGeneration.InternalNamespace;
            }
        }

        public override void GenerateClear(FileGeneration fg, string accessorPrefix, string cmdAccessor)
        {
            if (this.SingletonType != SingletonLevel.Singleton)
            {
                base.GenerateClear(fg, accessorPrefix, cmdAccessor);
            }
        }

        public override string GenerateACopy(string rhsAccessor)
        {
            return $"{this._TargetObjectGeneration.ObjectName}.Copy({rhsAccessor})";
        }

        public string GenerateMaskString(string type)
        {
            switch (this.RefType)
            {
                case LoquiRefType.Direct:
                    return this.TargetObjectGeneration.GetMaskString(type);
                case LoquiRefType.Generic:
                    if (this.TargetObjectGeneration != null)
                    {
                        return this.TargetObjectGeneration.GetMaskString(type);
                    }
                    else
                    {
                        return $"IMask<{type}>";
                    }
                default:
                    throw new NotImplementedException();
            }
        }

        public override void GenerateForEquals(FileGeneration fg, Accessor accessor, Accessor rhsAccessor)
        {
            fg.AppendLine($"if (!object.Equals({accessor.DirectAccess}, {rhsAccessor.DirectAccess})) return false;");
        }

        public override void GenerateForEqualsMask(FileGeneration fg, Accessor accessor, Accessor rhsAccessor, string retAccessor)
        {
            if (this.HasBeenSet)
            {
                fg.AppendLine($"{retAccessor} = {accessor.PropertyOrDirectAccess}.{nameof(IHasBeenSetExt.LoquiEqualsHelper)}({rhsAccessor.PropertyOrDirectAccess}, (loqLhs, loqRhs) => loqLhs.GetEqualsMask(loqRhs));");
            }
            else
            {
                fg.AppendLine($"{retAccessor} = new MaskItem<bool, {this.GenerateMaskString("bool")}>();");
                if (this.TargetObjectGeneration == null)
                {
                    fg.AppendLine($"{retAccessor}.Overall = object.Equals({accessor.DirectAccess}, {rhsAccessor.DirectAccess});");
                }
                else
                {
                    fg.AppendLine($"{retAccessor}.Specific = {this.TargetObjectGeneration.ExtCommonName}.GetEqualsMask({accessor.DirectAccess}, {rhsAccessor.DirectAccess});");
                    fg.AppendLine($"{retAccessor}.Overall = {retAccessor}.Specific.AllEqual((b) => b);");
                }
            }
        }

        public override void GenerateToString(FileGeneration fg, string name, Accessor accessor, string fgAccessor)
        {
            fg.AppendLine($"{accessor.DirectAccess}?.ToString({fgAccessor}, \"{name}\");");
        }

        public override void GenerateForHasBeenSetCheck(FileGeneration fg, Accessor accessor, string checkMaskAccessor)
        {
            if (!this.HasBeenSet) return;
            fg.AppendLine($"if ({checkMaskAccessor}.Overall.HasValue && {checkMaskAccessor}.Overall.Value != {accessor.PropertyOrDirectAccess}.HasBeenSet) return false;");
            if (this.TargetObjectGeneration != null)
            {
                fg.AppendLine($"if ({checkMaskAccessor}.Specific != null && ({accessor.DirectAccess} == null || !{accessor.DirectAccess}.HasBeenSet({checkMaskAccessor}.Specific))) return false;");
            }
        }

        public override void GenerateForHasBeenSetMaskGetter(FileGeneration fg, Accessor accessor, string retAccessor)
        {
            if (this.TargetObjectGeneration == null)
            {
                fg.AppendLine($"{retAccessor} = new MaskItem<bool, {this.GetMaskString("bool")}>({(this.HasBeenSet ? $"{accessor.PropertyAccess}.HasBeenSet" : "true")}, null);");
            }
            else
            {
                fg.AppendLine($"{retAccessor} = new MaskItem<bool, {this.GetMaskString("bool")}>({(this.HasBeenSet ? $"{accessor.PropertyAccess}.HasBeenSet" : "true")}, {(this.TargetObjectGeneration.ExtCommonName)}.GetHasBeenSetMask({accessor.DirectAccess}));");
            }
        }

        public IEnumerable<string> GetGenericTypesEnumerable(params MaskType[] additionalMasks)
        {
            if (this.GenericSpecification == null) return null;
            if (this.TargetObjectGeneration.Generics.Count == 0) return null;
            List<string> ret = new List<string>();
            foreach (var gen in this.TargetObjectGeneration.Generics)
            {
                if (this.GenericSpecification.Specifications.TryGetValue(gen.Key, out var spec))
                {
                    if (ObjectNamedKey.TryFactory(spec, this.ObjectGen.ProtoGen.Protocol, out var namedKey)
                        && this.ObjectGen.ProtoGen.Gen.ObjectGenerationsByObjectNameKey.TryGetValue(
                            namedKey,
                            out var targetObjGen))
                    {
                        foreach (var mType in additionalMasks)
                        {
                            switch (mType)
                            {
                                case MaskType.Normal:
                                    ret.Add(targetObjGen.Name);
                                    break;
                                case MaskType.Error:
                                    ret.Add(targetObjGen.Mask(MaskType.Error));
                                    break;
                                case MaskType.Copy:
                                    ret.Add(targetObjGen.Mask(MaskType.Copy));
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        ret.Add(spec);
                    }
                }
                else if (this.GenericSpecification.Mappings.TryGetValue(gen.Key, out var mapping))
                {
                    ret.Add(mapping);
                }
                else
                {
                    throw new ArgumentException("Generic specifications were missing some needing mappings.");
                }
            }
            return ret;
        }

        public string GetGenericTypes(params MaskType[] additionalMasks)
        {
            var e = GetGenericTypesEnumerable(additionalMasks);
            if (e == null) return null;
            return $"<{string.Join(", ", e)}>";
        }

        public LoquiType Spawn(ObjectGeneration target)
        {
            switch (RefType)
            {
                case LoquiRefType.Direct:
                    break;
                case LoquiRefType.Generic:
                default:
                    throw new NotImplementedException();
            }
            var ret = new LoquiType()
            {
                _TargetObjectGeneration = target,
                RefName = target.Name,
                Name = this.Name
            };
            ret.SetObjectGeneration(this.ObjectGen, setDefaults: true);
            foreach (var custom in this.CustomData)
            {
                ret.CustomData[custom.Key] = custom.Value;
            }
            return ret;
        }

        public override bool IsNullable()
        {
            return this.SingletonType == SingletonLevel.None;
        }

        public bool TryGetSpecificationAsObject(string genName, out ObjectGeneration obj)
        {
            var specifications = this.GenericSpecification?.Specifications;
            if (specifications == null)
            {
                obj = null;
                return false;
            }

            if (!specifications.TryGetValue(genName, out var specVal))
            {
                obj = null;
                return false;
            }

            if (!ObjectNamedKey.TryFactory(specVal, out var objKey))
            {
                obj = null;
                return false;
            }

            return this.ObjectGen.ProtoGen.Gen.ObjectGenerationsByObjectNameKey.TryGetValue(
                objKey,
                out obj);
        }
    }
}
