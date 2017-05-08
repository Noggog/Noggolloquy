﻿using Noggog;
using System;
using System.Xml;
using System.Xml.Linq;

namespace Noggolloquy.Xml
{
    public class RangeInt64XmlTranslation : TypicalXmlTranslation<RangeInt64>
    {
        public readonly static RangeInt64XmlTranslation Instance = new RangeInt64XmlTranslation();
        public const string MIN = "Min";
        public const string MAX = "Max";

        protected override bool WriteValue(XmlWriter writer, string name, RangeInt64? item, bool doMasks, out object maskObj)
        {
            maskObj = null;
            if (!item.HasValue) return true;
            writer.WriteAttributeString(MIN, item.Value.Min.ToString());
            writer.WriteAttributeString(MAX, item.Value.Max.ToString());
            return true;
        }

        protected override string GetItemStr(RangeInt64 item)
        {
            throw new NotImplementedException();
        }

        protected override RangeInt64 ParseNonNullString(string str)
        {
            throw new NotImplementedException();
        }

        protected override TryGet<RangeInt64?> ParseValue(XElement root, bool nullable, bool doMasks, out object maskObj)
        {
            maskObj = null;
            long? min, max;
            if (root.TryGetAttribute(MIN, out XAttribute val))
            {
                if (long.TryParse(val.Value, out var i))
                {
                    min = i;
                }
                else
                {
                    throw new ArgumentException("Min value was malformed: " + val.Value);
                }
            }
            else
            {
                min = null;
            }
            if (root.TryGetAttribute(MAX, out val))
            {
                if (long.TryParse(val.Value, out var i))
                {
                    max = i;
                }
                else
                {
                    throw new ArgumentException("Min value was malformed: " + val.Value);
                }
            }
            else
            {
                max = null;
            }
            if (!min.HasValue && !max.HasValue) return TryGet<RangeInt64?>.Succeed(null);
            return TryGet<RangeInt64?>.Succeed(
                new RangeInt64(min, max));
        }
    }
}