using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfSharper.Fonts.AFM;
using PdfSharper.Pdf;
using PdfSharper.Pdf.AcroForms;
using PdfSharper.Pdf.Annotations;
using PDFsharper.UnitTests.Pdf.AcroForms;
using System;
using System.Linq;

namespace PDFsharper.UnitTests.Fonts.AFM
{
    [TestClass]
    public class AFMCacheTests
    {
        [TestMethod]
        public void Instance()
        {
            Assert.IsNotNull(AFMCache.Instance, "AFM Cache should not be null");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_Name_Empty()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes(string.Empty, false, false);

            Assert.IsNull(details, "AFM Details should be null");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Test fails because ArgumentNullException was not thrown.")]
        public void GetFontMetricsByNameAndAttributes_Name_Null()
        {
            AFMCache.Instance.GetFontMetricsByNameAndAttributes(null, false, false);
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_Name_DoesNotExist()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("abcdefghijklmnopqrstuvwxyz", false, false);

            Assert.IsNull(details, "AFM Details should be null");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_Name_IncorrectCase()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("courier", false, false);

            Assert.IsNull(details, "AFM Details should be null");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_Courier()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("Courier", false, false);

            Assert.IsNotNull(details, "AFM Details should not be null");
            Assert.IsTrue(details.FontName == "Courier", "FontName is not correct");
            Assert.IsTrue(details.Ascender == 629, "Ascender is not correct");
            Assert.IsTrue(details.BBoxLLX == -23, "BBoxLLX is not correct");
            Assert.IsTrue(details.BBoxLLY == -250, "BBoxLLY is not correct");
            Assert.IsTrue(details.BBoxURX == 715, "BBoxURX is not correct");
            Assert.IsTrue(details.BBoxURY == 805, "BBoxURY is not correct");
            Assert.IsTrue(details.CapHeight == 562, "CapHeight is not correct");
            Assert.IsNotNull(details.CharacterWidths, "CharacterWidths should not be null");
            Assert.IsTrue(details.CharacterWidths.Count == 149, "CharacterWidths count is not correct");
            Assert.IsTrue(details.Descender == -157, "Descender is not correct");
            Assert.IsTrue(details.EncodingScheme == "AdobeStandardEncoding", "EncodingScheme is not correct");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_CourierBold()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("Courier", true, false);

            Assert.IsNotNull(details, "AFM Details should not be null");
            Assert.IsTrue(details.FontName == "Courier-Bold", "FontName is not correct");
            Assert.IsTrue(details.Ascender == 629, "Ascender is not correct");
            Assert.IsTrue(details.BBoxLLX == -113, "BBoxLLX is not correct");
            Assert.IsTrue(details.BBoxLLY == -250, "BBoxLLY is not correct");
            Assert.IsTrue(details.BBoxURX == 749, "BBoxURX is not correct");
            Assert.IsTrue(details.BBoxURY == 801, "BBoxURY is not correct");
            Assert.IsTrue(details.CapHeight == 562, "CapHeight is not correct");
            Assert.IsNotNull(details.CharacterWidths, "CharacterWidths should not be null");
            Assert.IsTrue(details.CharacterWidths.Count == 149, "CharacterWidths count is not correct");
            Assert.IsTrue(details.Descender == -157, "Descender is not correct");
            Assert.IsTrue(details.EncodingScheme == "AdobeStandardEncoding", "EncodingScheme is not correct");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_CourierItalic()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("Courier", false, true);

            Assert.IsNotNull(details, "AFM Details should not be null");
            Assert.IsTrue(details.FontName == "Courier-Oblique", "FontName is not correct");
            Assert.IsTrue(details.Ascender == 629, "Ascender is not correct");
            Assert.IsTrue(details.BBoxLLX == -27, "BBoxLLX is not correct");
            Assert.IsTrue(details.BBoxLLY == -250, "BBoxLLY is not correct");
            Assert.IsTrue(details.BBoxURX == 849, "BBoxURX is not correct");
            Assert.IsTrue(details.BBoxURY == 805, "BBoxURY is not correct");
            Assert.IsTrue(details.CapHeight == 562, "CapHeight is not correct");
            Assert.IsNotNull(details.CharacterWidths, "CharacterWidths should not be null");
            Assert.IsTrue(details.CharacterWidths.Count == 149, "CharacterWidths count is not correct");
            Assert.IsTrue(details.Descender == -157, "Descender is not correct");
            Assert.IsTrue(details.EncodingScheme == "AdobeStandardEncoding", "EncodingScheme is not correct");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_CourierBoldItalic()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("Courier", true, true);

            Assert.IsNotNull(details, "AFM Details should not be null");
            Assert.IsTrue(details.FontName == "Courier-BoldOblique", "FontName is not correct");
            Assert.IsTrue(details.Ascender == 629, "Ascender is not correct");
            Assert.IsTrue(details.BBoxLLX == -57, "BBoxLLX is not correct");
            Assert.IsTrue(details.BBoxLLY == -250, "BBoxLLY is not correct");
            Assert.IsTrue(details.BBoxURX == 869, "BBoxURX is not correct");
            Assert.IsTrue(details.BBoxURY == 801, "BBoxURY is not correct");
            Assert.IsTrue(details.CapHeight == 562, "CapHeight is not correct");
            Assert.IsNotNull(details.CharacterWidths, "CharacterWidths should not be null");
            Assert.IsTrue(details.CharacterWidths.Count == 149, "CharacterWidths count is not correct");
            Assert.IsTrue(details.Descender == -157, "Descender is not correct");
            Assert.IsTrue(details.EncodingScheme == "AdobeStandardEncoding", "EncodingScheme is not correct");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_Helvetica()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("Helvetica", false, false);

            Assert.IsNotNull(details, "AFM Details should not be null");
            Assert.IsTrue(details.FontName == "Helvetica", "FontName is not correct");
            Assert.IsTrue(details.Ascender == 718, "Ascender is not correct");
            Assert.IsTrue(details.BBoxLLX == -166, "BBoxLLX is not correct");
            Assert.IsTrue(details.BBoxLLY == -225, "BBoxLLY is not correct");
            Assert.IsTrue(details.BBoxURX == 1000, "BBoxURX is not correct");
            Assert.IsTrue(details.BBoxURY == 931, "BBoxURY is not correct");
            Assert.IsTrue(details.CapHeight == 718, "CapHeight is not correct");
            Assert.IsNotNull(details.CharacterWidths, "CharacterWidths should not be null");
            Assert.IsTrue(details.CharacterWidths.Count == 149, "CharacterWidths count is not correct");
            Assert.IsTrue(details.Descender == -207, "Descender is not correct");
            Assert.IsTrue(details.EncodingScheme == "AdobeStandardEncoding", "EncodingScheme is not correct");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_HelveticaBold()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("Helvetica", true, false);

            Assert.IsNotNull(details, "AFM Details should not be null");
            Assert.IsTrue(details.FontName == "Helvetica-Bold", "FontName is not correct");
            Assert.IsTrue(details.Ascender == 718, "Ascender is not correct");
            Assert.IsTrue(details.BBoxLLX == -170, "BBoxLLX is not correct");
            Assert.IsTrue(details.BBoxLLY == -228, "BBoxLLY is not correct");
            Assert.IsTrue(details.BBoxURX == 1003, "BBoxURX is not correct");
            Assert.IsTrue(details.BBoxURY == 962, "BBoxURY is not correct");
            Assert.IsTrue(details.CapHeight == 718, "CapHeight is not correct");
            Assert.IsNotNull(details.CharacterWidths, "CharacterWidths should not be null");
            Assert.IsTrue(details.CharacterWidths.Count == 149, "CharacterWidths count is not correct");
            Assert.IsTrue(details.Descender == -207, "Descender is not correct");
            Assert.IsTrue(details.EncodingScheme == "AdobeStandardEncoding", "EncodingScheme is not correct");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_HelveticaItalic()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("Helvetica", false, true);

            Assert.IsNotNull(details, "AFM Details should not be null");
            Assert.IsTrue(details.FontName == "Helvetica-Oblique", "FontName is not correct");
            Assert.IsTrue(details.Ascender == 718, "Ascender is not correct");
            Assert.IsTrue(details.BBoxLLX == -170, "BBoxLLX is not correct");
            Assert.IsTrue(details.BBoxLLY == -225, "BBoxLLY is not correct");
            Assert.IsTrue(details.BBoxURX == 1116, "BBoxURX is not correct");
            Assert.IsTrue(details.BBoxURY == 931, "BBoxURY is not correct");
            Assert.IsTrue(details.CapHeight == 718, "CapHeight is not correct");
            Assert.IsNotNull(details.CharacterWidths, "CharacterWidths should not be null");
            Assert.IsTrue(details.CharacterWidths.Count == 149, "CharacterWidths count is not correct");
            Assert.IsTrue(details.Descender == -207, "Descender is not correct");
            Assert.IsTrue(details.EncodingScheme == "AdobeStandardEncoding", "EncodingScheme is not correct");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_HelveticaBoldItalic()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("Helvetica", true, true);

            Assert.IsNotNull(details, "AFM Details should not be null");
            Assert.IsTrue(details.FontName == "Helvetica-BoldOblique", "FontName is not correct");
            Assert.IsTrue(details.Ascender == 718, "Ascender is not correct");
            Assert.IsTrue(details.BBoxLLX == -174, "BBoxLLX is not correct");
            Assert.IsTrue(details.BBoxLLY == -228, "BBoxLLY is not correct");
            Assert.IsTrue(details.BBoxURX == 1114, "BBoxURX is not correct");
            Assert.IsTrue(details.BBoxURY == 962, "BBoxURY is not correct");
            Assert.IsTrue(details.CapHeight == 718, "CapHeight is not correct");
            Assert.IsNotNull(details.CharacterWidths, "CharacterWidths should not be null");
            Assert.IsTrue(details.CharacterWidths.Count == 149, "CharacterWidths count is not correct");
            Assert.IsTrue(details.Descender == -207, "Descender is not correct");
            Assert.IsTrue(details.EncodingScheme == "AdobeStandardEncoding", "EncodingScheme is not correct");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_Segoe()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("Segoe UI", false, false);

            Assert.IsNotNull(details, "AFM Details should not be null");
            Assert.IsTrue(details.FontName == "SegoeUI", "FontName is not correct");
            Assert.IsTrue(details.Ascender == 740, "Ascender is not correct");
            Assert.IsTrue(details.BBoxLLX == -432, "BBoxLLX is not correct");
            Assert.IsTrue(details.BBoxLLY == -412, "BBoxLLY is not correct");
            Assert.IsTrue(details.BBoxURX == 2000, "BBoxURX is not correct");
            Assert.IsTrue(details.BBoxURY == 1160, "BBoxURY is not correct");
            Assert.IsTrue(details.CapHeight == 700, "CapHeight is not correct");
            Assert.IsNotNull(details.CharacterWidths, "CharacterWidths should not be null");
            Assert.IsTrue(details.CharacterWidths.Count == 192, "CharacterWidths count is not correct");
            Assert.IsTrue(details.Descender == -230, "Descender is not correct");
            Assert.IsTrue(details.EncodingScheme == "ISO10646-1", "EncodingScheme is not correct");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_SegoeBold()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("Segoe UI", true, false);

            Assert.IsNotNull(details, "AFM Details should not be null");
            Assert.IsTrue(details.FontName == "SegoeUI-Bold", "FontName is not correct");
            Assert.IsTrue(details.Ascender == 740, "Ascender is not correct");
            Assert.IsTrue(details.BBoxLLX == -479, "BBoxLLX is not correct");
            Assert.IsTrue(details.BBoxLLY == -431, "BBoxLLY is not correct");
            Assert.IsTrue(details.BBoxURX == 2000, "BBoxURX is not correct");
            Assert.IsTrue(details.BBoxURY == 1160, "BBoxURY is not correct");
            Assert.IsTrue(details.CapHeight == 700, "CapHeight is not correct");
            Assert.IsNotNull(details.CharacterWidths, "CharacterWidths should not be null");
            Assert.IsTrue(details.CharacterWidths.Count == 192, "CharacterWidths count is not correct");
            Assert.IsTrue(details.Descender == -230, "Descender is not correct");
            Assert.IsTrue(details.EncodingScheme == "ISO10646-1", "EncodingScheme is not correct");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_SegoeItalic()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("Segoe UI", false, true);

            Assert.IsNotNull(details, "AFM Details should not be null");
            Assert.IsTrue(details.FontName == "SegoeUI-Italic", "FontName is not correct");
            Assert.IsTrue(details.Ascender == 740, "Ascender is not correct");
            Assert.IsTrue(details.BBoxLLX == -458, "BBoxLLX is not correct");
            Assert.IsTrue(details.BBoxLLY == -265, "BBoxLLY is not correct");
            Assert.IsTrue(details.BBoxURX == 1423, "BBoxURX is not correct");
            Assert.IsTrue(details.BBoxURY == 1160, "BBoxURY is not correct");
            Assert.IsTrue(details.CapHeight == 700, "CapHeight is not correct");
            Assert.IsNotNull(details.CharacterWidths, "CharacterWidths should not be null");
            Assert.IsTrue(details.CharacterWidths.Count == 192, "CharacterWidths count is not correct");
            Assert.IsTrue(details.Descender == -233, "Descender is not correct");
            Assert.IsTrue(details.EncodingScheme == "ISO10646-1", "EncodingScheme is not correct");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_SegoeBoldItalic()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("Segoe UI", true, true);

            Assert.IsNotNull(details, "AFM Details should not be null");
            Assert.IsTrue(details.FontName == "SegoeUI-BoldItalic", "FontName is not correct");
            Assert.IsTrue(details.Ascender == 740, "Ascender is not correct");
            Assert.IsTrue(details.BBoxLLX == -458, "BBoxLLX is not correct");
            Assert.IsTrue(details.BBoxLLY == -289, "BBoxLLY is not correct");
            Assert.IsTrue(details.BBoxURX == 1534, "BBoxURX is not correct");
            Assert.IsTrue(details.BBoxURY == 1165, "BBoxURY is not correct");
            Assert.IsTrue(details.CapHeight == 700, "CapHeight is not correct");
            Assert.IsNotNull(details.CharacterWidths, "CharacterWidths should not be null");
            Assert.IsTrue(details.CharacterWidths.Count == 192, "CharacterWidths count is not correct");
            Assert.IsTrue(details.Descender == -231, "Descender is not correct");
            Assert.IsTrue(details.EncodingScheme == "ISO10646-1", "EncodingScheme is not correct");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_Symbol()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("Symbol", false, false);

            Assert.IsNotNull(details, "AFM Details should not be null");
            Assert.IsTrue(details.FontName == "Symbol", "FontName is not correct");
            Assert.IsTrue(details.Ascender == 0, "Ascender is not correct");
            Assert.IsTrue(details.BBoxLLX == -180, "BBoxLLX is not correct");
            Assert.IsTrue(details.BBoxLLY == -293, "BBoxLLY is not correct");
            Assert.IsTrue(details.BBoxURX == 1090, "BBoxURX is not correct");
            Assert.IsTrue(details.BBoxURY == 1010, "BBoxURY is not correct");
            Assert.IsTrue(details.CapHeight == 0, "CapHeight is not correct");
            Assert.IsNotNull(details.CharacterWidths, "CharacterWidths should not be null");
            Assert.IsTrue(details.CharacterWidths.Count == 189, "CharacterWidths count is not correct");
            Assert.IsTrue(details.Descender == 0, "Descender is not correct");
            Assert.IsTrue(details.EncodingScheme == "FontSpecific", "EncodingScheme is not correct");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_SymbolBold()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("Symbol", true, false);

            Assert.IsNotNull(details, "AFM Details should not be null");
            Assert.IsTrue(details.FontName == "Symbol", "FontName is not correct");
            Assert.IsTrue(details.Ascender == 0, "Ascender is not correct");
            Assert.IsTrue(details.BBoxLLX == -180, "BBoxLLX is not correct");
            Assert.IsTrue(details.BBoxLLY == -293, "BBoxLLY is not correct");
            Assert.IsTrue(details.BBoxURX == 1090, "BBoxURX is not correct");
            Assert.IsTrue(details.BBoxURY == 1010, "BBoxURY is not correct");
            Assert.IsTrue(details.CapHeight == 0, "CapHeight is not correct");
            Assert.IsNotNull(details.CharacterWidths, "CharacterWidths should not be null");
            Assert.IsTrue(details.CharacterWidths.Count == 189, "CharacterWidths count is not correct");
            Assert.IsTrue(details.Descender == 0, "Descender is not correct");
            Assert.IsTrue(details.EncodingScheme == "FontSpecific", "EncodingScheme is not correct");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_SymbolItalic()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("Symbol", false, true);

            Assert.IsNotNull(details, "AFM Details should not be null");
            Assert.IsTrue(details.FontName == "Symbol", "FontName is not correct");
            Assert.IsTrue(details.Ascender == 0, "Ascender is not correct");
            Assert.IsTrue(details.BBoxLLX == -180, "BBoxLLX is not correct");
            Assert.IsTrue(details.BBoxLLY == -293, "BBoxLLY is not correct");
            Assert.IsTrue(details.BBoxURX == 1090, "BBoxURX is not correct");
            Assert.IsTrue(details.BBoxURY == 1010, "BBoxURY is not correct");
            Assert.IsTrue(details.CapHeight == 0, "CapHeight is not correct");
            Assert.IsNotNull(details.CharacterWidths, "CharacterWidths should not be null");
            Assert.IsTrue(details.CharacterWidths.Count == 189, "CharacterWidths count is not correct");
            Assert.IsTrue(details.Descender == 0, "Descender is not correct");
            Assert.IsTrue(details.EncodingScheme == "FontSpecific", "EncodingScheme is not correct");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_SymbolBoldItalic()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("Symbol", true, true);

            Assert.IsNotNull(details, "AFM Details should not be null");
            Assert.IsTrue(details.FontName == "Symbol", "FontName is not correct");
            Assert.IsTrue(details.Ascender == 0, "Ascender is not correct");
            Assert.IsTrue(details.BBoxLLX == -180, "BBoxLLX is not correct");
            Assert.IsTrue(details.BBoxLLY == -293, "BBoxLLY is not correct");
            Assert.IsTrue(details.BBoxURX == 1090, "BBoxURX is not correct");
            Assert.IsTrue(details.BBoxURY == 1010, "BBoxURY is not correct");
            Assert.IsTrue(details.CapHeight == 0, "CapHeight is not correct");
            Assert.IsNotNull(details.CharacterWidths, "CharacterWidths should not be null");
            Assert.IsTrue(details.CharacterWidths.Count == 189, "CharacterWidths count is not correct");
            Assert.IsTrue(details.Descender == 0, "Descender is not correct");
            Assert.IsTrue(details.EncodingScheme == "FontSpecific", "EncodingScheme is not correct");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_TimesRoman()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("Times-Roman", false, false);

            Assert.IsNotNull(details, "AFM Details should not be null");
            Assert.IsTrue(details.FontName == "Times-Roman", "FontName is not correct");
            Assert.IsTrue(details.Ascender == 683, "Ascender is not correct");
            Assert.IsTrue(details.BBoxLLX == -168, "BBoxLLX is not correct");
            Assert.IsTrue(details.BBoxLLY == -218, "BBoxLLY is not correct");
            Assert.IsTrue(details.BBoxURX == 1000, "BBoxURX is not correct");
            Assert.IsTrue(details.BBoxURY == 898, "BBoxURY is not correct");
            Assert.IsTrue(details.CapHeight == 662, "CapHeight is not correct");
            Assert.IsNotNull(details.CharacterWidths, "CharacterWidths should not be null");
            Assert.IsTrue(details.CharacterWidths.Count == 149, "CharacterWidths count is not correct");
            Assert.IsTrue(details.Descender == -217, "Descender is not correct");
            Assert.IsTrue(details.EncodingScheme == "AdobeStandardEncoding", "EncodingScheme is not correct");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_TimesRomanBold()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("Times-Roman", true, false);

            Assert.IsNotNull(details, "AFM Details should not be null");
            Assert.IsTrue(details.FontName == "Times-Bold", "FontName is not correct");
            Assert.IsTrue(details.Ascender == 683, "Ascender is not correct");
            Assert.IsTrue(details.BBoxLLX == -168, "BBoxLLX is not correct");
            Assert.IsTrue(details.BBoxLLY == -218, "BBoxLLY is not correct");
            Assert.IsTrue(details.BBoxURX == 1000, "BBoxURX is not correct");
            Assert.IsTrue(details.BBoxURY == 935, "BBoxURY is not correct");
            Assert.IsTrue(details.CapHeight == 676, "CapHeight is not correct");
            Assert.IsNotNull(details.CharacterWidths, "CharacterWidths should not be null");
            Assert.IsTrue(details.CharacterWidths.Count == 149, "CharacterWidths count is not correct");
            Assert.IsTrue(details.Descender == -217, "Descender is not correct");
            Assert.IsTrue(details.EncodingScheme == "AdobeStandardEncoding", "EncodingScheme is not correct");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_TimesRomanItalic()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("Times-Roman", false, true);

            Assert.IsNotNull(details, "AFM Details should not be null");
            Assert.IsTrue(details.FontName == "Times-Italic", "FontName is not correct");
            Assert.IsTrue(details.Ascender == 683, "Ascender is not correct");
            Assert.IsTrue(details.BBoxLLX == -169, "BBoxLLX is not correct");
            Assert.IsTrue(details.BBoxLLY == -217, "BBoxLLY is not correct");
            Assert.IsTrue(details.BBoxURX == 1010, "BBoxURX is not correct");
            Assert.IsTrue(details.BBoxURY == 883, "BBoxURY is not correct");
            Assert.IsTrue(details.CapHeight == 653, "CapHeight is not correct");
            Assert.IsNotNull(details.CharacterWidths, "CharacterWidths should not be null");
            Assert.IsTrue(details.CharacterWidths.Count == 149, "CharacterWidths count is not correct");
            Assert.IsTrue(details.Descender == -217, "Descender is not correct");
            Assert.IsTrue(details.EncodingScheme == "AdobeStandardEncoding", "EncodingScheme is not correct");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_TimesRomanBoldItalic()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("Times-Roman", true, true);

            Assert.IsNotNull(details, "AFM Details should not be null");
            Assert.IsTrue(details.FontName == "Times-BoldItalic", "FontName is not correct");
            Assert.IsTrue(details.Ascender == 683, "Ascender is not correct");
            Assert.IsTrue(details.BBoxLLX == -200, "BBoxLLX is not correct");
            Assert.IsTrue(details.BBoxLLY == -218, "BBoxLLY is not correct");
            Assert.IsTrue(details.BBoxURX == 996, "BBoxURX is not correct");
            Assert.IsTrue(details.BBoxURY == 921, "BBoxURY is not correct");
            Assert.IsTrue(details.CapHeight == 669, "CapHeight is not correct");
            Assert.IsNotNull(details.CharacterWidths, "CharacterWidths should not be null");
            Assert.IsTrue(details.CharacterWidths.Count == 149, "CharacterWidths count is not correct");
            Assert.IsTrue(details.Descender == -217, "Descender is not correct");
            Assert.IsTrue(details.EncodingScheme == "AdobeStandardEncoding", "EncodingScheme is not correct");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_TimesNewRoman()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("TimesNewRomanPSMT", false, false);

            Assert.IsNotNull(details, "AFM Details should not be null");
            Assert.IsTrue(details.FontName == "TimesNewRomanPSMT", "FontName is not correct");
            Assert.IsTrue(details.Ascender == 891, "Ascender is not correct");
            Assert.IsTrue(details.BBoxLLX == -568, "BBoxLLX is not correct");
            Assert.IsTrue(details.BBoxLLY == -307, "BBoxLLY is not correct");
            Assert.IsTrue(details.BBoxURX == 2028, "BBoxURX is not correct");
            Assert.IsTrue(details.BBoxURY == 1007, "BBoxURY is not correct");
            Assert.IsTrue(details.CapHeight == 0, "CapHeight is not correct");
            Assert.IsNotNull(details.CharacterWidths, "CharacterWidths should not be null");
            Assert.IsTrue(details.CharacterWidths.Count == 255, "CharacterWidths count is not correct");
            Assert.IsTrue(details.Descender == -216, "Descender is not correct");
            Assert.IsTrue(details.EncodingScheme == "FontSpecific", "EncodingScheme is not correct");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_TimesNewRomanBold()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("TimesNewRomanPSMT", true, false);

            Assert.IsNotNull(details, "AFM Details should not be null");
            Assert.IsTrue(details.FontName == "TimesNewRomanPSMT", "FontName is not correct");
            Assert.IsTrue(details.Ascender == 891, "Ascender is not correct");
            Assert.IsTrue(details.BBoxLLX == -568, "BBoxLLX is not correct");
            Assert.IsTrue(details.BBoxLLY == -307, "BBoxLLY is not correct");
            Assert.IsTrue(details.BBoxURX == 2028, "BBoxURX is not correct");
            Assert.IsTrue(details.BBoxURY == 1007, "BBoxURY is not correct");
            Assert.IsTrue(details.CapHeight == 0, "CapHeight is not correct");
            Assert.IsNotNull(details.CharacterWidths, "CharacterWidths should not be null");
            Assert.IsTrue(details.CharacterWidths.Count == 255, "CharacterWidths count is not correct");
            Assert.IsTrue(details.Descender == -216, "Descender is not correct");
            Assert.IsTrue(details.EncodingScheme == "FontSpecific", "EncodingScheme is not correct");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_TimesNewRomanItalic()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("TimesNewRomanPSMT", false, true);

            Assert.IsNotNull(details, "AFM Details should not be null");
            Assert.IsTrue(details.FontName == "TimesNewRomanPSMT", "FontName is not correct");
            Assert.IsTrue(details.Ascender == 891, "Ascender is not correct");
            Assert.IsTrue(details.BBoxLLX == -568, "BBoxLLX is not correct");
            Assert.IsTrue(details.BBoxLLY == -307, "BBoxLLY is not correct");
            Assert.IsTrue(details.BBoxURX == 2028, "BBoxURX is not correct");
            Assert.IsTrue(details.BBoxURY == 1007, "BBoxURY is not correct");
            Assert.IsTrue(details.CapHeight == 0, "CapHeight is not correct");
            Assert.IsNotNull(details.CharacterWidths, "CharacterWidths should not be null");
            Assert.IsTrue(details.CharacterWidths.Count == 255, "CharacterWidths count is not correct");
            Assert.IsTrue(details.Descender == -216, "Descender is not correct");
            Assert.IsTrue(details.EncodingScheme == "FontSpecific", "EncodingScheme is not correct");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_TimesNewRomanBoldItalic()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("TimesNewRomanPSMT", true, true);

            Assert.IsNotNull(details, "AFM Details should not be null");
            Assert.IsTrue(details.FontName == "TimesNewRomanPSMT", "FontName is not correct");
            Assert.IsTrue(details.Ascender == 891, "Ascender is not correct");
            Assert.IsTrue(details.BBoxLLX == -568, "BBoxLLX is not correct");
            Assert.IsTrue(details.BBoxLLY == -307, "BBoxLLY is not correct");
            Assert.IsTrue(details.BBoxURX == 2028, "BBoxURX is not correct");
            Assert.IsTrue(details.BBoxURY == 1007, "BBoxURY is not correct");
            Assert.IsTrue(details.CapHeight == 0, "CapHeight is not correct");
            Assert.IsNotNull(details.CharacterWidths, "CharacterWidths should not be null");
            Assert.IsTrue(details.CharacterWidths.Count == 255, "CharacterWidths count is not correct");
            Assert.IsTrue(details.Descender == -216, "Descender is not correct");
            Assert.IsTrue(details.EncodingScheme == "FontSpecific", "EncodingScheme is not correct");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_ZapDingBats()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("ITC Zapf Dingbats", false, false);

            Assert.IsNotNull(details, "AFM Details should not be null");
            Assert.IsTrue(details.FontName == "ZapfDingbats", "FontName is not correct");
            Assert.IsTrue(details.Ascender == 0, "Ascender is not correct");
            Assert.IsTrue(details.BBoxLLX == -1, "BBoxLLX is not correct");
            Assert.IsTrue(details.BBoxLLY == -143, "BBoxLLY is not correct");
            Assert.IsTrue(details.BBoxURX == 981, "BBoxURX is not correct");
            Assert.IsTrue(details.BBoxURY == 820, "BBoxURY is not correct");
            Assert.IsTrue(details.CapHeight == 0, "CapHeight is not correct");
            Assert.IsNotNull(details.CharacterWidths, "CharacterWidths should not be null");
            Assert.IsTrue(details.CharacterWidths.Count == 202, "CharacterWidths count is not correct");
            Assert.IsTrue(details.Descender == 0, "Descender is not correct");
            Assert.IsTrue(details.EncodingScheme == "FontSpecific", "EncodingScheme is not correct");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_ZapDingBatsBold()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("ITC Zapf Dingbats", true, false);

            Assert.IsNotNull(details, "AFM Details should not be null");
            Assert.IsTrue(details.FontName == "ZapfDingbats", "FontName is not correct");
            Assert.IsTrue(details.Ascender == 0, "Ascender is not correct");
            Assert.IsTrue(details.BBoxLLX == -1, "BBoxLLX is not correct");
            Assert.IsTrue(details.BBoxLLY == -143, "BBoxLLY is not correct");
            Assert.IsTrue(details.BBoxURX == 981, "BBoxURX is not correct");
            Assert.IsTrue(details.BBoxURY == 820, "BBoxURY is not correct");
            Assert.IsTrue(details.CapHeight == 0, "CapHeight is not correct");
            Assert.IsNotNull(details.CharacterWidths, "CharacterWidths should not be null");
            Assert.IsTrue(details.CharacterWidths.Count == 202, "CharacterWidths count is not correct");
            Assert.IsTrue(details.Descender == 0, "Descender is not correct");
            Assert.IsTrue(details.EncodingScheme == "FontSpecific", "EncodingScheme is not correct");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_ZapDingBatsItalic()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("ITC Zapf Dingbats", false, true);

            Assert.IsNotNull(details, "AFM Details should not be null");
            Assert.IsTrue(details.FontName == "ZapfDingbats", "FontName is not correct");
            Assert.IsTrue(details.Ascender == 0, "Ascender is not correct");
            Assert.IsTrue(details.BBoxLLX == -1, "BBoxLLX is not correct");
            Assert.IsTrue(details.BBoxLLY == -143, "BBoxLLY is not correct");
            Assert.IsTrue(details.BBoxURX == 981, "BBoxURX is not correct");
            Assert.IsTrue(details.BBoxURY == 820, "BBoxURY is not correct");
            Assert.IsTrue(details.CapHeight == 0, "CapHeight is not correct");
            Assert.IsNotNull(details.CharacterWidths, "CharacterWidths should not be null");
            Assert.IsTrue(details.CharacterWidths.Count == 202, "CharacterWidths count is not correct");
            Assert.IsTrue(details.Descender == 0, "Descender is not correct");
            Assert.IsTrue(details.EncodingScheme == "FontSpecific", "EncodingScheme is not correct");
        }

        [TestMethod]
        public void GetFontMetricsByNameAndAttributes_ZapDingBatsBoldItalic()
        {
            AFMDetails details = AFMCache.Instance.GetFontMetricsByNameAndAttributes("ITC Zapf Dingbats", true, true);

            Assert.IsNotNull(details, "AFM Details should not be null");
            Assert.IsTrue(details.FontName == "ZapfDingbats", "FontName is not correct");
            Assert.IsTrue(details.Ascender == 0, "Ascender is not correct");
            Assert.IsTrue(details.BBoxLLX == -1, "BBoxLLX is not correct");
            Assert.IsTrue(details.BBoxLLY == -143, "BBoxLLY is not correct");
            Assert.IsTrue(details.BBoxURX == 981, "BBoxURX is not correct");
            Assert.IsTrue(details.BBoxURY == 820, "BBoxURY is not correct");
            Assert.IsTrue(details.CapHeight == 0, "CapHeight is not correct");
            Assert.IsNotNull(details.CharacterWidths, "CharacterWidths should not be null");
            Assert.IsTrue(details.CharacterWidths.Count == 202, "CharacterWidths count is not correct");
            Assert.IsTrue(details.Descender == 0, "Descender is not correct");
            Assert.IsTrue(details.EncodingScheme == "FontSpecific", "EncodingScheme is not correct");
        }
    }
}
