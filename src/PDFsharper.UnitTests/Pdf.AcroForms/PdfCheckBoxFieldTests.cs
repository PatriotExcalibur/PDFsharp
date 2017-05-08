using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfSharper.Drawing;
using PdfSharper.Pdf;
using PdfSharper.Pdf.AcroForms;
using PdfSharper.Pdf.Advanced;
using PdfSharper.Pdf.Annotations;
using System.IO;

namespace PDFsharper.UnitTests.Pdf.AcroForms
{
    [TestClass]
    public class PdfCheckBoxFieldTests
    {
        [TestMethod]
        public void Flatten()
        {
            string fieldValue = "Test";
            string targetStreamValue = "q\n1.0 0.0 0.0 1.0 0.0 0.0 cm\n1 w\n0 J\n0 j\n[]0 d\n1 1 1 RG\n/GS0 gs\n0 0 200 20 re\nS\nq\n/Tx BMC\nBT\n/" + PdfAcroFieldTestHelpers.FONT_NAME + " 10 Tf\n0 g\n2 6.3644 Td\n(" + fieldValue + ")Tj\nET\nEMC\nQ\nQ\n";

            PdfDocument document = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfCheckBoxField field1 = PdfAcroFieldTestHelpers.CreateCheckBoxFieldForTest(document);
            PdfTextField field2 = PdfAcroFieldTestHelpers.CreateTextFieldForTest(document);

            //field1.Checked = true;

            Assert.IsNotNull(document);
            Assert.IsNotNull(document.Pages);
            Assert.IsTrue(document.Pages.Count == 1);
            Assert.IsNotNull(document.Pages[0].Contents);
            Assert.IsNotNull(document.Pages[0].Contents.Elements);
            Assert.IsTrue(document.Pages[0].Contents.Elements.Count == 0);
            field2.Flatten();
            field1.Checked = true;
            field1.Flatten();

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
        public void ummmm()
        {
            PdfDocument document = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            //PdfCheckBoxField field1 = PdfAcroFieldTestHelpers.CreateCheckBoxFieldForTest(document);
            PdfTextField field2 = PdfAcroFieldTestHelpers.CreateTextFieldForTest(document);

            field2.Text = "test";

            field2.Flatten();

            document.Save(@"c:\users\simsr\desktop\test.pdf");
        }

    }
}
