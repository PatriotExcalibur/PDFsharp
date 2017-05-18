using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfSharper.Pdf;
using PdfSharper.Pdf.IO;
using PdfSharper.Pdf.Filters;
using System.Linq;
using System.IO.Compression;
using System.IO;

namespace PDFsharper.UnitTests.Pdf.IO
{
    [TestClass]
    public class ParserTests
    {
       
#if DEBUG
        [TestMethod]
        public void Debug_FileRead()
        {
            PdfDocument doc = PdfReader.Open(@"D:\Blank.pdf");
        }
#endif
    }
}
