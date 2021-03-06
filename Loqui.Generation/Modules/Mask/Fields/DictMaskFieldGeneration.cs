using System;

namespace Loqui.Generation
{
    public class DictMaskFieldGeneration : MaskModuleField
    {
        public static string GetMaskString(IDictType dictType, string typeStr, bool getter)
        {
            return $"MaskItem<{typeStr}, IEnumerable<{GetSubMaskString(dictType, typeStr, getter)}>?>";
        }

        public override string GetMaskTypeStr(TypeGeneration field, string typeStr)
        {
            return GetMaskString(field as IDictType, typeStr, getter: false);
        }

        public static string GetSubMaskString(IDictType dictType, string typeStr, bool getter)
        {
            LoquiType keyLoquiType = dictType.KeyTypeGen as LoquiType;
            LoquiType valueLoquiType = dictType.ValueTypeGen as LoquiType;

            switch (dictType.Mode)
            {
                case DictMode.KeyValue:
                    if (valueLoquiType != null)
                    {
                        return $"{(valueLoquiType == null ? $"({dictType.KeyTypeGen.TypeName(getter)} Key, {typeStr} Value)" : $"MaskItemIndexed<{dictType.KeyTypeGen.TypeName(getter)}, {typeStr}, {valueLoquiType.GetMaskString(typeStr)}?>")}";
                    }
                    else
                    {
                        string keyStr = $"{(keyLoquiType == null ? dictType.KeyTypeGen.TypeName(getter: true) : $"MaskItem<{typeStr}, {keyLoquiType.GetMaskString(typeStr)}?>")}";
                        string valueStr = $"{(valueLoquiType == null ? typeStr : $"MaskItem<{typeStr}, {valueLoquiType.GetMaskString(typeStr)}?>")}";
                        return $"KeyValuePair<{keyStr}, {valueStr}>";
                    }
                case DictMode.KeyedValue:
                    return $"{(valueLoquiType == null ? $"({dictType.KeyTypeGen.TypeName(getter)} Key, {typeStr} Value)" : $"MaskItemIndexed<{dictType.KeyTypeGen.TypeName(getter)}, {typeStr}, {valueLoquiType.GetMaskString(typeStr)}?>")}";
                default:
                    throw new NotImplementedException();
            };
        }

        public static string GetErrorMaskString(IDictType dictType)
        {
            return $"MaskItem<Exception?, IEnumerable<{GetSubErrorMaskString(dictType)}>?>";
        }

        public static string GetSubErrorMaskString(IDictType dictType)
        {
            LoquiType valueLoquiType = dictType.ValueTypeGen as LoquiType;

            switch (dictType.Mode)
            {
                case DictMode.KeyValue:
                    LoquiType keyLoquiType = dictType.KeyTypeGen as LoquiType;
                    if (keyLoquiType != null)
                    {
                        throw new NotImplementedException();
                    }
                    if (valueLoquiType == null)
                    {
                        return $"KeyValuePair<{dictType.KeyTypeGen.TypeName(getter: true)}, Exception?>";
                    }
                    else
                    {
                        return $"MaskItemIndexed<{dictType.KeyTypeGen.TypeName(getter: true)}, Exception?, {valueLoquiType.Mask(MaskType.Error)}?>";
                    }
                case DictMode.KeyedValue:
                    return $"{(valueLoquiType == null ? "Exception?" : $"MaskItem<Exception?, {valueLoquiType.Mask(MaskType.Error)}?>")}";
                default:
                    throw new NotImplementedException();
            }
        }

        public static string GetSubTranslationMaskString(IDictType dictType)
        {
            LoquiType keyLoquiType = dictType.KeyTypeGen as LoquiType;
            LoquiType valueLoquiType = dictType.ValueTypeGen as LoquiType;
            string keyStr = $"{(keyLoquiType == null ? "bool" : $"MaskItem<bool, {keyLoquiType.Mask(MaskType.Translation)}?>")}";
            string valueStr = $"{(valueLoquiType == null ? "bool" : $"MaskItem<bool, {valueLoquiType.Mask(MaskType.Translation)}?>")}";

            string itemStr;
            switch (dictType.Mode)
            {
                case DictMode.KeyValue:
                    itemStr = $"KeyValuePair<{keyStr}, {valueStr}>";
                    break;
                case DictMode.KeyedValue:
                    itemStr = valueStr;
                    break;
                default:
                    throw new NotImplementedException();
            }
            return itemStr;
        }

