using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfSharper.Drawing;
using System;

namespace PDFsharper.UnitTests.Drawing
{
    [TestClass]
    public class XFontTests 
    {
        [TestMethod]
        public void MapFamilyNameToSystemFontName_Arial()
        {
            XFont font = new XFont("Arial", 10.0);
            Assert.AreEqual("ArialMT", font.ContentFontName);
        }

        [TestMethod]
        public void MapFamilyNameToSystemFontName_ArialBoldOblique()
        {
            XFont font = new XFont("ArialBoldItalic", 10.0, XFontStyle.BoldItalic);
            Assert.AreEqual("Arial-BoldItalicMT", font.ContentFontName);
        }

        [TestMethod]
        public void MapFamilyNameToSystemFontName_ArialBold()
        {
            XFont font = new XFont("ArialBold", 10.0, XFontStyle.Bold);
            Assert.AreEqual("Arial-BoldMT", font.ContentFontName);
        }

        [TestMethod]
        public void MapFamilyNameToSystemFontName_ArialOblique()
        {
            XFont font = new XFont("ArialItalic", 10.0, XFontStyle.Italic);
            Assert.AreEqual("Arial-ItalicMT", font.ContentFontName);
        }

        [TestMethod]
        public void MapFamilyNameToSystemFontName_Courier()
        {
            XFont font = new XFont("Courier", 10.0);
            Assert.AreEqual("Cour", font.ContentFontName);
        }

        [TestMethod]
        public void MapFamilyNameToSystemFontName_CourierBoldOblique()
        {
            XFont font = new XFont("Courier", 10.0, XFontStyle.BoldItalic);
            Assert.AreEqual("CoBO", font.ContentFontName);

            XFont font2 = new XFont("Courier-BoldOblique", 10.0);
            Assert.AreEqual("CoBO", font2.ContentFontName);
        }

        [TestMethod]
        public void MapFamilyNameToSystemFontName_CourierBold()
        {
            XFont font = new XFont("Courier", 10.0, XFontStyle.Bold);
            Assert.AreEqual("CoBo", font.ContentFontName);

            XFont font2 = new XFont("Courier-Bold", 10.0);
            Assert.AreEqual("CoBo", font2.ContentFontName);
        }

        [TestMethod]
        public void MapFamilyNameToSystemFontName_CourierOblique()
        {
            XFont font = new XFont("Courier", 10.0, XFontStyle.Italic);
            Assert.AreEqual("CoOb", font.ContentFontName);

            XFont font2 = new XFont("Courier-Oblique", 10.0);
            Assert.AreEqual("CoOb", font2.ContentFontName);
        }

        [TestMethod]
        public void MapFamilyNameToSystemFontName_CourierStd()
        {
            XFont font = new XFont("CourierStd", 10.0);
            Assert.AreEqual("CourierStd", font.ContentFontName);
        }

        [TestMethod]
        public void MapFamilyNameToSystemFontName_CourierNew()
        {
            XFont font = new XFont("Courier New", 10.0);
            Assert.AreEqual("Cour", font.ContentFontName);
        }

        [TestMethod]
        public void MapFamilyNameToSystemFontName_Helvetica()
        {
            XFont font = new XFont("Helvetica", 10.0);
            Assert.AreEqual("Helv", font.ContentFontName);
        }

        [TestMethod]
        public void MapFamilyNameToSystemFontName_HelveticaBold()
        {
            XFont font = new XFont("Helvetica", 10.0, XFontStyle.Bold);
            Assert.AreEqual("HeBo", font.ContentFontName);

            XFont font2 = new XFont("Helvetica-Bold", 10.0);
            Assert.AreEqual("HeBo", font2.ContentFontName);
        }

        [TestMethod]
        public void MapFamilyNameToSystemFontName_HelveticaBoldOblique()
        {
            XFont font = new XFont("Helvetica", 10.0, XFontStyle.BoldItalic);
            Assert.AreEqual("HeBO", font.ContentFontName);

            XFont font2 = new XFont("Helvetica-BoldOblique", 10.0);
            Assert.AreEqual("HeBO", font2.ContentFontName);
        }

        [TestMethod]
        public void MapFamilyNameToSystemFontName_HelveticaOblique()
        {
            XFont font = new XFont("Helvetica", 10.0, XFontStyle.Italic);
            Assert.AreEqual("HeOb", font.ContentFontName);

            XFont font2 = new XFont("Helvetica-Oblique", 10.0);
            Assert.AreEqual("HeOb", font2.ContentFontName);
        }

        [TestMethod]
        public void MapFamilyNameToSystemFontName_Symbol()
        {
            XFont font = new XFont("Symbol", 10.0);
            Assert.AreEqual("Symb", font.ContentFontName);
        }

        [TestMethod]
        public void MapFamilyNameToSystemFontName_TimesBold()
        {
            XFont font = new XFont("Times-Roman", 10.0, XFontStyle.Bold);
            Assert.AreEqual("TiBo", font.ContentFontName);

            XFont font2 = new XFont("Times-Bold", 10.0);
            Assert.AreEqual("TiBo", font2.ContentFontName);
        }

        [TestMethod]
        public void MapFamilyNameToSystemFontName_TimesBoldItalic()
        {
            XFont font = new XFont("Times-Roman", 10.0, XFontStyle.BoldItalic);
            Assert.AreEqual("TiBI", font.ContentFontName);

            XFont font2 = new XFont("Times-BoldItalic", 10.0);
            Assert.AreEqual("TiBI", font2.ContentFontName);
        }

        [TestMethod]
        public void MapFamilyNameToSystemFontName_TimesItalic()
        {
            XFont font = new XFont("Times-Roman", 10.0, XFontStyle.Italic);
            Assert.AreEqual("TiIt", font.ContentFontName);

            XFont font2 = new XFont("Times-Italic", 10.0);
            Assert.AreEqual("TiIt", font2.ContentFontName);
        }

        [TestMethod]
        public void MapFamilyNameToSystemFontName_TimesNewRoman()
        {
            XFont font = new XFont("Times New Roman", 10.0);
            Assert.AreEqual("TimesNewRomanPSMT", font.ContentFontName);
        }


        [TestMethod]
        public void MapFamilyNameToSystemFontName_TimesRoman()
        {
            XFont font = new XFont("Times-Roman", 10.0);
            Assert.AreEqual("TiRo", font.ContentFontName);

            XFont font2 = new XFont("Times", 10.0);
            Assert.AreEqual("TiRo", font2.ContentFontName);
        }

        [TestMethod]
        public void MapFamilyNameToSystemFontName_ZapfDingbats()
        {
            XFont font = new XFont("ZapfDingbats", 10.0);
            Assert.AreEqual("ZaDb", font.ContentFontName);
        }
    }
}
