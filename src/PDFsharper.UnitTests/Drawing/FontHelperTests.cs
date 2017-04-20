using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfSharper.Pdf.AcroForms;
using PdfSharper.Pdf;
using System.IO;
using PdfSharper.Pdf.Annotations;
using PdfSharper.Drawing;
using System.Drawing;
using System.Collections.Generic;

namespace PDFsharper.UnitTests.Drawing
{
    [TestClass]
    public class FontHelperTests
    {
        [TestMethod]
        public void MeasureString_Courier()
        {
            XFont font = new XFont("Courier", 10.0);
            XSize size = FontHelper.MeasureString("test", font);

            Assert.IsNotNull(size, "size should not be null");
            Assert.IsTrue(size.Width > 0, "size.Width should be greater than zero");
            Assert.IsTrue(size.Height > 0, "size.Height should be greater than zero");
        }

        [TestMethod]
        public void MeasureString_CourierBoldOblique()
        {
            XFont font = new XFont("Courier", 10.0, XFontStyle.BoldItalic);
            XSize size = FontHelper.MeasureString("test", font);

            Assert.IsNotNull(size, "size should not be null");
            Assert.IsTrue(size.Width > 0, "size.Width should be greater than zero");
            Assert.IsTrue(size.Height > 0, "size.Height should be greater than zero");
        }

        [TestMethod]
        public void MeasureString_CourierBold()
        {
            XFont font = new XFont("Courier", 10.0, XFontStyle.Bold);
            XSize size = FontHelper.MeasureString("test", font);

            Assert.IsNotNull(size, "size should not be null");
            Assert.IsTrue(size.Width > 0, "size.Width should be greater than zero");
            Assert.IsTrue(size.Height > 0, "size.Height should be greater than zero");
        }

        [TestMethod]
        public void MeasureString_CourierOblique()
        {
            XFont font = new XFont("Courier", 10.0, XFontStyle.Italic);
            XSize size = FontHelper.MeasureString("test", font);

            Assert.IsNotNull(size, "size should not be null");
            Assert.IsTrue(size.Width > 0, "size.Width should be greater than zero");
            Assert.IsTrue(size.Height > 0, "size.Height should be greater than zero");
        }

        [TestMethod]
        public void MeasureString_Helvetica()
        {
            XFont font = new XFont("Helvetica", 10.0);
            XSize size = FontHelper.MeasureString("test", font);

            Assert.IsNotNull(size, "size should not be null");
            Assert.IsTrue(size.Width > 0, "size.Width should be greater than zero");
            Assert.IsTrue(size.Height > 0, "size.Height should be greater than zero");
        }

        [TestMethod]
        public void MeasureString_HelveticaBold()
        {
            XFont font = new XFont("Helvetica", 10.0, XFontStyle.Bold);
            XSize size = FontHelper.MeasureString("test", font);

            Assert.IsNotNull(size, "size should not be null");
            Assert.IsTrue(size.Width > 0, "size.Width should be greater than zero");
            Assert.IsTrue(size.Height > 0, "size.Height should be greater than zero");
        }

        [TestMethod]
        public void MeasureString_HelveticaBoldOblique()
        {
            XFont font = new XFont("Helvetica", 10.0, XFontStyle.BoldItalic);
            XSize size = FontHelper.MeasureString("test", font);

            Assert.IsNotNull(size, "size should not be null");
            Assert.IsTrue(size.Width > 0, "size.Width should be greater than zero");
            Assert.IsTrue(size.Height > 0, "size.Height should be greater than zero");
        }

        [TestMethod]
        public void MeasureString_HelveticaOblique()
        {
            XFont font = new XFont("Helvetica", 10.0, XFontStyle.Italic);
            XSize size = FontHelper.MeasureString("test", font);

            Assert.IsNotNull(size, "size should not be null");
            Assert.IsTrue(size.Width > 0, "size.Width should be greater than zero");
            Assert.IsTrue(size.Height > 0, "size.Height should be greater than zero");
        }

