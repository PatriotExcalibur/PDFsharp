using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfSharper.Pdf.Filters;
using PdfSharper.Pdf;
using System.Linq;

namespace PDFsharper.UnitTests.Pdf
{
    [TestClass]
    public class PdfDictionaryTest
    {
        [TestMethod]
        public void DecodeStream()
        {
            byte[] deflatedXRefStream = new byte[] { 104, 222, 98, 98, 0, 2, 38, 48, 193, 200, 184, 129, 129, 233, 255, 255, 0, 40, 23, 76, 48, 49, 8, 130, 89, 140, 76, 255, 249, 226, 255, 51, 49, 126, 90, 4, 84, 194, 55, 9, 72, 124, 138, 1, 234, 224, 15, 6, 18, 31, 247, 51, 50, 253, 99, 120, 247, 31, 106, 10, 255, 124, 160, 142, 143, 133, 32, 109, 187, 129, 4, 63, 43, 3, 64, 128, 1, 0, 82, 69, 19, 9 };
            byte[] encodedXRefStream = new FlateDecode().Decode(deflatedXRefStream);
            byte[] actual = PdfDictionary.PdfStream.DecodeStream(encodedXRefStream, 4, 12);


            byte[] expected = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 176, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 17, 0, 2, 0, 17, 1, 1, 14, 112, 0, 2, 0, 18, 0, 1, 14, 164, 0, 0, 0, 0, 0, 1, 15, 83, 0, 2, 0, 18, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 15, 159, 0, 1, 0, 16, 0, 1, 0, 203, 0, 1, 15, 208, 0 };
            Assert.IsTrue(expected.SequenceEqual(actual), "Decoded stream does not match expected");
        }

        [TestMethod]
        public void EncodeStream()
        {
            byte[] expected = new byte[] { 2, 0, 0, 0, 0, 2, 0, 0, 0, 0, 2, 1, 1, 176, 0, 2, 255, 255, 80, 0, 2, 0, 0, 0, 0, 2, 0, 0, 0, 0, 2, 2, 0, 17, 0, 2, 0, 0, 0, 1, 2, 255, 14, 95, 255, 2, 1, 242, 162, 0, 2, 255, 14, 146, 0, 2, 255, 242, 92, 0, 2, 1, 15, 83, 0, 2, 1, 241, 191, 1, 2, 254, 0, 238, 255, 2, 0, 0, 0, 0, 2, 1, 15, 159, 0, 2, 0, 241, 113, 0, 2, 0, 0, 187, 0, 2, 0, 15, 5, 0 };
            byte[] rawXRefStream = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 176, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 17, 0, 2, 0, 17, 1, 1, 14, 112, 0, 2, 0, 18, 0, 1, 14, 164, 0, 0, 0, 0, 0, 1, 15, 83, 0, 2, 0, 18, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 15, 159, 0, 1, 0, 16, 0, 1, 0, 203, 0, 1, 15, 208, 0 };


            byte[] actual = PdfDictionary.PdfStream.EncodeStream(rawXRefStream, 4, 12);
            Assert.IsTrue(expected.SequenceEqual(actual), "Encoded stream does not match expected");
        }

        [TestMethod]
        public void EncodeStream_Max()
        {
            byte[] expected = new byte[] { 2, 0, 0, 0, 0, 0, 2, 1, 0, 221, 177, 0, 2, 0, 0, 4, 196, 0, 2, 0, 0, 8, 13, 0, 2, 0, 0, 3, 10, 0, 2, 0, 0, 4, 60, 0, 2, 0, 0, 15, 251, 0, 2, 0, 1, 1, 138, 0 };
            byte[] rawXRefStream = new byte[] { 0, 0, 0, 0, 0, 1, 0, 221, 177, 0, 1, 0, 225, 117, 0, 1, 0, 233, 130, 0, 1, 0, 236, 140, 0, 1, 0, 240, 200, 0, 1, 0, 255, 195, 0, 1, 1, 0, 77, 0 };

            byte[] actual = PdfDictionary.PdfStream.EncodeStream(rawXRefStream, 5, 12);
            Assert.IsTrue(expected.SequenceEqual(actual), "Encoded stream does not match expected");
        }
    }
}
