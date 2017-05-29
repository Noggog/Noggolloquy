﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loqui.Generation
{
    public class ListXmlTranslationGeneration : XmlTranslationGeneration
    {
        public override bool OutputsErrorMask => true;
        private readonly XmlTranslationModule _mod;

        public ListXmlTranslationGeneration(XmlTranslationModule mod)
        {
            this._mod = mod;
        }

        public override void GenerateWrite(
            FileGeneration fg,
            TypeGeneration typeGen,
            string writerAccessor,
            string itemAccessor,
            string maskAccessor,
            string nameAccessor)
        {
            var list = typeGen as ListType;
            if (!_mod.TypeGenerations.TryGetValue(list.SubTypeGeneration.GetType(), out var subTransl))
            {
                throw new ArgumentException("Unsupported type generator: " + list.SubTypeGeneration);
            }

            using (var args = new ArgsWrapper(fg,
                $"ListXmlTranslation<{list.SubTypeGeneration.TypeName}>.Instance.Write"))
            {
                args.Add($"writer: {writerAccessor}");
                args.Add($"name: {nameAccessor}");
                args.Add($"item: {itemAccessor}");
                args.Add($"doMasks: doMasks");
                args.Add("maskObj: out object errorMaskObj");
                args.Add((gen) =>
                {
                    gen.AppendLine($"transl: ({list.SubTypeGeneration.TypeName} subItem, out object subMask) =>");
                    using (new BraceWrapper(gen))
                    {
                        subTransl.GenerateWrite(
                            fg: gen, 
                            typeGen: list.SubTypeGeneration, 
                            writerAccessor: "writer", 
                            itemAccessor: $"subItem", 
                            maskAccessor: $"subMask",
                            nameAccessor: "null");
                        if (!subTransl.OutputsErrorMask)
                        {
                            gen.AppendLine($"subMask = null;");
                        }
                    }
                });
            }

            fg.AppendLine($"if (errorMaskObj != null)");
            using (new BraceWrapper(fg))
            {
                fg.AppendLine($"{maskAccessor}().SetNthMask((ushort){typeGen.IndexEnumName}, errorMaskObj);");
            }
        }

        public override void GenerateCopyIn(FileGeneration fg, TypeGeneration typeGen, string nodeAccessor, string itemAccessor, string maskAccessor)
        {
            fg.AppendLine($"throw new NotImplementedException();");
        }
    }
}
