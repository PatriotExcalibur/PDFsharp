using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfSharper.Drawing;
using PdfSharper.Pdf;
using PdfSharper.Pdf.AcroForms;
using PdfSharper.Pdf.Advanced;
using PdfSharper.Pdf.Annotations;
using System.Linq;

namespace PDFsharper.UnitTests.Pdf.AcroForms
{
    [TestClass]
    public class PdfAcroFieldTests
    {
        [TestMethod]
        public void DefaultFonts()
        {
            PdfDocument doc = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfAcroField field = PdfAcroFieldTestHelpers.CreateTextFieldForTest(doc);

            Assert.IsNotNull(field, "field should not be null");
            Assert.IsNotNull(field.Font, "field.Font should not be null");
            Assert.IsTrue(field.Font.FamilyName == "Helvetica", "Font FamilyName is not correct");
            Assert.IsTrue(field.Font.ContentFontName == "Helv", "Font FamilyName is not correct");
            Assert.IsTrue(field.Font.Size == 10, "Font Size is not correct");
        }

        [TestMethod]
        public void GetIsVisible()
        {
            PdfDocument doc = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfAcroField field = PdfAcroFieldTestHelpers.CreateTextFieldForTest(doc);

            bool isVisible = field.GetIsVisible();

            Assert.IsTrue(isVisible, "isVisible should be true");
        }

        [TestMethod]
        public void GetIsVisible_WithFlagsAdded()
        {
            PdfDocument doc = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfAcroField field = PdfAcroFieldTestHelpers.CreateTextFieldForTest(doc);

            PdfAnnotationFlags itemFlags = field.Flags;
            itemFlags |= ~PdfAnnotationFlags.Invisible;
            itemFlags |= ~PdfAnnotationFlags.Hidden;
            itemFlags |= ~PdfAnnotationFlags.NoView;
            field.Flags = itemFlags;

            bool isVisible = field.GetIsVisible();

            Assert.IsFalse(isVisible, "isVisible should be false");
        }

        [TestMethod]
        public void GetIsVisible_WithFlagsRemoved()
        {
            PdfDocument doc = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfAcroField field = PdfAcroFieldTestHelpers.CreateTextFieldForTest(doc);

            PdfAnnotationFlags itemFlags = field.Flags;
            itemFlags &= ~PdfAnnotationFlags.Invisible;
            itemFlags &= ~PdfAnnotationFlags.Hidden;
            itemFlags &= ~PdfAnnotationFlags.NoView;
            field.Flags = itemFlags;

            bool isVisible = field.GetIsVisible();

            Assert.IsTrue(isVisible, "isVisible should be true");

            itemFlags = field.Flags;
            itemFlags |= ~PdfAnnotationFlags.Invisible;
            itemFlags |= ~PdfAnnotationFlags.Hidden;
            itemFlags |= ~PdfAnnotationFlags.NoView;
            field.Flags = itemFlags;

            bool isNotVisible = field.GetIsVisible();

            Assert.IsFalse(isNotVisible, "isVisible should be false");
        }

        [TestMethod]
        public void RemoveJavascript()
        {
            PdfDocument document = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfAcroField field = PdfAcroFieldTestHelpers.CreateTextFieldForTest(document);

            Assert.IsFalse(document.Catalog.Elements.Any(e => e.Key == "/Names"), "document-catalog should not have a Names item");

            PdfDictionary javascript = new PdfDictionary();
            javascript.Elements.Add("/NoMatter", new PdfArray());

            PdfDictionary names = new PdfDictionary();
            names.Elements.Add("/JavaScript", javascript);

            document.Catalog.Elements.Add("/Names", names);

            Assert.IsTrue(document.Catalog.Elements.Any(e => e.Key == "/Names"), "document-catalog should have a names item");
            Assert.IsTrue(document.Catalog.Elements.GetDictionary("/Names").Elements.Any(e => e.Key == "/JavaScript"), "document-catalog should have a Javascript item");

            field.RemoveJavascript();

            Assert.IsTrue(document.Catalog.Elements.Any(e => e.Key == "/Names"), "document should have a Names item");
            Assert.IsFalse(document.Catalog.Elements.GetDictionary("/Names").Elements.Any(e => e.Key == "/JavaScript"), "document-catalog should not have a Javascript item");
        }

