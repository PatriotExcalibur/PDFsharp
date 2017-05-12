using PdfSharper.Drawing;
using PdfSharper.Fonts;
using PdfSharper.Pdf;
using PdfSharper.Pdf.AcroForms;
using PdfSharper.Pdf.Advanced;
using PdfSharper.Pdf.Annotations;
using PdfSharper.Pdf.Internal;

namespace PDFsharper.UnitTests.Pdf.AcroForms
{
    public class PdfAcroFieldTestHelpers
    {
        public static readonly string FONT_BASEFONT = "Helvetica";
        public static readonly string FONT_NAME = "Helv";
        private static readonly object GlobalFontSettingsLock = new object();

        public static PdfDocument SetupDocumentForTest()
        {
            PdfDocument document = new PdfDocument();

            document.Catalog.AcroForm = new PdfAcroForm(document);

            PdfPage p1 = document.Pages.Add();
            document.AcroForm.Elements.SetValue(PdfAcroForm.Keys.Fields, new PdfAcroField.PdfAcroFieldCollection(new PdfArray(document)));

            PdfDictionary helvFont = new PdfDictionary(document);
            helvFont.Elements.SetName("/BaseFont", FONT_BASEFONT);
            helvFont.Elements.SetName("/Name", "/" + FONT_NAME);
            helvFont.Elements.SetName("/Subtype", "/Type1");
            helvFont.Elements.SetName("/Type", "/Font");

            document.Internals.AddObject(helvFont);

            PdfDictionary fontDictionary = new PdfDictionary(document);
            fontDictionary.Elements.SetReference("/" + FONT_NAME, helvFont);

            PdfDictionary resourceDict = new PdfDictionary(document);
            resourceDict.Elements.SetObject("/Font", fontDictionary);
            document.AcroForm.Elements.SetObject("/DR", resourceDict);

            return document;
        }

        public static PdfTextField CreateTextFieldForTest(PdfDocument document)
        {
            PdfTextField textField = new PdfTextField(document);
            textField.BorderColor = new XColor(XKnownColor.White);
            textField.Rectangle = new PdfRectangle(new XRect(0, 0, 200, 20));

            document.AcroForm.Fields.Add(textField, 1);

            return textField;
        }

        public static PdfCheckBoxField CreateCheckBoxFieldForTest(PdfDocument document)
        {
            PdfCheckBoxField checkboxField = new PdfCheckBoxField(document, true);
            checkboxField.BorderColor = new XColor(XKnownColor.White);
            checkboxField.Rectangle = new PdfRectangle(new XRect(0, 0, 200, 20));

            document.AcroForm.Fields.Add(checkboxField, 1);
            document.PrepareForSave();

            return checkboxField;
        }
    }
}
