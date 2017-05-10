using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfSharper.Drawing;
using PdfSharper.Pdf;
using PdfSharper.Pdf.AcroForms;
using PdfSharper.Pdf.Advanced;
using PdfSharper.Pdf.Annotations;

namespace PDFsharper.UnitTests.Pdf.AcroForms
{
    [TestClass]
    public class PdfTextFieldTests
    {
        [TestMethod]
        public void RenderAppearance()
        {
            PdfDocument testDoc = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfTextField tf = PdfAcroFieldTestHelpers.CreateTextFieldForTest(testDoc);

            tf.Text = "Some Test Text";

            Assert.IsFalse(tf.Elements.ContainsKey(PdfAnnotation.Keys.AP), "Text Field should have rendered an appearance stream prior to PrepareForSave.");

            tf.PrepareForSave();

            Assert.IsTrue(tf.Elements.ContainsKey(PdfAnnotation.Keys.AP), "Text Field should have rendered an appearance stream.");
        }

        [TestMethod]
        public void RenderAppearance_Empty()
        {
            PdfDocument testDoc = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfTextField tf = PdfAcroFieldTestHelpers.CreateTextFieldForTest(testDoc);

            tf.Text = "Some Test Text";

            Assert.IsFalse(tf.Elements.ContainsKey(PdfAnnotation.Keys.AP), "Text Field should have rendered an appearance stream prior to PrepareForSave.");

            tf.PrepareForSave();

            Assert.IsTrue(tf.Elements.ContainsKey(PdfAnnotation.Keys.AP), "Text Field should have rendered an appearance stream.");

            tf.BorderColor = new XColor();
            tf.Text = string.Empty;

            tf.PrepareForSave();

            Assert.IsFalse(tf.Elements.ContainsKey(PdfAnnotation.Keys.AP), "Empty Text Field should not have an appearance stream after clearing text and saving");
        }

