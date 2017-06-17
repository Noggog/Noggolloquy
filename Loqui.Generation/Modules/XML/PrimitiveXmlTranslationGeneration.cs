﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loqui.Generation
{
    public class PrimitiveXmlTranslationGeneration<T> : XmlTranslationGeneration
    {
        private string typeName;
        private bool? nullable;
        public bool Nullable => nullable ?? false || typeof(T).GetName().EndsWith("?");
        public bool CanBeNotNullable = true;

        public PrimitiveXmlTranslationGeneration(string typeName = null, bool? nullable = null)
        {
            this.nullable = nullable;
            this.typeName = typeName ?? typeof(T).GetName().Replace("?", string.Empty);
        }

        public override void GenerateWrite(
            FileGeneration fg,
            TypeGeneration typeGen,
            string writerAccessor, 
            string itemAccessor,
            string maskAccessor,
            string nameAccessor)
        {
            using (var args = new ArgsWrapper(fg,
                $"{this.typeName}XmlTranslation.Instance.Write"))
            {
                args.Add(writerAccessor);
                args.Add(nameAccessor);
                args.Add(itemAccessor);
                args.Add($"doMasks: doMasks");
                args.Add($"errorMask: out {maskAccessor}");
            }
        }

        public override void GenerateCopyIn(FileGeneration fg, TypeGeneration typeGen, string nodeAccessor, string itemAccessor, string maskAccessor)
        {
            using (var args = new ArgsWrapper(fg,
                $"var tryGet = {this.typeName}XmlTranslation.Instance.Parse"))
            {
                args.Add(nodeAccessor);
                if (CanBeNotNullable)
                {
                    args.Add($"nullable: {Nullable.ToString().ToLower()}");
                }
                args.Add($"doMasks: doMasks");
                args.Add($"errorMask: out {maskAccessor}");
            }
            fg.AppendLine("if (tryGet.Succeeded)");
            using (new BraceWrapper(fg))
            {
                fg.AppendLine($"{itemAccessor} = tryGet.Value{(Nullable ? null : ".Value")};");
            }
        }

        public override void GenerateCopyInRet(FileGeneration fg, TypeGeneration typeGen, string nodeAccessor, string retAccessor, string maskAccessor)
        {
            using (var args = new ArgsWrapper(fg,
                $"{retAccessor}{this.typeName}XmlTranslation.Instance.Parse",
                (this.Nullable ? string.Empty : $".Bubble((o) => o.Value)")))
            {
                args.Add(nodeAccessor);
                if (CanBeNotNullable)
                {
                    args.Add($"nullable: {Nullable.ToString().ToLower()}");
                }
                args.Add($"doMasks: doMasks");
                args.Add($"errorMask: out {maskAccessor}");
            }
        }
    }
}
