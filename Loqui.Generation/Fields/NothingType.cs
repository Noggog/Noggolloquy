﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Loqui.Generation
{
    public class NothingType : TypeGeneration
    {
        public override bool GenerateTypicalItems => false;

        public override string TypeName => null;

        public override string ProtectedName => null;

        public override bool CopyNeedsTryCatch => false;

        public override string GenerateACopy(string rhsAccessor)
        {
            return null;
        }

        public override void GenerateClear(FileGeneration fg, string accessorPrefix, string cmdAccessor)
        {
        }

        public override void GenerateForClass(FileGeneration fg)
        {
        }

        public override void GenerateForCopy(FileGeneration fg, string accessorPrefix, string rhsAccessorPrefix, string copyMaskAccessor, string defaultFallbackAccessor, string cmdsAccessor, bool protectedMembers)
        {
        }

        public override void GenerateForEquals(FileGeneration fg, string rhsAccessor)
        {
        }

        public override void GenerateForEqualsMask(FileGeneration fg, string accessor, string rhsAccessor, string retAccessor)
        {
        }

        public override void GenerateForGetterInterface(FileGeneration fg)
        {
        }

        public override void GenerateForHasBeenSetCheck(FileGeneration fg, string accessor, string checkMaskAccessor)
        {
        }

        public override void GenerateForHasBeenSetMaskGetter(FileGeneration fg, string accessor, string retAccessor)
        {
        }

        public override void GenerateForHash(FileGeneration fg, string hashResultAccessor)
        {
        }

        public override void GenerateForInterface(FileGeneration fg)
        {
        }

        public override void GenerateGetNth(FileGeneration fg, string identifier)
        {
        }

        public override void GenerateSetNth(FileGeneration fg, string accessorPrefix, string rhsAccessorPrefix, string cmdsAccessor, bool internalUse)
        {
        }

        public override void GenerateSetNthHasBeenSet(FileGeneration fg, string identifier, string onIdentifier, bool internalUse)
        {
        }

        public override void GenerateToString(FileGeneration fg, string name, string accessor, string fgAccessor)
        {
        }

        public override void GenerateUnsetNth(FileGeneration fg, string identifier, string cmdsAccessor)
        {
        }

        public override void Load(XElement node, bool requireName = true)
        {
        }

        public override string ToString()
        {
            return "Nothing";
        }

        public override string SkipCheck(string copyMaskAccessor)
        {
            return null;
        }
    }
}