        public override void GenerateForField(FileGeneration fg, TypeGeneration field, string typeStr)
        {
            fg.AppendLine($"public {GetMaskString(field as IDictType, typeStr, getter: false)}? {field.Name};");
        }

        public override void GenerateSetException(FileGeneration fg, TypeGeneration field)
        {
            fg.AppendLine($"this.{field.Name} = new {GetErrorMaskString(field as IDictType)}(ex, null);");
        }

        public override void GenerateSetMask(FileGeneration fg, TypeGeneration field)
        {
            fg.AppendLine($"this.{field.Name} = ({GetErrorMaskString(field as IDictType)})obj;");
        }

        public override void GenerateForCopyMask(FileGeneration fg, TypeGeneration field)
        {
            DictType dictType = field as DictType;
            LoquiType keyLoquiType = dictType.KeyTypeGen as LoquiType;
            LoquiType valueLoquiType = dictType.ValueTypeGen as LoquiType;

            switch (dictType.Mode)
            {
                case DictMode.KeyValue:
                    if (keyLoquiType == null && valueLoquiType == null)
                    {
                        fg.AppendLine($"public bool {field.Name};");
                    }
                    else if (keyLoquiType != null && valueLoquiType != null)
                    {
                        fg.AppendLine($"public MaskItem<bool, KeyValuePair<({nameof(RefCopyType)} Type, {keyLoquiType.TargetObjectGeneration.Mask(MaskType.Copy)} Mask), ({nameof(RefCopyType)} Type, {valueLoquiType.TargetObjectGeneration.Mask(MaskType.Copy)} Mask)>> {field.Name};");
                    }
                    else
                    {
                        LoquiType loqui = keyLoquiType ?? valueLoquiType;
                        fg.AppendLine($"public MaskItem<bool, ({nameof(RefCopyType)} Type, {loqui.TargetObjectGeneration.Mask(MaskType.Copy)} Mask)> {field.Name};");
                    }
                    break;
                case DictMode.KeyedValue:
                    fg.AppendLine($"public MaskItem<{nameof(CopyOption)}, {valueLoquiType.Mask(MaskType.Copy)}> {field.Name};");
                    break;
                default:
                    break;
            }
        }

        public override void GenerateForTranslationMask(FileGeneration fg, TypeGeneration field)
        {
            DictType dictType = field as DictType;
            LoquiType keyLoquiType = dictType.KeyTypeGen as LoquiType;
            LoquiType valueLoquiType = dictType.ValueTypeGen as LoquiType;

            switch (dictType.Mode)
            {
                case DictMode.KeyValue:
                    if (keyLoquiType == null && valueLoquiType == null)
                    {
                        fg.AppendLine($"public bool {field.Name};");
                    }
                    else if (keyLoquiType != null && valueLoquiType != null)
                    {
                        fg.AppendLine($"public KeyValuePair<{keyLoquiType.TargetObjectGeneration.Mask(MaskType.Translation)}, {valueLoquiType.TargetObjectGeneration.Mask(MaskType.Translation)}>? {field.Name};");
                    }
                    else
                    {
                        LoquiType loqui = keyLoquiType ?? valueLoquiType;
                        fg.AppendLine($"public {loqui.TargetObjectGeneration.Mask(MaskType.Translation)}? {field.Name};");
                    }
                    break;
                case DictMode.KeyedValue:
                    fg.AppendLine($"public {valueLoquiType.Mask(MaskType.Translation)}? {field.Name};");
                    break;
                default:
                    break;
            }
        }