        [TestMethod]
        public void MeasureString_Symbol()
        {
            XFont font = new XFont("Symbol", 10.0);
            XSize size = FontHelper.MeasureString("test", font);

            Assert.IsNotNull(size, "size should not be null");
            Assert.IsTrue(size.Width > 0, "size.Width should be greater than zero");
            Assert.IsTrue(size.Height > 0, "size.Height should be greater than zero");
        }

        [TestMethod]
        public void MeasureString_TimesBold()
        {
            XFont font = new XFont("Times-Roman", 10.0, XFontStyle.Bold);
            XSize size = FontHelper.MeasureString("test", font);

            Assert.IsNotNull(size, "size should not be null");
            Assert.IsTrue(size.Width > 0, "size.Width should be greater than zero");
            Assert.IsTrue(size.Height > 0, "size.Height should be greater than zero");
        }

        [TestMethod]
        public void MeasureString_TimesBoldItalic()
        {
            XFont font = new XFont("Times-Roman", 10.0, XFontStyle.BoldItalic);
            XSize size = FontHelper.MeasureString("test", font);

            Assert.IsNotNull(size, "size should not be null");
            Assert.IsTrue(size.Width > 0, "size.Width should be greater than zero");
            Assert.IsTrue(size.Height > 0, "size.Height should be greater than zero");
        }

        [TestMethod]
        public void MeasureString_TimesItalic()
        {
            XFont font = new XFont("Times-Roman", 10.0, XFontStyle.Italic);
            XSize size = FontHelper.MeasureString("test", font);

            Assert.IsNotNull(size, "size should not be null");
            Assert.IsTrue(size.Width > 0, "size.Width should be greater than zero");
            Assert.IsTrue(size.Height > 0, "size.Height should be greater than zero");
        }

        [TestMethod]
        public void MeasureString_TimesNewRoman()
        {
            XFont font = new XFont("Times New Roman", 10.0);
            XSize size = FontHelper.MeasureString("test", font);

            Assert.IsNotNull(size, "size should not be null");
            Assert.IsTrue(size.Width > 0, "size.Width should be greater than zero");
            Assert.IsTrue(size.Height > 0, "size.Height should be greater than zero");
        }

        [TestMethod]
        public void MeasureString_TimesRoman()
        {
            XFont font = new XFont("Times-Roman", 10.0);
            XSize size = FontHelper.MeasureString("test", font);

            Assert.IsNotNull(size, "size should not be null");
            Assert.IsTrue(size.Width > 0, "size.Width should be greater than zero");
            Assert.IsTrue(size.Height > 0, "size.Height should be greater than zero");
        }

        [TestMethod]
        public void MeasureString_ZapfDingbats()
        {
            XFont font = new XFont("ZapfDingbats", 10.0);
            XSize size = FontHelper.MeasureString("test", font);

            Assert.IsNotNull(size, "size should not be null");
            Assert.IsTrue(size.Width > 0, "size.Width should be greater than zero");
            Assert.IsTrue(size.Height > 0, "size.Height should be greater than zero");
        }

