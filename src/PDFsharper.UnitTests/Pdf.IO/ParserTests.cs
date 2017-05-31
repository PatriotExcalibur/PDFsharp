using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfSharper.Pdf;
using PdfSharper.Pdf.IO;
using PdfSharper.Pdf.Filters;
using System.Linq;
using System.IO.Compression;
using System.IO;
using PdfSharper.Pdf.AcroForms;

namespace PDFsharper.UnitTests.Pdf.IO
{
    [TestClass]
    public class ParserTests
    {

#if DEBUG
        [TestMethod]
        public void Debug_FileRead()
        {
            PdfDocument doc = PdfReader.Open(@"D:\Blank_linear.pdf");

            doc.Save(@"D:\Blank_passthrough.pdf");
        }

        [TestMethod]
        public void MFRI_Passthrough()
        {
            PdfDocument doc = PdfReader.Open(@"C:\Dev\PEX\Main\Source\Forms\memoforrecordinformational.pdf");
            var b = doc.PageCount;
            doc.Save(@"D:\mfri_passthrough.pdf");
        }

        [TestMethod]
        public void MFRI_Passthrough_Read()
        {
            PdfDocument doc = PdfReader.Open(@"D:\mfri_passthrough.pdf");
            var b = doc.PageCount;

        }


        [TestMethod]
        public void AF4349_Passthrough()
        {
            PdfDocument doc = PdfReader.Open(@"C:\Dev\PEX\Main\Source\Forms\af4349.pdf");
            var b = doc.PageCount;
            doc.Save(@"D:\af4349_passthrough.pdf");
        }



        [TestMethod]
        public void AF4349_Passthrough_readBack()
        {
            PdfDocument doc = PdfReader.Open(@"D:\af4349_passthrough.pdf");
            var b = doc.PageCount;
        }

        [TestMethod]
        public void ACC134_Passthrough()
        {
            PassThrough("ACC134");
        }


        [TestMethod]
        public void ACC134_PassthroughAndFill()
        {
            PassThroughAndFill("ACC134");
        }

        [TestMethod]
        public void AF2407_PassthroughAndFill()
        {
            PassThroughAndFill("AF2407");
        }

        [TestMethod]
        public void MFRE_PassthroughAndFill()
        {
            PassThroughAndFill("MemoForRecordEndorsement");
        }

        [TestMethod]
        public void MFRE_Passthrough()
        {
            PassThrough("MemoForRecordEndorsement");
        }

        [TestMethod]
        public void AF3862_PassthroughFillFlat()
        {
            PassThroughFillAndFlatten("AF3862");
        }


        [TestMethod]
        public void PassthroughAll()
        {
            string[] files = Directory.GetFiles(@"c:\dev\pex\main\source\forms", "*.pdf");
            foreach (string file in files)
            {
                PassThrough(Path.GetFileNameWithoutExtension(file));
            }
        }

        [TestMethod]
        public void PassthroughAndFillAll()
        {
            string[] files = Directory.GetFiles(@"c:\dev\pex\main\source\forms", "*.pdf");
            foreach (string file in files)
            {
                PassThroughAndFill(Path.GetFileNameWithoutExtension(file));
            }
        }

        [TestMethod]
        public void PassthroughFillAndFlattenAll()
        {
            string[] files = Directory.GetFiles(@"c:\dev\pex\main\source\forms", "*.pdf");
            foreach (string file in files)
            {
                PassThroughFillAndFlatten(Path.GetFileNameWithoutExtension(file));
            }
        }

        private static void PassThrough(string formName)
        {
            PdfDocument doc = PdfReader.Open(Path.Combine(@"C:\Dev\PEX\Main\Source\Forms", formName + ".pdf"));
            var b = doc.PageCount;
            doc.Save(Path.Combine(@"D:\passthrough", formName + "_passthrough.pdf"));

            PdfDocument passThroughDoc = PdfReader.Open(Path.Combine(@"D:\passthrough", formName + "_passthrough.pdf"));

            foreach (var iref in doc._irefTable.AllReferences)
            {
                Assert.IsTrue(passThroughDoc._irefTable.Contains(iref.ObjectID), $"Pass through doc {formName} missing object {iref.ObjectNumber}.");
            }
        }

        private static void PassThroughAndFill(string formName)
        {
            PdfDocument doc = PdfReader.Open(Path.Combine(@"C:\Dev\PEX\Main\Source\Forms", formName + ".pdf"));
            var b = doc.PageCount;

            foreach (var field in doc.AcroForm.Fields.Cast<PdfAcroField>().SelectMany(gf => doc.AcroForm.WalkAllFields(gf)).OfType<PdfTextField>())
            {
                field.Text = field.Name;
            }

            doc.Save(Path.Combine(@"D:\filled", formName + "_filled.pdf"));

            PdfDocument passThroughDoc = PdfReader.Open(Path.Combine(@"D:\filled", formName + "_filled.pdf"));

            foreach (var iref in doc._irefTable.AllReferences)
            {
                Assert.IsTrue(passThroughDoc._irefTable.Contains(iref.ObjectID), $"Pass through doc {formName} missing object {iref.ObjectNumber}.");
            }
        }

        private static void PassThroughFillAndFlatten(string formName)
        {
            PdfDocument doc = PdfReader.Open(Path.Combine(@"C:\Dev\PEX\Main\Source\Forms", formName + ".pdf"));
            var b = doc.PageCount;

            foreach (var field in doc.AcroForm.Fields.Cast<PdfAcroField>().SelectMany(gf => doc.AcroForm.WalkAllFields(gf)).OfType<PdfTextField>())
            {
                field.Text = field.Name;
            }

            doc.AcroForm.Flatten();

            doc.Save(Path.Combine(@"D:\filled", formName + "_filled.pdf"));

            PdfDocument passThroughDoc = PdfReader.Open(Path.Combine(@"D:\filled", formName + "_filled.pdf"));

            foreach (var iref in doc._irefTable.AllReferences)
            {
                Assert.IsTrue(passThroughDoc._irefTable.Contains(iref.ObjectID), $"Pass through doc {formName} missing object {iref.ObjectNumber}.");
            }
        }
#endif
    }
}
