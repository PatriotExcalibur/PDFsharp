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
        public void AF4023_PassthroughAndFill()
        {
            PassThroughAndFill("AF4023");
        }

        [TestMethod]
        public void MFRI_PassthroughFillFlat()
        {
            PassThroughFillAndFlatten("MemoForRecordInformational");
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

        private static void PassThroughAndFill(string formName, int fieldNumber = -1)
        {
            PdfDocument doc = PdfReader.Open(Path.Combine(@"C:\Dev\PEX\Main\Source\Forms", formName + ".pdf"));
            var b = doc.PageCount;
            int i = 0;
            foreach (var field in doc.AcroForm.Fields.Cast<PdfAcroField>().SelectMany(gf => doc.AcroForm.WalkAllFields(gf)).OfType<PdfTextField>())
            {
                if (fieldNumber != -1 && i != fieldNumber)
                {
                    i++;
                    continue;
                }

                field.Text = field.Name;
                i++;
            }

            doc.Save(Path.Combine(@"D:\filled", formName + (fieldNumber != -1 ? "_" + fieldNumber : string.Empty) + "_filled.pdf"));

            PdfDocument passThroughDoc = PdfReader.Open(Path.Combine(@"D:\filled", formName + (fieldNumber != -1 ? "_" + fieldNumber : string.Empty) + "_filled.pdf"));

            foreach (var iref in doc._irefTable.AllReferences)
            {
                Assert.IsTrue(passThroughDoc._irefTable.Contains(iref.ObjectID), $"Pass through doc {formName} missing object {iref.ObjectNumber}.");
            }
        }

        private static void PassThroughFillAndFlatten(string formName, int fieldNumber = -1)
        {
            using (PdfDocument doc = PdfReader.Open(Path.Combine(@"C:\Dev\PEX\Main\Source\Forms", formName + ".pdf")))
            {
                var b = doc.PageCount;
                int i = 0;
                foreach (var field in doc.AcroForm.Fields.Cast<PdfAcroField>().SelectMany(gf => doc.AcroForm.WalkAllFields(gf)).OfType<PdfTextField>().ToList())
                {
                    if (fieldNumber != -1 && i != fieldNumber)
                    {
                        i++;
                        continue;
                    }

                    field.Text = field.Name;
                    field.Flatten();
                    i++;
                }

                //doc.AcroForm.Flatten();

                doc.Save(Path.Combine(@"D:\flattened", formName + (fieldNumber != -1 ? "_" + fieldNumber : string.Empty) + "_flat.pdf"));

                using (PdfDocument passThroughDoc = PdfReader.Open(Path.Combine(@"D:\flattened", formName + (fieldNumber != -1 ? "_" + fieldNumber : string.Empty) + "_flat.pdf")))
                {
                    foreach (var iref in doc._irefTable.AllReferences)
                    {
                        Assert.IsTrue(passThroughDoc._irefTable.Contains(iref.ObjectID), $"Pass through doc {formName} missing object {iref.ObjectNumber}.");
                    }
                }
            }
        }
#endif
    }
}
