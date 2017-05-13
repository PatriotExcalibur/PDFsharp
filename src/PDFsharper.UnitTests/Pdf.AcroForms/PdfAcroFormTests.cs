using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfSharper.Pdf;
using PdfSharper.Pdf.AcroForms;
using PdfSharper.Pdf.Advanced;
using PdfSharper.Pdf.Annotations;
using System.Linq;
using System.Collections.Generic;

namespace PDFsharper.UnitTests.Pdf.AcroForms
{
    [TestClass]
    public class PdfAcroFormTests 
    {
        [TestMethod]
        public void Flatten()
        {
            string fieldValue = "Test";
            string targetStreamValue = "q\n1.0 0.0 0.0 1.0 0.0 0.0 cm\n1 w\n0 J\n0 j\n[]0 d\n1 1 1 RG\n/GS0 gs\n0 0 200 20 re\nS\nq\n/Tx BMC\nBT\n/" + PdfAcroFieldTestHelpers.FONT_NAME + " 10 Tf\n0 g\n2 6.6793 Td\n(" + fieldValue + ")Tj\nET\nEMC\nQ\nQ\n";

            PdfDocument document = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfTextField field1 = PdfAcroFieldTestHelpers.CreateTextFieldForTest(document);

            field1.Text = fieldValue;

            Assert.IsNotNull(document);
            Assert.IsNotNull(document.Pages);
            Assert.IsTrue(document.Pages.Count == 1);
            Assert.IsNotNull(document.Pages[0].Contents);
            Assert.IsNotNull(document.Pages[0].Contents.Elements);
            Assert.IsTrue(document.Pages[0].Contents.Elements.Count == 0);

            document.AcroForm.Flatten();

            Assert.IsTrue(field1.Elements.ContainsKey(PdfAnnotation.Keys.AP), "field1 should have rendered an appearance stream.");

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
            Assert.IsTrue(stringRepresentationOfStream == targetStreamValue, "Stream value is not correct");
        }

        [TestMethod]
        public void WalkAllFields()
        {
            PdfDocument document = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfTextField field1 = PdfAcroFieldTestHelpers.CreateTextFieldForTest(document);

            IEnumerable<PdfAcroField> fields = document.AcroForm.WalkAllFields(field1);

            Assert.IsNotNull(fields, "fields should not be null");
            Assert.IsTrue(fields.Count() == 1, "incorrect number of fields returned");
            Assert.IsTrue(fields.Contains(field1), "fields should contain field1");
        }

        [TestMethod]
        public void WalkAllFields_NullParamter()
        {
            PdfDocument document = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfTextField field1 = PdfAcroFieldTestHelpers.CreateTextFieldForTest(document);

            IEnumerable<PdfAcroField> fields = document.AcroForm.WalkAllFields(null);

            Assert.IsNotNull(fields, "fields should not be null");
            Assert.IsTrue(fields.Count() == 0, "incorrect number of fields returned");
        }

        [TestMethod]
        public void RemoveJavascript()
        {
            PdfDocument document = PdfAcroFieldTestHelpers.SetupDocumentForTest();

            Assert.IsFalse(document.Catalog.Elements.Any(e => e.Key == "/Names"), "document-catalog should not have a Names item");

            PdfDictionary javascript = new PdfDictionary();
            javascript.Elements.Add("/NoMatter", new PdfArray());

            PdfDictionary names = new PdfDictionary();
            names.Elements.Add("/JavaScript", javascript);

            document.Catalog.Elements.Add("/Names", names);

            Assert.IsTrue(document.Catalog.Elements.Any(e => e.Key == "/Names"), "document-catalog should have a names item");
            Assert.IsTrue(document.Catalog.Elements.GetDictionary("/Names").Elements.Any(e => e.Key == "/JavaScript"), "document-catalog should have a Javascript item");

            document.AcroForm.RemoveJavascript();

            Assert.IsTrue(document.Catalog.Elements.Any(e => e.Key == "/Names"), "document should have a Names item");
            Assert.IsFalse(document.Catalog.Elements.GetDictionary("/Names").Elements.Any(e => e.Key == "/JavaScript"), "document-catalog should not have a Javascript item");
        }

        [TestMethod]
        public void RemoveJavascript_NoJavascript()
        {
            PdfDocument document = PdfAcroFieldTestHelpers.SetupDocumentForTest();

            Assert.IsFalse(document.Catalog.Elements.Any(e => e.Key == "/Names"), "document-catalog should not have a Names item");

            document.AcroForm.RemoveJavascript();

            Assert.IsFalse(document.Catalog.Elements.Any(e => e.Key == "/Names"), "document-catalog should not have a Names item");
        }
    }
}
