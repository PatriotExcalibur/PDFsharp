using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfSharper.Pdf;
using PdfSharper.Pdf.AcroForms;
using PdfSharper.Pdf.Annotations;
using PDFsharper.UnitTests.Pdf.AcroForms;
using System;
using System.Linq;
using static PdfSharper.Pdf.PdfPage;

namespace PDFsharper.UnitTests.Pdf
{
    [TestClass]
    public class PdfPageTests
    {
        [TestMethod]
        public void Rotate_Valid()
        {
            PdfDocument doc = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfAcroField field = PdfAcroFieldTestHelpers.CreateTextFieldForTest(doc);

            doc.Pages[0].Rotate = 0;

            Assert.IsTrue(doc.Pages[0].Rotate == 0, "Rotate did not store properly");
            Assert.IsTrue(field.Page.Rotate == 0, "Rotate did not store properly");
            Assert.IsTrue(field.Page.Rotate == 0, "Rotate did not store properly");
            Assert.IsTrue(doc.Pages[0].Elements.GetInteger(InheritablePageKeys.Rotate) == 0, "Rotate did not store properly");

            doc.Pages[0].Rotate = 90;

            Assert.IsTrue(doc.Pages[0].Rotate == 90, "Rotate did not store properly");
            Assert.IsTrue(field.Page.Rotate == 90, "Rotate did not store properly");
            Assert.IsTrue(field.Page.Rotate == 90, "Rotate did not store properly");
            Assert.IsTrue(doc.Pages[0].Elements.GetInteger(InheritablePageKeys.Rotate) == 90, "Rotate did not store properly");

            doc.Pages[0].Rotate = 180;

            Assert.IsTrue(doc.Pages[0].Rotate == 180, "Rotate did not store properly");
            Assert.IsTrue(field.Page.Rotate == 180, "Rotate did not store properly");
            Assert.IsTrue(field.Page.Rotate == 180, "Rotate did not store properly");
            Assert.IsTrue(doc.Pages[0].Elements.GetInteger(InheritablePageKeys.Rotate) == 180, "Rotate did not store properly");

            doc.Pages[0].Rotate = 270;

            Assert.IsTrue(doc.Pages[0].Rotate == 270, "Rotate did not store properly");
            Assert.IsTrue(field.Page.Rotate == 270, "Rotate did not store properly");
            Assert.IsTrue(field.Page.Rotate == 270, "Rotate did not store properly");
            Assert.IsTrue(doc.Pages[0].Elements.GetInteger(InheritablePageKeys.Rotate) == 270, "Rotate did not store properly");

            doc.Pages[0].Rotate = 360;

            Assert.IsTrue(doc.Pages[0].Rotate == 0, "Rotate did not store properly");
            Assert.IsTrue(field.Page.Rotate == 0, "Rotate did not store properly");
            Assert.IsTrue(field.Page.Rotate == 0, "Rotate did not store properly");
            Assert.IsTrue(doc.Pages[0].Elements.GetInteger(InheritablePageKeys.Rotate) == 0, "Rotate did not store properly");
        }

