﻿using Noggog;
using System;
using System.Xml;
using System.Xml.Linq;

namespace Loqui.Xml
{
    public class RangeUInt64XmlTranslation : PrimitiveXmlTranslation<RangeUInt64>
    {
        public readonly static RangeUInt64XmlTranslation Instance = new RangeUInt64XmlTranslation();
        public const string MIN = "Min";
        public const string MAX = "Max";

        protected override void WriteValue(XmlWriter writer, string name, RangeUInt64? item)
        {
            if (!item.HasValue) return;
            writer.WriteAttributeString(MIN, item.Value.Min.ToString());
            writer.WriteAttributeString(MAX, item.Value.Max.ToString());
        }

        protected override string GetItemStr(RangeUInt64 item)
        {
            throw new NotImplementedException();
        }

        protected override RangeUInt64 ParseNonNullString(string str)
        {
            throw new NotImplementedException();
        }

        protected override RangeUInt64? ParseValue(XElement root)
        {
            ulong? min, max;
            if (root.TryGetAttribute(MIN, out XAttribute val))
            {
                if (ulong.TryParse(val.Value, out var i))
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
                if (ulong.TryParse(val.Value, out var i))
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
            if (!min.HasValue && !max.HasValue) return null;
            return new RangeUInt64(min, max);
        }
    }
}