        public override void GenerateMaskToString(FileGeneration fg, TypeGeneration field, Accessor accessor, bool topLevel, bool printMask)
        {
            if (printMask)
            {
                fg.AppendLine($"if ({GenerateBoolMaskCheck(field, "printMask")})");
            }
            using (new BraceWrapper(fg, printMask))
            {
                fg.AppendLine($"fg.{nameof(FileGeneration.AppendLine)}(\"{field.Name} =>\");");
                fg.AppendLine($"fg.{nameof(FileGeneration.AppendLine)}(\"[\");");
                fg.AppendLine($"using (new DepthWrapper(fg))");
                using (new BraceWrapper(fg))
                {
                    DictType dictType = field as DictType;
                    var valIsLoqui = dictType.ValueTypeGen is LoquiType;

                    fg.AppendLine($"if ({accessor} != null)");
                    using (new BraceWrapper(fg))
                    {
                        fg.AppendLine($"if ({accessor}.Overall != null)");
                        using (new BraceWrapper(fg))
                        {
                            fg.AppendLine($"fg.{nameof(FileGeneration.AppendLine)}({accessor}.Overall.ToString());");
                        }
                        fg.AppendLine($"if ({accessor}.Specific != null)");
                        using (new BraceWrapper(fg))
                        {
                            fg.AppendLine($"foreach (var subItem in {accessor}{(topLevel ? ".Specific" : string.Empty)})");
                            using (new BraceWrapper(fg))
                            {
                                var keyFieldGen = this.Module.GetMaskModule(dictType.KeyTypeGen.GetType());
                                var valFieldGen = this.Module.GetMaskModule(dictType.ValueTypeGen.GetType());
                                fg.AppendLine($"fg.{nameof(FileGeneration.AppendLine)}(\"[\");");
                                fg.AppendLine($"using (new DepthWrapper(fg))");
                                using (new BraceWrapper(fg))
                                {
                                    switch (dictType.Mode)
                                    {
                                        case DictMode.KeyValue:
                                            fg.AppendLine($"fg.{nameof(FileGeneration.AppendLine)}(\"Key => [\");");
                                            fg.AppendLine($"using (new DepthWrapper(fg))");
                                            using (new BraceWrapper(fg))
                                            {
                                                keyFieldGen.GenerateMaskToString(fg, dictType.KeyTypeGen, $"subItem.{(valIsLoqui ? "Index" : "Key")}", topLevel: false, printMask: false);
                                            }
                                            fg.AppendLine($"fg.{nameof(FileGeneration.AppendLine)}(\"]\");");
                                            fg.AppendLine($"fg.{nameof(FileGeneration.AppendLine)}(\"Value => [\");");
                                            fg.AppendLine($"using (new DepthWrapper(fg))");
                                            using (new BraceWrapper(fg))
                                            {
                                                valFieldGen.GenerateMaskToString(fg, dictType.ValueTypeGen, $"subItem.{(valIsLoqui ? "Specific" : "Value")}", topLevel: false, printMask: false);
                                            }
                                            fg.AppendLine($"fg.{nameof(FileGeneration.AppendLine)}(\"]\");");
                                            break;
                                        case DictMode.KeyedValue:
                                            keyFieldGen.GenerateMaskToString(fg, dictType.KeyTypeGen, "subItem", topLevel: false, printMask: false);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                fg.AppendLine($"fg.{nameof(FileGeneration.AppendLine)}(\"]\");");
                            }
                        }
                    }
                }
                fg.AppendLine($"fg.{nameof(FileGeneration.AppendLine)}(\"]\");");
            }
        }

        public override void GenerateForAll(FileGeneration fg, TypeGeneration field, Accessor accessor, bool nullCheck, bool indexed)
        {
            DictType dictType = field as DictType;

            if (nullCheck)
            {
                fg.AppendLine($"if ({accessor.Access} != null)");
            }
            using (new BraceWrapper(fg, doIt: nullCheck))
            {
                fg.AppendLine($"if (!eval({accessor.Access}.Overall)) return false;");
                fg.AppendLine($"if ({accessor.Access}.Specific != null)");
                using (new BraceWrapper(fg))
                {
                    fg.AppendLine($"foreach (var item in {accessor.Access}.Specific)");
                    using (new BraceWrapper(fg))
                    {
                        switch (dictType.Mode)
                        {
                            case DictMode.KeyValue:
                                if (dictType.ValueTypeGen is LoquiType loquiVal)
                                {
                                    fg.AppendLine($"if (item.Specific != null)");
                                    using (new BraceWrapper(fg))
                                    {
                                        fg.AppendLine($"if (!eval(item.Overall)) return false;");
                                        fg.AppendLine($"if (!item.Specific?.All(eval) ?? false) return false;");
                                    }
                                }
                                else
                                {
                                    fg.AppendLine($"if (!eval(item.Value)) return false;");
                                }
                                break;
                            case DictMode.KeyedValue:
                                fg.AppendLine($"if (!eval(item.Overall)) return false;");
                                fg.AppendLine($"if (!item.Specific?.All(eval) ?? false) return false;");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        public override void GenerateForAny(FileGeneration fg, TypeGeneration field, Accessor accessor, bool nullCheck, bool indexed)
        {
            DictType dictType = field as DictType;

            if (nullCheck)
            {
                fg.AppendLine($"if ({accessor.Access} != null)");
            }
            using (new BraceWrapper(fg, doIt: nullCheck))
            {
                fg.AppendLine($"if (eval({accessor.Access}.Overall)) return true;");
                fg.AppendLine($"if ({accessor.Access}.Specific != null)");
                using (new BraceWrapper(fg))
                {
                    fg.AppendLine($"foreach (var item in {accessor.Access}.Specific)");
                    using (new BraceWrapper(fg))
                    {
                        switch (dictType.Mode)
                        {
                            case DictMode.KeyValue:
                                if (dictType.ValueTypeGen is LoquiType loquiVal)
                                {
                                    fg.AppendLine($"if (item.Specific != null)");
                                    using (new BraceWrapper(fg))
                                    {
                                        fg.AppendLine($"if (eval(item.Overall)) return true;");
                                        fg.AppendLine($"if (item.Specific?.Any(eval) ?? false) return true;");
                                    }
                                }
                                else
                                {
                                    fg.AppendLine($"if (eval(item.Value)) return true;");
                                }
                                break;
                            case DictMode.KeyedValue:
                                fg.AppendLine($"if (eval(item.Overall)) return true;");
                                fg.AppendLine($"if (item.Specific?.Any(eval) ?? false) return true;");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        public override void GenerateForTranslate(FileGeneration fg, TypeGeneration field, string retAccessor, string rhsAccessor, bool indexed)
        {
            DictType dictType = field as DictType;

            fg.AppendLine($"if ({field.Name} != null)");
            using (new BraceWrapper(fg))
            {
                fg.AppendLine($"{retAccessor} = new {DictMaskFieldGeneration.GetMaskString(dictType, "R", getter: false)}(eval({rhsAccessor}.Overall), default);");
                fg.AppendLine($"if ({field.Name}.Specific != null)");
                using (new BraceWrapper(fg))
                {
                    fg.AppendLine($"List<{GetSubMaskString(dictType, "R", getter: false)}> l = new List<{GetSubMaskString(dictType, "R", getter: false)}>();");
                    fg.AppendLine($"{retAccessor}.Specific = l;");
                    fg.AppendLine($"foreach (var item in {field.Name}.Specific)");
                    using (new BraceWrapper(fg))
                    {
                        switch (dictType.Mode)
                        {
                            case DictMode.KeyValue:
                                fg.AppendLine("throw new NotImplementedException();");
                                //if (dictType.ValueTypeGen is LoquiType loquiVal)
                                //{
                                //    fg.AppendLine($"MaskItem<R, {loquiVal.GenerateMaskString("R")}> valVal = default(MaskItem<R, {loquiVal.GenerateMaskString("R")}>);");
                                //    this.Module.GetMaskModule(loquiVal.GetType()).GenerateForTranslate(fg, loquiVal, "valVal", "item.Value", indexed);
                                //}
                                //else
                                //{
                                //    fg.AppendLine($"R valVal = eval(item.Value);");
                                //}
                                //fg.AppendLine($"l.Add(new {GetSubMaskString(dictType, "R", getter: false)}(item.Key, valVal));");
                                break;
                            case DictMode.KeyedValue:
                                fg.AppendLine("throw new NotImplementedException();");
                                //var loquiType = dictType.ValueTypeGen as LoquiType;
                                //fg.AppendLine($"MaskItemIndexed<{dictType.KeyTypeGen.TypeName(getter: false)}, R, {loquiType.GenerateMaskString("R")}?> mask = default(MaskItemIndexed<{dictType.KeyTypeGen.TypeName(getter: false)}, R, {loquiType.GenerateMaskString("R")}?>);");
                                //var fieldGen = this.Module.GetMaskModule(loquiType.GetType());
                                //fieldGen.GenerateForTranslate(fg, loquiType, "mask", "item", true);
                                //fg.AppendLine($"l.Add(mask);");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        public override void GenerateForErrorMaskCombine(FileGeneration fg, TypeGeneration field, string accessor, string retAccessor, string rhsAccessor)
        {
            DictType dictType = field as DictType;
            switch (dictType.Mode)
            {
                case DictMode.KeyValue:
                    string str;
                    if (dictType.ValueTypeGen is LoquiType valLoqui)
                    {
                        str = $"MaskItemIndexed<{dictType.KeyTypeGen.TypeName(getter: true)}, Exception?, {valLoqui.Mask(MaskType.Error)}?>";
                    }
                    else
                    {
                        if (dictType.KeyTypeGen is LoquiType keyLoqui)
                        {
                            throw new NotImplementedException();
                        }
                        str = $"KeyValuePair<{dictType.KeyTypeGen.TypeName(getter: true)}, Exception?>";
                    }
                    fg.AppendLine($"{retAccessor} = new MaskItem<Exception?, IEnumerable<{str}>?>(ExceptionExt.Combine({accessor}?.Overall, {rhsAccessor}?.Overall), ExceptionExt.Combine({accessor}?.Specific, {rhsAccessor}?.Specific));");
                    break;
                case DictMode.KeyedValue:
                    var loqui = dictType.ValueTypeGen as LoquiType;
                    fg.AppendLine($"{retAccessor} = new MaskItem<Exception?, IEnumerable<MaskItem<Exception?, {loqui.Mask(MaskType.Error)}?>>?>(ExceptionExt.Combine({accessor}?.Overall, {rhsAccessor}?.Overall), ExceptionExt.Combine({accessor}?.Specific, {rhsAccessor}?.Specific));");
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public override string GenerateBoolMaskCheck(TypeGeneration field, string boolMaskAccessor)
        {
            return $"{boolMaskAccessor}?.{field.Name}?.Overall ?? true";
        }

        public override void GenerateForCtor(FileGeneration fg, TypeGeneration field, string typeStr, string valueStr)
        {
            fg.AppendLine($"this.{field.Name} = new {GetMaskString(field as IDictType, typeStr, getter: false)}({valueStr}, null);");
        }

        public override string GetErrorMaskTypeStr(TypeGeneration field)
        {
            return DictMaskFieldGeneration.GetErrorMaskString(field as IDictType);
        }

        public override string GetTranslationMaskTypeStr(TypeGeneration field)
        {
            return $"MaskItem<bool, IEnumerable<{GetSubTranslationMaskString(field as IDictType)}>>";
        }

        public override void GenerateForClearEnumerable(FileGeneration fg, TypeGeneration field)
        {
            fg.AppendLine($"this.{field.Name}.Specific = null;");
        }

        public override string GenerateForTranslationMaskCrystalization(TypeGeneration field)
        {
            var dictType = field as DictType;
            if (dictType.ValueTypeGen is LoquiType loquiType)
            {
                return $"({field.Name} != null || DefaultOn, {field.Name}?.GetCrystal())";
            }
            else
            {
                return $"({field.Name}, null)";
            }
        }

        public override void GenerateForCopyMaskCtor(FileGeneration fg, TypeGeneration field, string basicValueStr, string deepCopyStr)
        {
            DictType dictType = field as DictType;
            LoquiType keyLoquiType = dictType.KeyTypeGen as LoquiType;
            LoquiType valueLoquiType = dictType.ValueTypeGen as LoquiType;

            switch (dictType.Mode)
            {
                case DictMode.KeyValue:
                    if (keyLoquiType == null && valueLoquiType == null)
                    {
                        fg.AppendLine($"this.{field.Name} = {basicValueStr};");
                    }
                    else if (keyLoquiType != null && valueLoquiType != null)
                    {
                        fg.AppendLine($"this.{field.Name} = new MaskItem<bool, KeyValuePair<({nameof(RefCopyType)} Type, {keyLoquiType.TargetObjectGeneration.Mask(MaskType.Copy)} Mask), ({nameof(RefCopyType)} Type, {valueLoquiType.TargetObjectGeneration.Mask(MaskType.Copy)} Mask)>>({basicValueStr}, default);");
                    }
                    else
                    {
                        LoquiType loqui = keyLoquiType ?? valueLoquiType;
                        fg.AppendLine($"this.{field.Name} = new MaskItem<bool, ({nameof(RefCopyType)} Type, {loqui.TargetObjectGeneration.Mask(MaskType.Copy)} Mask)>({basicValueStr}, default);");
                    }
                    break;
                case DictMode.KeyedValue:
                    fg.AppendLine($"this.{field.Name} = new MaskItem<{nameof(CopyOption)}, {valueLoquiType.Mask(MaskType.Copy)}>({deepCopyStr}, default);");
                    break;
                default:
                    break;
            }
        }

        public override void GenerateForTranslationMaskSet(FileGeneration fg, TypeGeneration field, Accessor accessor, string onAccessor)
        {
            DictType dictType = field as DictType;
            LoquiType keyLoquiType = dictType.KeyTypeGen as LoquiType;
            LoquiType valueLoquiType = dictType.ValueTypeGen as LoquiType;

            switch (dictType.Mode)
            {
                case DictMode.KeyValue:
                    if (keyLoquiType == null && valueLoquiType == null)
                    {
                        fg.AppendLine($"{accessor.Access} = {onAccessor};");
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
