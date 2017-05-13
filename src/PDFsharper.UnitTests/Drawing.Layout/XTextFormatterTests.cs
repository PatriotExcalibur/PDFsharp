using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfSharper.Drawing;
using PdfSharper.Drawing.Layout;
using PdfSharper.Fonts;
using PdfSharper.Pdf;
using PdfSharper.Pdf.AcroForms;
using PdfSharper.Pdf.IO;
using System;

namespace PDFsharper.UnitTests.Drawing.Layout
{
    [TestClass]
    public class XTextFormatterTests 
    {
        [TestMethod]
        public void Defaults()
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter formatter = new XTextFormatter(gfx);

            Assert.IsTrue(formatter.Text == string.Empty, "Default value of Text should be empty string");
            Assert.IsTrue(formatter.WrapText == false, "Default value of WrapText should be false");
        }
        [TestMethod]
        public void DrawString()
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter formatter = new XTextFormatter(gfx);
            PdfRectangle rect = new PdfRectangle(0, 0, 100, 100);
            XFont font = new XFont("Courier", 9.0);

            formatter.DrawString("Test", font, new XSolidBrush(new XColor(XKnownColor.Blue)), rect.ToXRect(), XStringFormats.Center);

            Assert.IsTrue(formatter.Text == "Test", "Text was not set correctly.");
            Assert.IsTrue(formatter.Font.FamilyName == "Courier", "Font Family was not set correctly");
            Assert.IsTrue(formatter.Font.Size == 9, "Font Size was not set correctly");
            Assert.IsTrue(formatter.LayoutRectangle != null, "LayoutRectangle was not initialized");
            Assert.IsTrue(formatter.LayoutRectangle.TopLeft != null, "LayoutRectangle.TopLeft was not initialized");
            Assert.IsTrue(formatter.LayoutRectangle.TopLeft.X == 0, "TopLeft.X was not set correctly");
            Assert.IsTrue(formatter.LayoutRectangle.TopLeft.Y == 0, "TopLeft.Y was not set correctly");
            Assert.IsTrue(formatter.LayoutRectangle.TopRight != null, "LayoutRectangle.TopRight was not initialized");
            Assert.IsTrue(formatter.LayoutRectangle.TopRight.X == 100, "TopRight.X was not set correctly");
            Assert.IsTrue(formatter.LayoutRectangle.TopRight.Y == 0, "TopRight.Y was not set correctly");
            Assert.IsTrue(formatter.LayoutRectangle.BottomLeft != null, "LayoutRectangle.BottomLeft was not initialized");
            Assert.IsTrue(formatter.LayoutRectangle.BottomLeft.X == 0, "BottomLeft.X was not set correctly");
            Assert.IsTrue(formatter.LayoutRectangle.BottomLeft.Y == 100, "BottomLeft.Y was not set correctly");
            Assert.IsTrue(formatter.LayoutRectangle.BottomRight != null, "LayoutRectangle.BottomRight was not initialized");
            Assert.IsTrue(formatter.LayoutRectangle.BottomRight.X == 100, "BottomRight.X was not set correctly");
            Assert.IsTrue(formatter.LayoutRectangle.BottomRight.Y == 100, "BottomRight.Y was not set correctly");
        }

        ///
        /// This is for manual testing.  Requires a document on local system with fields.
        ///
        //[TestMethod]
        //public void DrawString_AllAlignments()
        //{
        //    GlobalFontSettings.DefaultFontEncoding = PdfFontEncoding.WinAnsi;
        //    PdfDocument doc = PdfReader.Open(@"c:\users\simsr\desktop\alignments.pdf");

        //    (doc.AcroForm.Fields["LeftSingle"] as PdfTextField).Text = "abcdefghijkl mnopqrstuvwxyz";
        //    (doc.AcroForm.Fields["CenterSingle"] as PdfTextField).Text = "2 abcdefghijkl mnopqrstuvwxyz";
        //    (doc.AcroForm.Fields["RightSingle"] as PdfTextField).Text = "3 abcdefghijkl mnopqrstuvwxyz";
        //    (doc.AcroForm.Fields["Page1"]["LeftMulti"] as PdfTextField).Text = "Test Test test test test test tests tese" + Environment.NewLine + "Line 2";
        //    (doc.AcroForm.Fields["Page1"]["CenterMulti"] as PdfTextField).Text = "Test" + Environment.NewLine + "Line 2";
        //    (doc.AcroForm.Fields["Page1"]["RightMulti"] as PdfTextField).Text = "Test" + Environment.NewLine + "Line 2";
        //    (doc.AcroForm.Fields["Page1"]["LeftSingle2"] as PdfTextField).Text = "1";
        //    (doc.AcroForm.Fields["Page1"]["CenterSingle2"] as PdfTextField).Text = "2";
        //    (doc.AcroForm.Fields["Page1"]["RightSingle2"] as PdfTextField).Text = "3";

        //    (doc.AcroForm.Fields["A"].Fields[1] as PdfCheckBoxField).Checked = true;
        //    (doc.AcroForm.Fields["B"].Fields[1] as PdfCheckBoxField).Checked = true;
        //    (doc.AcroForm.Fields["C"].Fields[1] as PdfCheckBoxField).Checked = true;
        //    (doc.AcroForm.Fields["D"].Fields[1] as PdfCheckBoxField).Checked = true;
        //    (doc.AcroForm.Fields["E"].Fields[1] as PdfCheckBoxField).Checked = true;
        //    (doc.AcroForm.Fields["F"].Fields[1] as PdfCheckBoxField).Checked = true;

        //    //doc.AcroForm.Flatten();

        //    doc.Save(@"c:\users\simsr\desktop\flat1.pdf");
        //}

        //[TestMethod]
        //public void DrawString_Landscape_AllAlignments()
        //{
        //    GlobalFontSettings.DefaultFontEncoding = PdfFontEncoding.WinAnsi;
        //    PdfDocument doc = PdfReader.Open(@"c:\users\simsr\desktop\LandscapeAF3862.pdf");

        //    (doc.AcroForm.Fields["Page1"]["NAME"] as PdfTextField).Text = "NAME Field";
        //    (doc.AcroForm.Fields["Page1"]["GRADE"] as PdfTextField).Text = "GRADE Field";
        //    (doc.AcroForm.Fields["Page1"]["SSN"] as PdfTextField).Text = "SSN Field";
        //    (doc.AcroForm.Fields["Page1"]["YesRstrn"] as PdfCheckBoxField).Checked = true;

        //    //doc.AcroForm.Flatten();

        //    doc.Save(@"c:\users\simsr\desktop\flat2.pdf");
        //}
    }
}