        [TestMethod]
        public void Flatten()
        {
            string fieldValue = "Test";
            string targetStreamValue = "q\n1.0 0.0 0.0 1.0 0.0 0.0 cm\n1 w\n0 J\n0 j\n[]0 d\n1 1 1 RG\n/GS0 gs\n0 0 200 20 re\nS\nq\n/Tx BMC\nBT\n/" + PdfAcroFieldTestHelpers.FONT_NAME + " 10 Tf\n0 g\n2 6.3644 Td\n(" + fieldValue + ")Tj\nET\nEMC\nQ\nQ\n";

            PdfDocument document = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfTextField field1 = PdfAcroFieldTestHelpers.CreateTextFieldForTest(document);

            field1.Text = fieldValue;

            Assert.IsNotNull(document);
            Assert.IsNotNull(document.Pages);
            Assert.IsTrue(document.Pages.Count == 1);
            Assert.IsNotNull(document.Pages[0].Contents);
            Assert.IsNotNull(document.Pages[0].Contents.Elements);
            Assert.IsTrue(document.Pages[0].Contents.Elements.Count == 0);

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
        public void Flatten_TextAndNewLines()
        {
            string fieldValue = "Test \n Test";
            string targetStreamValue = "q\n1.0 0.0 0.0 1.0 0.0 0.0 cm\n1 w\n0 J\n0 j\n[]0 d\n1 1 1 RG\n/GS0 gs\n0 0 200 20 re\nS\nq\n/Tx BMC\nBT\n/" + PdfAcroFieldTestHelpers.FONT_NAME + " 10 Tf\n0 g\n2 12.1444 Td\n(Test )Tj\n0 -11.56 Td\n( )Tj\n2.78 0 Td\n(Test)Tj\nET\nEMC\nQ\nQ\n";

            PdfDocument document = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfTextField field1 = PdfAcroFieldTestHelpers.CreateTextFieldForTest(document);

            field1.Text = fieldValue;

            Assert.IsNotNull(document);
            Assert.IsNotNull(document.Pages);
            Assert.IsTrue(document.Pages.Count == 1);
            Assert.IsNotNull(document.Pages[0].Contents);
            Assert.IsNotNull(document.Pages[0].Contents.Elements);
            Assert.IsTrue(document.Pages[0].Contents.Elements.Count == 0);

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
        public void Flatten_TextAndWhiteSpace()
        {
            string fieldValue = "Test 1 2 3 4";
            string targetStreamValue = "q\n1.0 0.0 0.0 1.0 0.0 0.0 cm\n1 w\n0 J\n0 j\n[]0 d\n1 1 1 RG\n/GS0 gs\n0 0 200 20 re\nS\nq\n/Tx BMC\nBT\n/" + PdfAcroFieldTestHelpers.FONT_NAME + " 10 Tf\n0 g\n2 6.3644 Td\n(Test )Tj\n22.23 0 Td\n(1 )Tj\n8.34 0 Td\n(2 )Tj\n8.34 0 Td\n(3 )Tj\n8.34 0 Td\n(4)Tj\nET\nEMC\nQ\nQ\n";

            PdfDocument document = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfTextField field1 = PdfAcroFieldTestHelpers.CreateTextFieldForTest(document);

            field1.Text = fieldValue;

            Assert.IsNotNull(document);
            Assert.IsNotNull(document.Pages);
            Assert.IsTrue(document.Pages.Count == 1);
            Assert.IsNotNull(document.Pages[0].Contents);
            Assert.IsNotNull(document.Pages[0].Contents.Elements);
            Assert.IsTrue(document.Pages[0].Contents.Elements.Count == 0);

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
        public void Flatten_WhiteSpace()
        {
            string fieldValue = "";
            string targetStreamValue = "q\n1.0 0.0 0.0 1.0 0.0 0.0 cm\n1 w\n0 J\n0 j\n[]0 d\n1 1 1 RG\n/GS0 gs\n0 0 200 20 re\nS\nQ\n";

            PdfDocument document = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfTextField field1 = PdfAcroFieldTestHelpers.CreateTextFieldForTest(document);

            field1.Text = fieldValue;

            Assert.IsNotNull(document);
            Assert.IsNotNull(document.Pages);
            Assert.IsTrue(document.Pages.Count == 1);
            Assert.IsNotNull(document.Pages[0].Contents);
            Assert.IsNotNull(document.Pages[0].Contents.Elements);
            Assert.IsTrue(document.Pages[0].Contents.Elements.Count == 0);

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
        public void Flatten_NoText()
        {
            string fieldValue = "";
            string targetStreamValue = "q\n1.0 0.0 0.0 1.0 0.0 0.0 cm\n1 w\n0 J\n0 j\n[]0 d\n1 1 1 RG\n/GS0 gs\n0 0 200 20 re\nS\nQ\n";

            PdfDocument document = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfTextField field1 = PdfAcroFieldTestHelpers.CreateTextFieldForTest(document);

            field1.Text = fieldValue;

            Assert.IsNotNull(document);
            Assert.IsNotNull(document.Pages);
            Assert.IsTrue(document.Pages.Count == 1);
            Assert.IsNotNull(document.Pages[0].Contents);
            Assert.IsNotNull(document.Pages[0].Contents.Elements);
            Assert.IsTrue(document.Pages[0].Contents.Elements.Count == 0);

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
        public void Multiline()
        {
            PdfDocument document = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfTextField field = PdfAcroFieldTestHelpers.CreateTextFieldForTest(document);

            Assert.IsFalse(field.MultiLine, "field should not be multiline");
            Assert.IsFalse(field.MultiLine & field.Flags == 0, "Flags should not contain multiline");
            Assert.IsFalse(field.MultiLine & field.FieldFlags == 0, "fieldFlags should not contain multiline");

            field.SetFlags |= PdfAcroFieldFlags.Multiline;

            Assert.IsTrue(field.MultiLine, "field should be multiline");
            Assert.IsFalse(field.MultiLine & field.Flags == 0, "Flags should not contain multiline");
            Assert.IsFalse(field.MultiLine & field.FieldFlags == 0, "fieldFlags should not contain multiline");

            field.SetFlags &= ~PdfAcroFieldFlags.Multiline;

            Assert.IsFalse(field.MultiLine, "field should not be multiline");
            Assert.IsFalse(field.MultiLine & field.Flags == 0, "Flags should not contain multiline");
            Assert.IsFalse(field.MultiLine & field.FieldFlags == 0, "fieldFlags should not contain multiline");
        }

        [TestMethod]
        public void MaxLength()
        {
            PdfDocument document = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfTextField field = PdfAcroFieldTestHelpers.CreateTextFieldForTest(document);

            Assert.IsTrue(field.MaxLength == 0, "MaxLength should be zero");
            Assert.IsTrue(field.Elements.GetInteger(PdfTextField.Keys.MaxLen) == 0, "field elements should contain MaxLength Key with Value of 0");

            field.MaxLength = 100;

            Assert.IsTrue(field.MaxLength == 100, "MaxLength should be 100");
            Assert.IsTrue(field.Elements.GetInteger(PdfTextField.Keys.MaxLen) == 100, "field elements should contain MaxLength Key with Value of 100");
        }

        [TestMethod]
        public void Password()
        {
            PdfDocument document = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfTextField field = PdfAcroFieldTestHelpers.CreateTextFieldForTest(document);

            Assert.IsFalse(field.Password, "field should not be Password");
            Assert.IsFalse(field.Password & field.Flags == 0, "Flags should not contain Password");
            Assert.IsFalse(field.Password & field.FieldFlags == 0, "fieldFlags should not contain Password");

            field.SetFlags |= PdfAcroFieldFlags.Password;

            Assert.IsTrue(field.Password, "field should be Password");
            Assert.IsFalse(field.Password & field.Flags == 0, "Flags should not contain Password");
            Assert.IsFalse(field.Password & field.FieldFlags == 0, "fieldFlags should not contain Password");

            field.SetFlags &= ~PdfAcroFieldFlags.Password;

            Assert.IsFalse(field.Password, "field should not be Password");
            Assert.IsFalse(field.Password & field.Flags == 0, "Flags should not contain Password");
            Assert.IsFalse(field.Password & field.FieldFlags == 0, "fieldFlags should not contain Password");
        }

        [TestMethod]
        public void Combine()
        {
            PdfDocument document = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfTextField field = PdfAcroFieldTestHelpers.CreateTextFieldForTest(document);

            Assert.IsFalse(field.Combined, "field should not be Combined");
            Assert.IsFalse(field.Combined & field.Flags == 0, "Flags should not contain Comb");
            Assert.IsFalse(field.Combined & field.FieldFlags == 0, "fieldFlags should not contain Comb");

            field.SetFlags |= PdfAcroFieldFlags.Comb;

            Assert.IsTrue(field.Combined, "field should be Combined");
            Assert.IsFalse(field.Combined & field.Flags == 0, "Flags should not contain Comb");
            Assert.IsFalse(field.Combined & field.FieldFlags == 0, "fieldFlags should not contain Comb");

            field.SetFlags &= ~PdfAcroFieldFlags.Comb;

            Assert.IsFalse(field.Combined, "field should not be Combined");
            Assert.IsFalse(field.Combined & field.Flags == 0, "Flags should not contain Comb");
            Assert.IsFalse(field.Combined & field.FieldFlags == 0, "fieldFlags should not contain Comb");
        }

        [TestMethod]
        public void SetDefaultMargins_Multiline()
        {
            PdfDocument document = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfTextField field = PdfAcroFieldTestHelpers.CreateTextFieldForTest(document);

            field.TopMargin = 0;
            field.BottomMargin = 0;
            field.LeftMargin = 0;
            field.RightMargin = 0;

            Assert.IsTrue(field.TopMargin == 0, "TopMargin should be 0");
            Assert.IsTrue(field.BottomMargin == 0, "BottomMargin should be 0");
            Assert.IsTrue(field.LeftMargin == 0, "LeftMargin should be 0");
            Assert.IsTrue(field.RightMargin == 0, "RightMargin should be 0");

            field.MultiLine = true;
            
            Assert.IsTrue(field.TopMargin == 4, "TopMargin should be 4");
            Assert.IsTrue(field.BottomMargin == 4, "BottomMargin should be 4");
            Assert.IsTrue(field.LeftMargin == 2, "LeftMargin should be 2");
            Assert.IsTrue(field.RightMargin == 2, "RightMargin should be 2");
        }

        [TestMethod]
        public void SetDefaultMargins_Not_Multiline()
        {
            PdfDocument document = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfTextField field = PdfAcroFieldTestHelpers.CreateTextFieldForTest(document);

            field.TopMargin = 0;
            field.BottomMargin = 0;
            field.LeftMargin = 0;
            field.RightMargin = 0;

            Assert.IsTrue(field.TopMargin == 0, "TopMargin should be 0");
            Assert.IsTrue(field.BottomMargin == 0, "BottomMargin should be 0");
            Assert.IsTrue(field.LeftMargin == 0, "LeftMargin should be 0");
            Assert.IsTrue(field.RightMargin == 0, "RightMargin should be 0");

            field.MultiLine = false;
            
            Assert.IsTrue(field.TopMargin == 0, "TopMargin should be 0");
            Assert.IsTrue(field.BottomMargin == 0, "BottomMargin should be 0");
            Assert.IsTrue(field.LeftMargin == 2, "LeftMargin should be 2");
            Assert.IsTrue(field.RightMargin == 0, "RightMargin should be 0");
        }

        [TestMethod]
        public void Text()
        {
            PdfDocument document = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfTextField field = PdfAcroFieldTestHelpers.CreateTextFieldForTest(document);

            Assert.IsTrue(field.Text == string.Empty, "Text should be \"\"");
            Assert.IsTrue(field.Elements.GetString(PdfTextField.Keys.V) == string.Empty, "field elements should contain Value Key with Value of string.empty");

            field.Text  = "Test";

            Assert.IsTrue(field.Text == "Test", "Text should be \"Test\"");
            Assert.IsTrue(field.Elements.GetString(PdfTextField.Keys.V) == "Test", "field elements should contain Value Key with Value of Test");
        }

        [TestMethod]
        public void Alignment()
        {
            PdfDocument document = PdfAcroFieldTestHelpers.SetupDocumentForTest();
            PdfTextField field = PdfAcroFieldTestHelpers.CreateTextFieldForTest(document);

            Assert.IsTrue(field.MultiLine == false, "Field should not be multiline");
            Assert.IsNotNull(field.Alignment, "Alignment should not be null");
            Assert.IsTrue(field.Alignment.Alignment == XStringAlignment.Near, "Alignment should be Near");
            Assert.IsTrue(field.Alignment.LineAlignment == XLineAlignment.Center, "Line Alignment should be Center");
            Assert.IsTrue(field.Elements.GetInteger(PdfTextField.Keys.Q) == 0, "field elements should contain Q Key with Value of string.empty");

            field.MultiLine = false;
            field.Alignment = XStringFormats.TopCenter;

            Assert.IsTrue(field.Alignment.Alignment == XStringAlignment.Center, "Alignment should be Center");
            Assert.IsTrue(field.Alignment.LineAlignment == XLineAlignment.Center, "Line Alignment should be Center");
            Assert.IsTrue(field.Elements.GetInteger(PdfTextField.Keys.Q) == 1, "field elements should contain Q Key with Value of string.empty");

            field.MultiLine = false;
            field.Alignment = XStringFormats.TopRight;

            Assert.IsTrue(field.Alignment.Alignment == XStringAlignment.Far, "Alignment should be Far");
            Assert.IsTrue(field.Alignment.LineAlignment == XLineAlignment.Center, "Line Alignment should be Center");
            Assert.IsTrue(field.Elements.GetInteger(PdfTextField.Keys.Q) == 2, "field elements should contain Q Key with Value of string.empty");

            field.MultiLine = false;
            field.Alignment = XStringFormats.TopLeft;

            Assert.IsTrue(field.Alignment.Alignment == XStringAlignment.Near, "Alignment should be Near");
            Assert.IsTrue(field.Alignment.LineAlignment == XLineAlignment.Center, "Line Alignment should be Center");
            Assert.IsTrue(field.Elements.GetInteger(PdfTextField.Keys.Q) == 0, "field elements should contain Q Key with Value of string.empty");

            field.MultiLine = true;
            field.Alignment = XStringFormats.TopCenter;

            Assert.IsTrue(field.Alignment.Alignment == XStringAlignment.Center, "Alignment should be Center");
            Assert.IsTrue(field.Alignment.LineAlignment == XLineAlignment.Near, "Line Alignment should be Center");
            Assert.IsTrue(field.Elements.GetInteger(PdfTextField.Keys.Q) == 1, "field elements should contain Q Key with Value of string.empty");

            field.MultiLine = true;
            field.Alignment = XStringFormats.TopRight;

            Assert.IsTrue(field.Alignment.Alignment == XStringAlignment.Far, "Alignment should be Far");
            Assert.IsTrue(field.Alignment.LineAlignment == XLineAlignment.Near, "Line Alignment should be Center");
            Assert.IsTrue(field.Elements.GetInteger(PdfTextField.Keys.Q) == 2, "field elements should contain Q Key with Value of string.empty");

            field.MultiLine = true;
            field.Alignment = XStringFormats.TopLeft;

            Assert.IsTrue(field.Alignment.Alignment == XStringAlignment.Near, "Alignment should be Near");
            Assert.IsTrue(field.Alignment.LineAlignment == XLineAlignment.Near, "Line Alignment should be Center");
            Assert.IsTrue(field.Elements.GetInteger(PdfTextField.Keys.Q) == 0, "field elements should contain Q Key with Value of string.empty");
        }
    }
}