        [TestMethod]
        public void RemoveJavascript_NoJavascript()
        {
            PdfDocument document = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfAcroField field = PdfAcroFieldTestHelpers.CreateTextFieldForTest(document);

            Assert.IsFalse(document.Catalog.Elements.Any(e => e.Key == "/Names"), "document-catalog should not have a Names item");

            field.RemoveJavascript();

            Assert.IsFalse(document.Catalog.Elements.Any(e => e.Key == "/Names"), "document-catalog should not have a Names item");
        }

        [TestMethod]
        public void RenderContentStream_Landscape()
        {
            string targetStreamValue = "q\n0.0 1.0 -1.0 0.0 200.0 0.0 cm\n";    //This represents the Matrix transform with rotation

            PdfDocument document = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfTextField field = PdfAcroFieldTestHelpers.CreateTextFieldForTest(document);

            field.Page.Rotate = 90;
            field.Page.Orientation = PdfSharper.PageOrientation.Landscape;

            field.Text = "Test";

            document.AcroForm.Flatten();    //This method triggers RenderContentStream for each Field

            Assert.IsNotNull(document, "document should not be null");
            Assert.IsNotNull(document.Pages, "document Pages should not be null");
            Assert.IsTrue(document.Pages.Count == 1, "document Pages count is not correct");
            Assert.IsNotNull(document.Pages[0].Contents, "document Pages contents should not be null");
            Assert.IsNotNull(document.Pages[0].Contents.Elements, "Page Elements should not be null");
            Assert.IsTrue(document.Pages[0].Contents.Elements.Count == 1, "Page Elements count is incorrect");
            Assert.IsTrue((document.Pages[0].Contents.Elements.Items[0] as PdfReference) != null, "Page Element should be a PdfReference");
            Assert.IsTrue(((document.Pages[0].Contents.Elements.Items[0] as PdfReference).Value as PdfDictionary) != null, "PdfReference Value should be a PdfDictionary");
            Assert.IsTrue(((document.Pages[0].Contents.Elements.Items[0] as PdfReference).Value as PdfDictionary).Stream != null, "PdfDictionary Stream should not be null");
            Assert.IsNotNull(((document.Pages[0].Contents.Elements.Items[0] as PdfReference).Value as PdfDictionary).Stream.Value, "PdfDictionary Stream Value should not be null");

            string stringRepresentationOfStream = System.Text.Encoding.UTF8.GetString(((document.Pages[0].Contents.Elements.Items[0] as PdfReference).Value as PdfDictionary).Stream.Value);

            Assert.IsNotNull(stringRepresentationOfStream, "stringRepresentationOfStream should not be null");
            Assert.IsTrue(stringRepresentationOfStream.StartsWith(targetStreamValue), "Stream value is not correct");
        }