        [TestMethod]
        public void Rotate_GreaterThan360()
        {
            PdfDocument doc = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfAcroField field = PdfAcroFieldTestHelpers.CreateTextFieldForTest(doc);

            doc.Pages[0].Rotate = 360;

            Assert.IsTrue(doc.Pages[0].Rotate == 0, "Rotate did not store properly");
            Assert.IsTrue(field.Page.Rotate == 0, "Rotate did not store properly");
            Assert.IsTrue(field.Page.Rotate == 0, "Rotate did not store properly");
            Assert.IsTrue(doc.Pages[0].Elements.GetInteger(InheritablePageKeys.Rotate) == 0, "Rotate did not store properly");

            doc.Pages[0].Rotate = 450;

            Assert.IsTrue(doc.Pages[0].Rotate == 90, "Rotate did not store properly");
            Assert.IsTrue(field.Page.Rotate == 90, "Rotate did not store properly");
            Assert.IsTrue(field.Page.Rotate == 90, "Rotate did not store properly");
            Assert.IsTrue(doc.Pages[0].Elements.GetInteger(InheritablePageKeys.Rotate) == 90, "Rotate did not store properly");

            doc.Pages[0].Rotate = 540;

            Assert.IsTrue(doc.Pages[0].Rotate == 180, "Rotate did not store properly");
            Assert.IsTrue(field.Page.Rotate == 180, "Rotate did not store properly");
            Assert.IsTrue(field.Page.Rotate == 180, "Rotate did not store properly");
            Assert.IsTrue(doc.Pages[0].Elements.GetInteger(InheritablePageKeys.Rotate) == 180, "Rotate did not store properly");

            doc.Pages[0].Rotate = 630;

            Assert.IsTrue(doc.Pages[0].Rotate == 270, "Rotate did not store properly");
            Assert.IsTrue(field.Page.Rotate == 270, "Rotate did not store properly");
            Assert.IsTrue(field.Page.Rotate == 270, "Rotate did not store properly");
            Assert.IsTrue(doc.Pages[0].Elements.GetInteger(InheritablePageKeys.Rotate) == 270, "Rotate did not store properly");

            doc.Pages[0].Rotate = 720;

            Assert.IsTrue(doc.Pages[0].Rotate == 0, "Rotate did not store properly");
            Assert.IsTrue(field.Page.Rotate == 0, "Rotate did not store properly");
            Assert.IsTrue(field.Page.Rotate == 0, "Rotate did not store properly");
            Assert.IsTrue(doc.Pages[0].Elements.GetInteger(InheritablePageKeys.Rotate) == 0, "Rotate did not store properly");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Test fails because ArgumentException was not thrown.")]
        public void Rotate_Invalid()
        {
            PdfDocument doc = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfAcroField field = PdfAcroFieldTestHelpers.CreateTextFieldForTest(doc);

            doc.Pages[0].Rotate = 2;
        }

        [TestMethod]
        public void Height_Portrait()
        {
            PdfDocument doc = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfAcroField field = PdfAcroFieldTestHelpers.CreateTextFieldForTest(doc);

            field.Page.Rotate = 0;
            field.Page.Orientation = PdfSharper.PageOrientation.Portrait;

            Assert.IsTrue(field.Page.Height == field.Page.MediaBox.Height, "Page Height is not correct");

            double x1 = field.Page.MediaBox.X1;
            double x2 = field.Page.MediaBox.X2;
            double y1 = field.Page.MediaBox.Y1;
            double y2 = field.Page.MediaBox.Y2;
            double newHeight = 543;

            field.Page.Height = newHeight;

            Assert.IsTrue(field.Page.Height == field.Page.MediaBox.Height, "Page Height is not correct");
            Assert.IsTrue(field.Page.Height == newHeight, "Page Height is not correct");
            Assert.IsTrue(field.Page.MediaBox.X1 == x1, "Media Box X1 is not correct");
            Assert.IsTrue(field.Page.MediaBox.X2 == x2, "Media Box X2 is not correct");
            Assert.IsTrue(field.Page.MediaBox.Y1 == y1, "Media Box Y1 is not correct");
            Assert.IsTrue(field.Page.MediaBox.Y2 == newHeight, "Media Box Y2 is not correct");
        }

        [TestMethod]
        public void Height_Landscape()
        {
            PdfDocument doc = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfAcroField field = PdfAcroFieldTestHelpers.CreateTextFieldForTest(doc);

            field.Page.Rotate = 90;
            field.Page.Orientation = PdfSharper.PageOrientation.Landscape;

            Assert.IsTrue(field.Page.Height == field.Page.MediaBox.Width, "Page Height is not correct");

            double x1 = field.Page.MediaBox.X1;
            double x2 = field.Page.MediaBox.X2;
            double y1 = field.Page.MediaBox.Y1;
            double y2 = field.Page.MediaBox.Y2;
            double newHeight = 543;

            field.Page.Height = newHeight;

            Assert.IsTrue(field.Page.Height == field.Page.MediaBox.Width, "Page Height is not correct");
            Assert.IsTrue(field.Page.Height == newHeight, "Page Height is not correct");
            Assert.IsTrue(field.Page.MediaBox.X1 == x1, "Media Box X1 is not correct");
            Assert.IsTrue(field.Page.MediaBox.X2 == newHeight, "Media Box X2 is not correct");
            Assert.IsTrue(field.Page.MediaBox.Y1 == y1, "Media Box Y1 is not correct");
            Assert.IsTrue(field.Page.MediaBox.Y2 == y2, "Media Box Y2 is not correct");
        }

        [TestMethod]
        public void Width_Portrait()
        {
            PdfDocument doc = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfAcroField field = PdfAcroFieldTestHelpers.CreateTextFieldForTest(doc);

            field.Page.Rotate = 0;
            field.Page.Orientation = PdfSharper.PageOrientation.Portrait;

            Assert.IsTrue(field.Page.Width == field.Page.MediaBox.Width, "Page Width is not correct");

            double x1 = field.Page.MediaBox.X1;
            double x2 = field.Page.MediaBox.X2;
            double y1 = field.Page.MediaBox.Y1;
            double y2 = field.Page.MediaBox.Y2;
            double newWidth = 543;

            field.Page.Width = newWidth;

            Assert.IsTrue(field.Page.Width == field.Page.MediaBox.Width, "Page Width is not correct");
            Assert.IsTrue(field.Page.Width == newWidth, "Page Width is not correct");
            Assert.IsTrue(field.Page.MediaBox.X1 == x1, "Media Box X1 is not correct");
            Assert.IsTrue(field.Page.MediaBox.X2 == newWidth, "Media Box X2 is not correct");
            Assert.IsTrue(field.Page.MediaBox.Y1 == y1, "Media Box Y1 is not correct");
            Assert.IsTrue(field.Page.MediaBox.Y2 == y2, "Media Box Y2 is not correct");
        }

        [TestMethod]
        public void Width_Landscape()
        {
            PdfDocument doc = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfAcroField field = PdfAcroFieldTestHelpers.CreateTextFieldForTest(doc);

            field.Page.Rotate = 90;
            field.Page.Orientation = PdfSharper.PageOrientation.Landscape;

            Assert.IsTrue(field.Page.Width == field.Page.MediaBox.Height, "Page Width is not correct");

            double x1 = field.Page.MediaBox.X1;
            double x2 = field.Page.MediaBox.X2;
            double y1 = field.Page.MediaBox.Y1;
            double y2 = field.Page.MediaBox.Y2;
            double newWidth = 543;

            field.Page.Width = newWidth;

            Assert.IsTrue(field.Page.Width == field.Page.MediaBox.Height, "Page Width is not correct");
            Assert.IsTrue(field.Page.Width == newWidth, "Page Width is not correct");
            Assert.IsTrue(field.Page.MediaBox.X1 == x1, "Media Box X1 is not correct");
            Assert.IsTrue(field.Page.MediaBox.X2 == x2, "Media Box X2 is not correct");
            Assert.IsTrue(field.Page.MediaBox.Y1 == y1, "Media Box Y1 is not correct");
            Assert.IsTrue(field.Page.MediaBox.Y2 == newWidth, "Media Box Y2 is not correct");
        }
    }
}
