﻿using Loqui.Tests.Internals;
using Loqui.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;

namespace Loqui.Tests.XML
{
    public class RefValDictXmlTranslation_Tests
    {
        public static readonly RefValDictXmlTranslation_Tests Instance = new RefValDictXmlTranslation_Tests();
        public string ExpectedName => "Dict";

        public IEnumerable<KeyValuePair<string, ObjectToRef>> GetTypicalContents()
        {
            yield return new KeyValuePair<string, ObjectToRef>("Hello", ObjectToRef.TYPICAL_VALUE);
            yield return new KeyValuePair<string, ObjectToRef>("Test", ObjectToRef.TYPICAL_VALUE_2);
            yield return new KeyValuePair<string, ObjectToRef>("Test2", ObjectToRef.TYPICAL_VALUE_3);
        }

        public DictXmlTranslation<string, ObjectToRef> GetTranslation()
        {
            return new DictXmlTranslation<string, ObjectToRef>();
        }

        public virtual XElement GetTypicalElement(string name = null)
        {
            var elem = XmlUtility.GetElementNoValue(ExpectedName, name);
            foreach (var item in GetTypicalContents())
            {
                var itemElem = new XElement("Item");
                var keyElem = new XElement("Key");
                var stringElem = new XElement("String");
                stringElem.SetAttributeValue("value", item.Key);
                keyElem.Add(stringElem);
                itemElem.Add(keyElem);
                var valElem = new XElement("Value");
                valElem.Add(ObjectToRefXmlTranslation_Test.Instance.GetTypicalElement(item.Value));
                itemElem.Add(valElem);
                elem.Add(itemElem);
            }
            return elem;
        }

        #region Element Name
        [Fact]
        public void ElementName()
        {
            var transl = GetTranslation();
            Assert.Equal(ExpectedName, transl.ElementName);
        }
        #endregion

        #region Single Items
        [Fact]
        public void ReimportSingleItem()
        {
            var val = new KeyValuePair<string, ObjectToRef>(
                "Hello",
                ObjectToRef.TYPICAL_VALUE);
            var transl = GetTranslation();
            var writer = XmlUtility.GetWriteBundle();
            transl.WriteSingleItem(
                keyTransl: (string item, out object subErrorMask) =>
                {
                    StringXmlTranslation.Instance.Write(
                        writer.Writer,
                        null,
                        item);
                    subErrorMask = null;
                },
                valTransl: (ObjectToRef item, out object subErrorMask) =>
                {
                    item.Write_XML(writer.Writer, out var errMask);
                    subErrorMask = errMask;
                },
                writer: writer.Writer,
                item: val,
                doMasks: false,
                keymaskItem: out MaskItem<Exception, object> keyMaskObj,
                valmaskItem: out MaskItem<Exception, object> valMaskObj);
            var readResp = transl.ParseSingleItem(
                writer.Resolve(),
                keyTranl: StringXmlTranslation.Instance,
                valTranl: LoquiXmlTranslation<ObjectToRef, ObjectToRef_ErrorMask>.Instance,
                doMasks: false,
                maskObj: out object readMaskObj);
            Assert.True(readResp.Succeeded);
            Assert.Equal(val, readResp.Value);
        }
        #endregion

        #region Parse - Typical
        [Fact]
        public void Parse_NoMask()
        {
            var transl = GetTranslation();
            var elem = GetTypicalElement();
            var ret = transl.Parse(
                elem,
                doMasks: false,
                maskObj: out object maskObj);
            Assert.True(ret.Succeeded);
            Assert.Null(maskObj);
            Assert.Equal(GetTypicalContents(), ret.Value);
        }

        [Fact]
        public void Parse_Mask()
        {
            var transl = GetTranslation();
            var elem = GetTypicalElement();
            var ret = transl.Parse(
                elem,
                doMasks: true,
                maskObj: out object maskObj);
            Assert.True(ret.Succeeded);
            Assert.Null(maskObj);
            Assert.Equal(GetTypicalContents(), ret.Value);
        }
        #endregion

        #region Parse - Bad Element Name
        [Fact]
        public void Parse_BadElementName_Mask()
        {
            var transl = GetTranslation();
            var elem = XmlUtility.GetBadlyNamedElement();
            var ret = transl.Parse(
                elem,
                doMasks: true,
                maskObj: out object maskObj);
            Assert.True(ret.Failed);
            Assert.NotNull(maskObj);
            Assert.IsType(typeof(ArgumentException), maskObj);
        }

        [Fact]
        public void Parse_BadElementName_NoMask()
        {
            var transl = GetTranslation();
            var elem = XmlUtility.GetBadlyNamedElement();
            Assert.Throws(
                typeof(ArgumentException),
                () => transl.Parse(
                    elem,
                    doMasks: false,
                    maskObj: out object maskObj));
        }
        #endregion

