#region PDFsharp - A .NET library for processing PDF
//
// Authors:
//   Stefan Lange
//
// Copyright (c) 2005-2016 empira Software GmbH, Cologne Area (Germany)
//
// http://www.PdfSharper.com
// http://sourceforge.net/projects/pdfsharp
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.IO;
using PdfSharper.Internal;
using System.IO.Compression;

namespace PdfSharper.Pdf.Filters
{
    /// <summary>
    /// Implements the FlateDecode filter by wrapping SharpZipLib.
    /// </summary>
    public class FlateDecode : Filter
    {
        // Reference: 3.3.3  LZWDecode and FlateDecode Filters / Page 71

        /// <summary>
        /// Encodes the specified data.
        /// </summary>
        public override byte[] Encode(byte[] data)
        {
            return Encode(data, PdfFlateEncodeMode.Default);
        }

        /// <summary>
        /// Encodes the specified data.
        /// </summary>
        public byte[] Encode(byte[] data, PdfFlateEncodeMode mode)
        {
            using (MemoryStream ms = new MemoryStream())
            using (DeflateStream zip = new DeflateStream(ms, CompressionMode.Compress, true))
            {
                ms.WriteByte(0x78);
                ms.WriteByte(0x01);

                zip.Write(data, 0, data.Length);
                zip.Close();

                return ms.ToArray();
            }
        }

        /// <summary>
        /// Decodes the specified data.
        /// </summary>
        public override byte[] Decode(byte[] data, FilterParms parms)
        {
            //header bytes are not wanted by .net
            using (MemoryStream msInput = new MemoryStream(data, 2, data.Length - 2))
            using (MemoryStream msOutput = new MemoryStream())
            using (DeflateStream zip = new DeflateStream(msInput, CompressionMode.Decompress, true))
            {
                int cbRead;
                byte[] abResult = new byte[1024];
                do
                {
                    cbRead = zip.Read(abResult, 0, abResult.Length);
                    if (cbRead > 0)
                        msOutput.Write(abResult, 0, cbRead);
                }
                while (cbRead > 0);

                zip.Close();
                msOutput.Flush();

                if (msOutput.Length >= 0)
                {
                    return msOutput.ToArray();
                }
                return null;
            }
        }
    }
}
