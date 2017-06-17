﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loqui.Generation
{
    public class LoquiXmlTranslationGeneration : XmlTranslationGeneration
    {
        public override void GenerateWrite(
            FileGeneration fg,
            TypeGeneration typeGen,
            string writerAccessor,
            string itemAccessor,
            string maskAccessor,
            string nameAccessor)
        {
            var loquiGen = typeGen as LoquiType;
            if (loquiGen.TargetObjectGeneration != null)
            {
                using (var args = new ArgsWrapper(fg,
                    $"{loquiGen.TargetObjectGeneration.ExtCommonName}.Write_XML"))
                {
                    args.Add($"writer: {writerAccessor}");
                    args.Add($"item: {itemAccessor}");
                    args.Add($"name: {nameAccessor}");
                    args.Add($"doMasks: doMasks");
                    args.Add($"errorMask: out {loquiGen.ErrorMaskItemString} loquiMask");
                }
                fg.AppendLine($"{maskAccessor} = loquiMask == null ? null : new MaskItem<Exception, {loquiGen.ErrorMaskItemString}>(null, loquiMask);");
            }
            else
            {
                UnsafeXmlTranslationGeneration unsafeXml = new UnsafeXmlTranslationGeneration()
                {
                    ErrMaskString = $"MaskItem<Exception, {loquiGen.ErrorMaskItemString}>"
                };
                unsafeXml.GenerateWrite(
                    fg: fg, 
                    typeGen: typeGen, 
                    writerAccessor: writerAccessor,
                    itemAccessor: itemAccessor,
                    maskAccessor: maskAccessor,
                    nameAccessor: nameAccessor);
            }
        }

        public override bool ShouldGenerateCopyIn(TypeGeneration typeGen)
        {
            var loquiGen = typeGen as LoquiType;
            return loquiGen.SingletonType != LoquiType.SingletonLevel.Singleton || loquiGen.InterfaceType != LoquiInterfaceType.IGetter;
        }

        public override void GenerateCopyIn(FileGeneration fg, TypeGeneration typeGen, string nodeAccessor, string itemAccessor, string maskAccessor)
        {
            var loquiGen = typeGen as LoquiType;
            if (loquiGen.TargetObjectGeneration != null)
            {
                if (loquiGen.SingletonType == LoquiType.SingletonLevel.Singleton)
                {
                    if (loquiGen.InterfaceType == LoquiInterfaceType.IGetter) return;
                    using (var args = new ArgsWrapper(fg,
                        $"var tmp = {loquiGen.TargetObjectGeneration.Name}.Create_XML"))
                    {
                        args.Add($"root: {nodeAccessor}");
                        args.Add($"doMasks: doMasks");
                        args.Add($"errorMask: out {loquiGen.ErrorMaskItemString} createMask");
                    }
                    using (var args = new ArgsWrapper(fg,
                        $"{loquiGen.TargetObjectGeneration.ExtCommonName}.CopyFieldsFrom"))
                    {
                        args.Add($"item: {itemAccessor}");
                        args.Add("rhs: tmp");
                        args.Add("def: null");
                        args.Add("cmds: null");
                        args.Add("copyMask: null");
                        args.Add("doErrorMask: doMasks");
                        args.Add($"errorMask: out {loquiGen.ErrorMaskItemString} copyMask");
                    }
                    fg.AppendLine($"var loquiMask = {loquiGen.ErrorMaskItemString}.Combine(createMask, copyMask);");
                    fg.AppendLine($"{maskAccessor} = loquiMask == null ? null : new MaskItem<Exception, {loquiGen.ErrorMaskItemString}>(null, loquiMask);");
                }
                else
                {
                    GenerateCopyInRet(fg, typeGen, nodeAccessor, null, maskAccessor);
                    fg.AppendLine("if (tryGet.Succeeded)");
                    using (new BraceWrapper(fg))
                    {
                        fg.AppendLine($"{itemAccessor} = tryGet.Value;");
                    }
                }
            }
            else
            {
                UnsafeXmlTranslationGeneration unsafeXml = new UnsafeXmlTranslationGeneration()
                {
                    ErrMaskString = $"MaskItem<Exception, {loquiGen.ErrorMaskItemString}>"
                };
                unsafeXml.GenerateCopyIn(
                    fg: fg,
                    typeGen: typeGen,
                    nodeAccessor: nodeAccessor,
                    itemAccessor: itemAccessor,
                    maskAccessor: "var unsafeMask");
                fg.AppendLine($"{maskAccessor} = unsafeMask == null ? null : new MaskItem<Exception, object>(null, unsafeMask);");
            }
        }

        public override void GenerateCopyInRet(FileGeneration fg, TypeGeneration typeGen, string nodeAccessor, string retAccessor, string maskAccessor)
        {
            var loquiGen = typeGen as LoquiType;
            if (loquiGen.TargetObjectGeneration != null)
            {
                fg.AppendLine($"{loquiGen.TargetObjectGeneration.ErrorMask} loquiMask;");
                fg.AppendLine($"TryGet<{typeGen.TypeName}> tryGet;");
                fg.AppendLine($"if (typeName.Equals(\"{loquiGen.TargetObjectGeneration.FullName}\"))");
                using (new BraceWrapper(fg))
                {
                    using (var args = new ArgsWrapper(fg,
                        $"tryGet = TryGet<{typeGen.TypeName}>.Succeed(({typeGen.TypeName}){loquiGen.TargetObjectGeneration.Name}.Create_XML"))
                    {
                        args.Add($"root: {nodeAccessor}");
                        args.Add($"doMasks: doMasks");
                        args.Add($"errorMask: out loquiMask)");
                    }
                }
                fg.AppendLine("else");
                using (new BraceWrapper(fg))
                {
                    fg.AppendLine($"var register = LoquiRegistration.GetRegisterByFullName(typeName);");
                    using (var args = new ArgsWrapper(fg,
                        $"tryGet = XmlTranslator.GetTranslator(register.ClassType).Item.Value.Parse",
                        $".Bubble((o) => ({typeGen.TypeName})o)"))
                    {
                        args.Add("root: root");
                        args.Add("doMasks: doMasks");
                        args.Add("maskObj: out var subErrorMaskObj");
                    }
                    fg.AppendLine($"loquiMask = ({loquiGen.TargetObjectGeneration.ErrorMask})subErrorMaskObj;");
                }
                fg.AppendLine($"{maskAccessor} = loquiMask == null ? null : new MaskItem<Exception, {loquiGen.ErrorMaskItemString}>(null, loquiMask);");
                if (retAccessor != null)
                {
                    fg.AppendLine($"{retAccessor}tryGet;");
                }
            }
            else
            {
                UnsafeXmlTranslationGeneration unsafeXml = new UnsafeXmlTranslationGeneration()
                {
                    ErrMaskString = $"MaskItem<Exception, {loquiGen.ErrorMaskItemString}>"
                };
                unsafeXml.GenerateCopyInRet(
                    fg: fg,
                    typeGen: typeGen,
                    nodeAccessor: nodeAccessor,
                    retAccessor: retAccessor,
                    maskAccessor: maskAccessor);
            }
        }
    }
}
