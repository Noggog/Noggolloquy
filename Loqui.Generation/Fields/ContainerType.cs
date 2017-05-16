﻿using System;
using System.Linq;
using System.Xml.Linq;

namespace Loqui.Generation
{
    public abstract class ContainerType : TypeGeneration
    {
        protected bool singleType;
        public TypeGeneration SingleTypeGen;
        protected bool isLoquiSingle;
        protected LoquiType LoquiTypeSingleton => SingleTypeGen as LoquiType;

        public override string Property => $"{this.Name}";
        public override string ProtectedName => $"{this.ProtectedProperty}";

        public override bool Imports
        {
            get
            {
                if (!base.Imports) return false;
                if (!SingleTypeGen.Imports) return false;
                return true;
            }
        }

        public virtual string ItemTypeName
        {
            get
            {
                if (singleType)
                {
                    return SingleTypeGen.TypeName;
                }
                throw new NotImplementedException();
            }
        }

        public TypeGeneration SubTypeGeneration => SingleTypeGen;

        public string GetterTypeName => (this.isLoquiSingle ? LoquiTypeSingleton.TypeName : ItemTypeName);

        public override void Load(XElement node, bool requireName = true)
        {
            base.Load(node, requireName);

            if (!node.Elements().Any())
            {
                throw new ArgumentException("List had no elements.");
            }

            if (node.Elements().Any()
                && ObjectGen.LoadField(
                    node.Elements().First(),
                    false,
                    out SingleTypeGen))
            {
                singleType = true;
                isLoquiSingle = SingleTypeGen as LoquiType != null;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        
        public void AddMaskException(FileGeneration fg, string errorMaskAccessor, string exception)
        {
            fg.AppendLine($"{errorMaskAccessor}?.{this.Name}.Specific.Value.Add({exception});");
        }

        public override void GenerateSetNthHasBeenSet(FileGeneration fg, string identifier, string onIdentifier, bool internalUse)
        {
            if (!this.Protected)
            {
                fg.AppendLine($"{identifier}.{this.GetName(internalUse)}.HasBeenSet = {onIdentifier};");
            }
            fg.AppendLine("break;");
        }

        public override void GenerateUnsetNth(FileGeneration fg, string identifier, string cmdsAccessor)
        {
            if (!this.Protected)
            {
                fg.AppendLine($"{identifier}.{this.GetName(false)}.Unset({cmdsAccessor});");
            }
            fg.AppendLine("break;");
        }

        public override string GetName(bool internalUse, bool property = true)
        {
            if (internalUse)
            {
                return $"_{this.Name}";
            }
            else
            {
                return this.Name;
            }
        }

        public override string GenerateACopy(string rhsAccessor)
        {
            throw new NotImplementedException();
        }
    }
}