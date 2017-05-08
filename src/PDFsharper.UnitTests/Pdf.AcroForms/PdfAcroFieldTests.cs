using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfSharper.Pdf;
using PdfSharper.Pdf.AcroForms;
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
            Assert.IsTrue(field.Font.FamilyName == "Helv", "Font FamilyName is not correct");
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
    }
}
