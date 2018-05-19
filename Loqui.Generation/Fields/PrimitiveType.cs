﻿using System;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Loqui.Generation
{
    public abstract class PrimitiveType : TypicalTypeGeneration
    {
        public override bool IsNullable() => this.TypeName.EndsWith("?");
        public override bool IsEnumerable => false;
        public override bool IsClass => false;
    }
}
