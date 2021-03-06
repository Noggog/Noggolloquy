using Loqui.Internal;
using Noggog;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Loqui.Xml
{
    public class ByteArrayXmlTranslation : TypicalXmlTranslation<MemorySlice<byte>>
    {
        public readonly static ByteArrayXmlTranslation Instance = new ByteArrayXmlTranslation();

        protected override string GetItemStr(MemorySlice<byte> item)
        {
            return item.ToHexString();
        }

        protected virtual string GetItemStr(ReadOnlySpan<byte> item)
        {
            return item.ToHexString();
        }

        public override bool Parse(XElement node, [MaybeNullWhen(false)] out MemorySlice<byte> val)
        {
            if (!node.TryGetAttribute(XmlConstants.VALUE_ATTRIBUTE, out XAttribute? attr)
                || attr.Value == null)
            {
                val = default;
                return false;
            }
            val = Parse(attr.Value);
            return true;
        }

        protected override MemorySlice<byte> Parse(string str)
        {
            if (str.Length % 2 != 0)
            {
                throw new ArgumentException($"Unexpected byte array length: {str.Length}.  Should be even.");
            }
            int i = 0;
            try
            {
                byte[] ret = new byte[str.Length / 2];
                for (; i < str.Length; i += 2)
                {
                    ret[i / 2] = Convert.ToByte(str.Substring(i, 2), 16);
                }
                return ret;
            }
            catch (FormatException)
            {
                throw new ArgumentException("Malformed byte: " + str.Substring(i, 2));
            }
        }

        public MemorySlice<byte>? Parse(
            XElement node,
            ErrorMaskBuilder? errorMask)
        {
            if (this.Parse(
                node: node,
                val: out var item))
            {
                return item;
            }
            return null;
        }

        public MemorySlice<byte> Parse(
            XElement node,
            ErrorMaskBuilder? errorMask,
            int fallbackLength)
        {
            if (this.Parse(
                node: node,
                val: out var item))
            {
                return item;
            }
            return new byte[fallbackLength];
        }

        public void Write(
            XElement node,
            string? name,
            ReadOnlySpan<byte> item,
            int fieldIndex,
            ErrorMaskBuilder? errorMask)
        {
            using (errorMask?.PushIndex(fieldIndex))
            {
                try
                {
                    this.Write(
                        node: node,
                        name: name,
                        item: item);
                }
                catch (Exception ex)
                when (errorMask != null)
                {
                    errorMask.ReportException(ex);
                }
            }
        }

        public void Write(
            XElement node,
            string? name,
            ReadOnlySpan<byte> item,
            ErrorMaskBuilder? errorMask)
        {
            this.Write(
                node: node,
                name: name,
                item: item);
        }

        protected virtual void WriteValue(XElement node, string? name, ReadOnlySpan<byte> item)
        {
            node.SetAttributeValue(
                XmlConstants.VALUE_ATTRIBUTE,
                item != null ? GetItemStr(item) : string.Empty);
        }

        private void Write(
            XElement node,
            string? name,
            ReadOnlySpan<byte> item)
        {
            var elem = new XElement(name!);
            node.Add(elem);
            WriteValue(elem, name, item);
        }
    }
}