        [TestMethod]
        public void MeasureString()
        {
            XFont font = new XFont("Helvetica", 10.0);

            XSize size1 = FontHelper.MeasureString("test", font);
            Assert.IsNotNull(size1, "size1 should not be null");
            Assert.IsTrue(size1.Width > 0, "size1.Width should be greater than zero");
            Assert.IsTrue(size1.Height > 0, "size1.Height should be greater than zero");

            XSize size2 = FontHelper.MeasureString("T ", font);
            Assert.IsNotNull(size2, "size2 should not be null");
            Assert.IsTrue(size2.Width > 0, "size2.Width should be greater than zero");
            Assert.IsTrue(size2.Height > 0, "size2.Height should be greater than zero");

            XSize size3 = FontHelper.MeasureString("e ", font);
            Assert.IsNotNull(size3, "size3 should not be null");
            Assert.IsTrue(size3.Width > 0, "size3.Width should be greater than zero");
            Assert.IsTrue(size3.Height > 0, "size3.Height should be greater than zero");

            XSize size4 = FontHelper.MeasureString("s ", font);
            Assert.IsNotNull(size4, "size4 should not be null");
            Assert.IsTrue(size4.Width > 0, "size4.Width should be greater than zero");
            Assert.IsTrue(size4.Height > 0, "size4.Height should be greater than zero");

            XSize size5 = FontHelper.MeasureString("t ", font);
            Assert.IsNotNull(size5, "size5 should not be null");
            Assert.IsTrue(size5.Width > 0, "size5.Width should be greater than zero");
            Assert.IsTrue(size5.Height > 0, "size5.Height should be greater than zero");

            XSize sizeSpace = FontHelper.MeasureString(" ", font);
            Assert.IsNotNull(sizeSpace, "sizeSpace should not be null");
            Assert.IsTrue(sizeSpace.Width > 0, "sizeSpace.Width should be greater than zero");
            Assert.IsTrue(sizeSpace.Height > 0, "sizeSpace.Height should be greater than zero");

            XSize sizeEmpty = FontHelper.MeasureString("", font);
            Assert.IsNotNull(sizeEmpty, "sizeEmpty should not be null");
            Assert.IsTrue(sizeEmpty.Width == 0, "sizeEmpty.Width should be zero");
            Assert.IsTrue(sizeEmpty.Height == 0, "sizeEmpty.Height should be zero");
        }

        [TestMethod]
        public void MeasureString_Text_Empty()
        {
            XFont font = new XFont("Times New Roman", 10.0);
            XSize size = FontHelper.MeasureString(string.Empty, font);

            Assert.IsTrue(size != null, "size should not be null");
            Assert.IsTrue(size.Width == 0, "Width should be 0");
            Assert.IsTrue(size.Height == 0, "Height should be 0");
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Test fails because ArgumentNullException was not thrown.")]
        public void MeasureString_Text_Null()
        {
            XFont font = new XFont("Times New Roman", 10.0);
            XSize size = FontHelper.MeasureString(null, font);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Test fails because ArgumentNullException was not thrown.")]
        public void MeasureString_Font_Null()
        {
            XSize size = FontHelper.MeasureString("test", null);
        }

        [TestMethod]
        public void CalcChecksum_AllZeroBuffer()
        {
            byte[] stream = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            ulong checkSum = FontHelper.CalcChecksum(stream);

            Assert.IsTrue(checkSum == 10, "checkSum should be 10, but {0} returned", checkSum);
        }

        [TestMethod]
        public void CalcChecksum_EmptyBuffer()
        {
            byte[] stream = new byte[0];

            ulong checkSum = FontHelper.CalcChecksum(stream);

            Assert.IsTrue(checkSum == 0, "checkSum should be 0");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Test fails because ArgumentNullException was not thrown.")]
        public void CalcChecksum_NullBuffer()
        {
            FontHelper.CalcChecksum(null);
        }

        [TestMethod]
        public void CreateStyle_NotBold_NotItalic()
        {
            bool isBold = false;
            bool isItalic = false;

            XFontStyle fontStyle = FontHelper.CreateStyle(isBold, isItalic);

            Assert.IsTrue(fontStyle == 0, "fontStyle should be 0");
        }

        [TestMethod]
        public void CreateStyle_IsBold_NotItalic()
        {
            bool isBold = true;
            bool isItalic = false;

            XFontStyle fontStyle = FontHelper.CreateStyle(isBold, isItalic);

            Assert.IsTrue(fontStyle == XFontStyle.Bold, "fontStyle should be Bold");
        }

        [TestMethod]
        public void CreateStyle_NotBold_IsItalic()
        {
            bool isBold = false;
            bool isItalic = true;

            XFontStyle fontStyle = FontHelper.CreateStyle(isBold, isItalic);

            Assert.IsTrue(fontStyle == XFontStyle.Italic, "fontStyle should be Italic");
        }

        [TestMethod]
        public void CreateStyle_IsBold_IsItalic()
        {
            bool isBold = true;
            bool isItalic = true;

            XFontStyle fontStyle = FontHelper.CreateStyle(isBold, isItalic);

            Assert.IsTrue(fontStyle == XFontStyle.BoldItalic, "fontStyle should be BoldItalic");
        }
    }
}