        #region Parse - No Value
        [Fact]
        public void Parse_NoValue_NoMask()
        {
            var transl = GetTranslation();
            var elem = XmlUtility.GetElementNoValue(ExpectedName);
            var ret = transl.Parse(
                elem,
                doMasks: true,
                maskObj: out object maskObj);
            Assert.True(ret.Succeeded);
            Assert.Null(maskObj);
            Assert.Empty(ret.Value);
        }

        [Fact]
        public void Parse_NoValue_Mask()
        {
            var transl = GetTranslation();
            var elem = XmlUtility.GetElementNoValue(ExpectedName);
            var ret = transl.Parse(
                elem,
                doMasks: true,
                maskObj: out object maskObj);
            Assert.True(ret.Succeeded);
            Assert.Null(maskObj);
            Assert.Empty(ret.Value);
        }
        #endregion

        #region Parse - Empty Value
        [Fact]
        public void Parse_EmptyValue_NoMask()
        {
            var transl = GetTranslation();
            var elem = XmlUtility.GetElementNoValue(ExpectedName);
            elem.SetAttributeValue(XName.Get(XmlConstants.VALUE_ATTRIBUTE), string.Empty);
            var ret = transl.Parse(
                elem,
                doMasks: false,
                maskObj: out object maskObj);
            Assert.True(ret.Succeeded);
            Assert.Null(maskObj);
            Assert.Empty(ret.Value);
        }

        [Fact]
        public void Parse_EmptyValue_Mask()
        {
            var transl = GetTranslation();
            var elem = XmlUtility.GetElementNoValue(ExpectedName);
            elem.SetAttributeValue(XName.Get(XmlConstants.VALUE_ATTRIBUTE), string.Empty);
            var ret = transl.Parse(
                elem,
                doMasks: true,
                maskObj: out object maskObj);
            Assert.True(ret.Succeeded);
            Assert.Null(maskObj);
            Assert.Empty(ret.Value);
        }
        #endregion

        #region Write - Typical
        [Fact]
        public void Write_NoMask()
        {
            var transl = GetTranslation();
            var writer = XmlUtility.GetWriteBundle();
            transl.Write(
                writer: writer.Writer,
                name: null,
                items: GetTypicalContents(),
                doMasks: false,
                maskObj: out object maskObj);
            Assert.Null(maskObj);
            XElement elem = writer.Resolve();
            Assert.Null(elem.Attribute(XName.Get(XmlConstants.NAME_ATTRIBUTE)));
            Assert.Equal(GetTypicalContents().Count(), elem.Elements().Count());
        }

        [Fact]
        public void Write_Mask()
        {
            var transl = GetTranslation();
            var writer = XmlUtility.GetWriteBundle();
            transl.Write(
                writer: writer.Writer,
                name: XmlUtility.TYPICAL_NAME,
                items: GetTypicalContents(),
                doMasks: true,
                maskObj: out object maskObj);
            Assert.Null(maskObj);
            XElement elem = writer.Resolve();
            Assert.Equal(XmlUtility.TYPICAL_NAME, elem.Attribute(XName.Get(XmlConstants.NAME_ATTRIBUTE)).Value);
            Assert.Equal(GetTypicalContents().Count(), elem.Elements().Count());
        }
        #endregion

        #region Reimport
        [Fact]
        public void Reimport_Typical()
        {
            var transl = GetTranslation();
            var writer = XmlUtility.GetWriteBundle();
            transl.Write(
                writer: writer.Writer,
                name: XmlUtility.TYPICAL_NAME,
                items: GetTypicalContents(),
                doMasks: false,
                maskObj: out object maskObj);
            var readResp = transl.Parse(
                writer.Resolve(),
                doMasks: false,
                maskObj: out object readMaskObj);
            Assert.True(readResp.Succeeded);
            Assert.Equal(GetTypicalContents(), readResp.Value);
        }

        [Fact]
        public void Reimport_Empty()
        {
            var transl = GetTranslation();
            var writer = XmlUtility.GetWriteBundle();
            transl.Write(
                writer: writer.Writer,
                name: XmlUtility.TYPICAL_NAME,
                items: new KeyValuePair<string, ObjectToRef>[] { },
                doMasks: false,
                maskObj: out object maskObj);
            var readResp = transl.Parse(
                writer.Resolve(),
                doMasks: false,
                maskObj: out object readMaskObj);
            Assert.True(readResp.Succeeded);
            Assert.Empty(readResp.Value);
        }
        #endregion
    }
}