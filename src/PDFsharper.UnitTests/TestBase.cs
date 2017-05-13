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
        private static bool _hasDefaultBeenSet = false;
        private static object _defaultSetLock = new object();
        static TestBase()
        {
            lock (_defaultSetLock)
            {
                if (_hasDefaultBeenSet)
                {
                    return;
                }

                _hasDefaultBeenSet = true;
                GlobalFontSettings.DefaultFontEncoding = PdfFontEncoding.WinAnsi;
            }
        }
    }
}
