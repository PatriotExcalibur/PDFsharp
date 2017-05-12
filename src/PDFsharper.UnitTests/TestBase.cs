using PdfSharper.Fonts;
using PdfSharper.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFsharper.UnitTests
{
    public class TestBase
    {
        static TestBase()
        {
            GlobalFontSettings.DefaultFontEncoding = PdfFontEncoding.WinAnsi;
        }
    }
}
