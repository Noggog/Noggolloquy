﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Loqui.Generation
{
    public class ListXmlTranslationGeneration : XmlTranslationGeneration
    {
        public virtual string TranslatorName => $"ListXmlTranslation";

        public override string GetTranslatorInstance(TypeGeneration typeGen)
        {
            var list = typeGen as ListType;
            if (!XmlMod.TryGetTypeGeneration(list.SubTypeGeneration.GetType(), out var subTransl))
            {
                throw new ArgumentException("Unsupported type generator: " + list.SubTypeGeneration);
            }

            var subMaskStr = subTransl.MaskModule.GetMaskModule(list.SubTypeGeneration.GetType()).GetErrorMaskTypeStr(list.SubTypeGeneration);
            return $"{TranslatorName}<{list.SubTypeGeneration.TypeName}, {subMaskStr}>.Instance";
        }

        public override void GenerateWrite(
            FileGeneration fg,
            ObjectGeneration objGen,
            TypeGeneration typeGen,
            string writerAccessor,
            Accessor itemAccessor,
            string maskAccessor,
            string nameAccessor)
        {
            var list = typeGen as ListType;
            if (!XmlMod.TryGetTypeGeneration(list.SubTypeGeneration.GetType(), out var subTransl))
            {
                throw new ArgumentException("Unsupported type generator: " + list.SubTypeGeneration);
            }
            
            using (var args = new ArgsWrapper(fg,
                $"{TranslatorName}<{list.SubTypeGeneration.TypeName}>.Instance.Write"))
            {
                args.Add($"node: {writerAccessor}");
                args.Add($"name: {nameAccessor}");
                args.Add($"item: {itemAccessor.DirectAccess}");
                if (typeGen.HasIndex)
                {
                    args.Add($"fieldIndex: (int){typeGen.IndexEnumName}");
                    args.Add($"errorMask: {maskAccessor}");
                }
                else
                {
                    throw new NotImplementedException();
                }
                args.Add((gen) =>
                {
                    gen.AppendLine($"transl: (XElement subNode, {list.SubTypeGeneration.TypeName} subItem, ErrorMaskBuilder listSubMask) =>");
                    using (new BraceWrapper(gen))
                    {
                        subTransl.GenerateWrite(
                            fg: gen,
                            objGen: objGen,
                            typeGen: list.SubTypeGeneration,
                            writerAccessor: "subNode",
                            itemAccessor: new Accessor($"subItem"),
                            maskAccessor: $"listSubMask",
                            nameAccessor: "\"Item\"");
                    }
                });
                ExtraWriteArgs(itemAccessor, typeGen, args);
            }
        }

        protected virtual void ExtraWriteArgs(
            Accessor itemAccessor,
            TypeGeneration typeGen,
            ArgsWrapper args)
        {
        }

        public override void GenerateCopyIn(
            FileGeneration fg,
            TypeGeneration typeGen,
            string nodeAccessor,
            Accessor itemAccessor,
            string maskAccessor)
        {
            GenerateCopyInRet_Internal(
                fg: fg,
                typeGen: typeGen,
                nodeAccessor: nodeAccessor,
                itemAccessor: itemAccessor,
                ret: false,
                indexAccessor: $"(int){typeGen.IndexEnumName}",
                maskAccessor: maskAccessor);
        }

        public override void GenerateCopyInRet(
            FileGeneration fg,
            TypeGeneration typeGen,
            string nodeAccessor,
            Accessor retAccessor,
            string indexAccessor,
            string maskAccessor)
        {
            GenerateCopyInRet_Internal(
                fg: fg,
                typeGen: typeGen,
                nodeAccessor: nodeAccessor,
                itemAccessor: retAccessor,
                ret: true,
                indexAccessor: indexAccessor,
                maskAccessor: maskAccessor);
        }

        public void GenerateCopyInRet_Internal(
            FileGeneration fg,
            TypeGeneration typeGen,
            string nodeAccessor,
            Accessor itemAccessor,
            bool ret,
            string indexAccessor,
            string maskAccessor)
        {
            var list = typeGen as ListType;
            if (!XmlMod.TryGetTypeGeneration(list.SubTypeGeneration.GetType(), out var subTransl))
            {
                throw new ArgumentException("Unsupported type generator: " + list.SubTypeGeneration);
            }
           
            if (ret)
            {
                throw new NotImplementedException();
            }

            MaskGenerationUtility.WrapErrorFieldIndexPush(
                fg: fg,
                toDo: () =>
                {
                    using (var args = new FunctionWrapper(
                        fg,
                        $"if ({TranslatorName}<{list.SubTypeGeneration.TypeName}>.Instance.Parse"))
                    {
                        args.Add("root: root");
                        args.Add($"enumer: out var {typeGen.Name}Item");
                        args.Add($"transl: {subTransl.GetTranslatorInstance(list.SubTypeGeneration)}.Parse");
                        args.Add("errorMask: errorMask)");
                    }
                    using (new BraceWrapper(fg))
                    {
                        fg.AppendLine($"{itemAccessor.DirectAccess}.SetTo({typeGen.Name}Item);");
                    }
                    fg.AppendLine("else");
                    using (new BraceWrapper(fg))
                    {
                        fg.AppendLine($"{itemAccessor.DirectAccess}.Unset();");
                    }
                },
                maskAccessor: maskAccessor,
                indexAccessor: typeGen.IndexEnumInt);
        }

        public override XElement GenerateForXSD(
            ObjectGeneration objGen,
            XElement rootElement,
            XElement choiceElement,
            TypeGeneration typeGen,
            string nameOverride = null)
        {
            var elem = new XElement(XmlTranslationModule.XSDNamespace + "element",
                new XAttribute("name", nameOverride ?? typeGen.Name),
                new XAttribute("type", $"{typeGen.Name}Type"));
            choiceElement.Add(elem);

            var subChoice = new XElement(XmlTranslationModule.XSDNamespace + "choice",
                new XAttribute("minOccurs", 0),
                new XAttribute("maxOccurs", "unbounded"));
            rootElement.Add(
                new XElement(XmlTranslationModule.XSDNamespace + "complexType",
                    new XAttribute("name", $"{typeGen.Name}Type"),
                    subChoice));

            var list = typeGen as ListType;
            var xmlGen = XmlMod.GetTypeGeneration(list.SubTypeGeneration.GetType());
            var subElem = xmlGen.GenerateForXSD(
                objGen,
                rootElement,
                subChoice,
                list.SubTypeGeneration,
                nameOverride: "Item");
            return elem;
        }

        public override void GenerateForCommonXSD(XElement rootElement, TypeGeneration typeGen)
        {
        }
    }
}
