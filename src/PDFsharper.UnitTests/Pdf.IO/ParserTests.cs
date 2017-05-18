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
        [TestMethod]
        public void DecodeCrossReferenceStream()
        {
            byte[] deflatedXRefStream = new byte[] { 104, 222, 98, 98, 0, 2, 38, 48, 193, 200, 184, 129, 129, 233, 255, 255, 0, 40, 23, 76, 48, 49, 8, 130, 89, 140, 76, 255, 249, 226, 255, 51, 49, 126, 90, 4, 84, 194, 55, 9, 72, 124, 138, 1, 234, 224, 15, 6, 18, 31, 247, 51, 50, 253, 99, 120, 247, 31, 106, 10, 255, 124, 160, 142, 143, 133, 32, 109, 187, 129, 4, 63, 43, 3, 64, 128, 1, 0, 82, 69, 19, 9 };
            byte[] encodedXRefStream = new FlateDecode().Decode(deflatedXRefStream);
            byte[] actual = Parser.DecodeCrossReferenceStream(encodedXRefStream, 4, 12);


            byte[] expected = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 176, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 17, 0, 2, 0, 17, 1, 1, 14, 112, 0, 2, 0, 18, 0, 1, 14, 164, 0, 0, 0, 0, 0, 1, 15, 83, 0, 2, 0, 18, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 15, 159, 0, 1, 0, 16, 0, 1, 0, 203, 0, 1, 15, 208, 0 };
            Assert.IsTrue(expected.SequenceEqual(actual), "Decoded stream does not match expected");
        }

        [TestMethod]
        public void EncodeCrossReferenceStream()
        {
            byte[] expected = new byte[] { 2, 0, 0, 0, 0, 2, 0, 0, 0, 0, 2, 1, 1, 176, 0, 2, 255, 255, 80, 0, 2, 0, 0, 0, 0, 2, 0, 0, 0, 0, 2, 2, 0, 17, 0, 2, 0, 0, 0, 1, 2, 255, 14, 95, 255, 2, 1, 242, 162, 0, 2, 255, 14, 146, 0, 2, 255, 242, 92, 0, 2, 1, 15, 83, 0, 2, 1, 241, 191, 1, 2, 254, 0, 238, 255, 2, 0, 0, 0, 0, 2, 1, 15, 159, 0, 2, 0, 241, 113, 0, 2, 0, 0, 187, 0, 2, 0, 15, 5, 0 };
            byte[] rawXRefStream = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 176, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 17, 0, 2, 0, 17, 1, 1, 14, 112, 0, 2, 0, 18, 0, 1, 14, 164, 0, 0, 0, 0, 0, 1, 15, 83, 0, 2, 0, 18, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 15, 159, 0, 1, 0, 16, 0, 1, 0, 203, 0, 1, 15, 208, 0 };


            byte[] actual = Parser.EncodeCrossReferenceStream(rawXRefStream, 4, 12);
            Assert.IsTrue(expected.SequenceEqual(actual), "Encoded stream does not match expected");
        }
#if DEBUG
        [TestMethod]
        public void Debug_FileRead()
        {
            PdfDocument doc = PdfReader.Open(@"D:\Blank.pdf");
        }
#endif
    }
}