        [TestMethod]
        public void RenderContentStream_Portrait()
        {
            string targetStreamValue = "q\n1.0 0.0 0.0 1.0 0.0 0.0 cm\n";   //This represnts the Matrix transform for no rotation

            PdfDocument document = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfTextField field = PdfAcroFieldTestHelpers.CreateTextFieldForTest(document);

            field.Page.Rotate = 0;
            field.Page.Orientation = PdfSharper.PageOrientation.Portrait;

            field.Text = "Test";

            document.AcroForm.Flatten();    //This method triggers RenderContentStream for each Field

            Assert.IsNotNull(document, "document should not be null");
            Assert.IsNotNull(document.Pages, "document Pages should not be null");
            Assert.IsTrue(document.Pages.Count == 1, "document Pages count is not correct");
            Assert.IsNotNull(document.Pages[0].Contents, "document Pages contents should not be null");
            Assert.IsNotNull(document.Pages[0].Contents.Elements, "Page Elements should not be null");
            Assert.IsTrue(document.Pages[0].Contents.Elements.Count == 1, "Page Elements count is incorrect");
            Assert.IsTrue((document.Pages[0].Contents.Elements.Items[0] as PdfReference) != null, "Page Element should be a PdfReference");
            Assert.IsTrue(((document.Pages[0].Contents.Elements.Items[0] as PdfReference).Value as PdfDictionary) != null, "PdfReference Value should be a PdfDictionary");
            Assert.IsTrue(((document.Pages[0].Contents.Elements.Items[0] as PdfReference).Value as PdfDictionary).Stream != null, "PdfDictionary Stream should not be null");
            Assert.IsNotNull(((document.Pages[0].Contents.Elements.Items[0] as PdfReference).Value as PdfDictionary).Stream.Value, "PdfDictionary Stream Value should not be null");

            string stringRepresentationOfStream = System.Text.Encoding.UTF8.GetString(((document.Pages[0].Contents.Elements.Items[0] as PdfReference).Value as PdfDictionary).Stream.Value);

            Assert.IsNotNull(stringRepresentationOfStream, "stringRepresentationOfStream should not be null");
            Assert.IsTrue(stringRepresentationOfStream.StartsWith(targetStreamValue), "Stream value is not correct");
        }

        [TestMethod]
        public void RenderContentStream_WithBorder()
        {
            string targetStreamValue = "q\n1.0 0.0 0.0 1.0 0.0 0.0 cm\n0 G\n";   //Check for color space going to greyscale

            PdfDocument document = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfTextField field = PdfAcroFieldTestHelpers.CreateTextFieldForTest(document);
            field.BorderColor = XColor.FromArgb(0, 0, 0);

            field.Page.Rotate = 0;
            field.Page.Orientation = PdfSharper.PageOrientation.Portrait;

            field.Text = "Test";

            document.AcroForm.Flatten();    //This method triggers RenderContentStream for each Field

            Assert.IsNotNull(document, "document should not be null");
            Assert.IsNotNull(document.Pages, "document Pages should not be null");
            Assert.IsTrue(document.Pages.Count == 1, "document Pages count is not correct");
            Assert.IsNotNull(document.Pages[0].Contents, "document Pages contents should not be null");
            Assert.IsNotNull(document.Pages[0].Contents.Elements, "Page Elements should not be null");
            Assert.IsTrue(document.Pages[0].Contents.Elements.Count == 1, "Page Elements count is incorrect");
            Assert.IsTrue((document.Pages[0].Contents.Elements.Items[0] as PdfReference) != null, "Page Element should be a PdfReference");
            Assert.IsTrue(((document.Pages[0].Contents.Elements.Items[0] as PdfReference).Value as PdfDictionary) != null, "PdfReference Value should be a PdfDictionary");
            Assert.IsTrue(((document.Pages[0].Contents.Elements.Items[0] as PdfReference).Value as PdfDictionary).Stream != null, "PdfDictionary Stream should not be null");
            Assert.IsNotNull(((document.Pages[0].Contents.Elements.Items[0] as PdfReference).Value as PdfDictionary).Stream.Value, "PdfDictionary Stream Value should not be null");

            string stringRepresentationOfStream = System.Text.Encoding.UTF8.GetString(((document.Pages[0].Contents.Elements.Items[0] as PdfReference).Value as PdfDictionary).Stream.Value);

            Assert.IsNotNull(stringRepresentationOfStream, "stringRepresentationOfStream should not be null");
            Assert.IsTrue(stringRepresentationOfStream.StartsWith(targetStreamValue), "Stream value is not correct");
        }
    }
}
