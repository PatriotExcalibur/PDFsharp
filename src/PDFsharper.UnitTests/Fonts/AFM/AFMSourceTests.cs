using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfSharper.Fonts.AFM;
using PdfSharper.Pdf;
using PdfSharper.Pdf.AcroForms;
using PdfSharper.Pdf.Annotations;
using PDFsharper.UnitTests.Pdf.AcroForms;
using System;
using System.Linq;

namespace PDFsharper.UnitTests.Fonts.AFM
{
    [TestClass]
    public class AFMSourceTests
    {
        [TestMethod]
        public void GetSourceByNameAndAttributes_EmptyName()
        {
            string source = AFMSource.GetSourceByNameAndAttributes(string.Empty, false, false);

            Assert.IsTrue(source == string.Empty, "Source didn't return correctly");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Test fails because ArgumentNullException was not thrown.")]
        public void GetSourceByNameAndAttributes_NullName()
        {
            string source = AFMSource.GetSourceByNameAndAttributes(null, false, false);
        }

        [TestMethod]
        public void GetSourceByNameAndAttributes_CourierFamily()
        {
            string courier = "PdfSharper.Fonts.AFM.Files.Courier.afm";
            string courierBold = "PdfSharper.Fonts.AFM.Files.Courier-Bold.afm";
            string courierOblique = "PdfSharper.Fonts.AFM.Files.Courier-Oblique.afm";
            string courierBoldOblique = "PdfSharper.Fonts.AFM.Files.Courier_BoldOblique.afm";

            string param1 = "Courier";
            string param2 = "Courier New";
            string param3 = "CourierStd";
            string param4 = "Cour";
            string param5 = "CoOb";
            string param6 = "CoBo";
            string param7 = "CoBO";

            string source = AFMSource.GetSourceByNameAndAttributes(param1, false, false);
            Assert.IsTrue(source == courier, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param1, true, false);
            Assert.IsTrue(source == courierBold, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param1, false, true);
            Assert.IsTrue(source == courierOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param1, true, true);
            Assert.IsTrue(source == courierBoldOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param2, false, false);
            Assert.IsTrue(source == courier, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param2, true, false);
            Assert.IsTrue(source == courierBold, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param2, false, true);
            Assert.IsTrue(source == courierOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param2, true, true);
            Assert.IsTrue(source == courierBoldOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param3, false, false);
            Assert.IsTrue(source == courier, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param3, true, false);
            Assert.IsTrue(source == courierBold, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param3, false, true);
            Assert.IsTrue(source == courierOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param3, true, true);
            Assert.IsTrue(source == courierBoldOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param4, false, false);
            Assert.IsTrue(source == courier, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param4, true, false);
            Assert.IsTrue(source == courierBold, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param4, false, true);
            Assert.IsTrue(source == courierOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param4, true, true);
            Assert.IsTrue(source == courierBoldOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param5, false, false);
            Assert.IsTrue(source == courierOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param5, true, false);
            Assert.IsTrue(source == courierOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param5, false, true);
            Assert.IsTrue(source == courierOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param5, true, true);
            Assert.IsTrue(source == courierOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param6, true, false);
            Assert.IsTrue(source == courierBold, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param6, false, true);
            Assert.IsTrue(source == courierBold, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param6, true, false);
            Assert.IsTrue(source == courierBold, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param6, true, true);
            Assert.IsTrue(source == courierBold, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param7, false, false);
            Assert.IsTrue(source == courierBoldOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param7, true, false);
            Assert.IsTrue(source == courierBoldOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param7, false, true);
            Assert.IsTrue(source == courierBoldOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param7, true, true);
            Assert.IsTrue(source == courierBoldOblique, "Source didn't return correctly");
        }

        [TestMethod]
        public void GetSourceByNameAndAttributes_HelveticaFamily()
        {
            string helvetica = "PdfSharper.Fonts.AFM.Files.Helvetica.afm";
            string helveticaBold = "PdfSharper.Fonts.AFM.Files.Helvetica-Bold.afm";
            string helveticaOblique = "PdfSharper.Fonts.AFM.Files.Helvetica-Oblique.afm";
            string helveticaBoldOblique = "PdfSharper.Fonts.AFM.Files.Helvetica-BoldOblique.afm";

            string param1 = "Helvetica";
            string param2 = "Helv";
            string param3 = "HeBo";
            string param4 = "HeOb";
            string param5 = "HeBO";
            string param6 = "Arial";
            string param7 = "ArialMT";

            string source = AFMSource.GetSourceByNameAndAttributes(param1, false, false);
            Assert.IsTrue(source == helvetica, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param1, true, false);
            Assert.IsTrue(source == helveticaBold, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param1, false, true);
            Assert.IsTrue(source == helveticaOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param1, true, true);
            Assert.IsTrue(source == helveticaBoldOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param2, false, false);
            Assert.IsTrue(source == helvetica, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param2, true, false);
            Assert.IsTrue(source == helveticaBold, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param2, false, true);
            Assert.IsTrue(source == helveticaOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param2, true, true);
            Assert.IsTrue(source == helveticaBoldOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param3, false, false);
            Assert.IsTrue(source == helveticaBold, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param3, true, false);
            Assert.IsTrue(source == helveticaBold, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param3, false, true);
            Assert.IsTrue(source == helveticaBold, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param3, true, true);
            Assert.IsTrue(source == helveticaBold, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param4, false, false);
            Assert.IsTrue(source == helveticaOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param4, true, false);
            Assert.IsTrue(source == helveticaOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param4, false, true);
            Assert.IsTrue(source == helveticaOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param4, true, true);
            Assert.IsTrue(source == helveticaOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param5, false, false);
            Assert.IsTrue(source == helveticaBoldOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param5, true, false);
            Assert.IsTrue(source == helveticaBoldOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param5, false, true);
            Assert.IsTrue(source == helveticaBoldOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param5, true, true);
            Assert.IsTrue(source == helveticaBoldOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param6, false, false);
            Assert.IsTrue(source == helvetica, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param6, true, false);
            Assert.IsTrue(source == helveticaBold, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param6, false, true);
            Assert.IsTrue(source == helveticaOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param6, true, true);
            Assert.IsTrue(source == helveticaBoldOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param7, false, false);
            Assert.IsTrue(source == helvetica, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param7, true, false);
            Assert.IsTrue(source == helveticaBold, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param7, false, true);
            Assert.IsTrue(source == helveticaOblique, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param7, true, true);
            Assert.IsTrue(source == helveticaBoldOblique, "Source didn't return correctly");
        }

        [TestMethod]
        public void GetSourceByNameAndAttributes_TimesFamily()
        {
            string timesRoman = "PdfSharper.Fonts.AFM.Files.Times-Roman.afm";
            string timesBold = "PdfSharper.Fonts.AFM.Files.Times-Bold.afm";
            string timesItalic = "PdfSharper.Fonts.AFM.Files.Times-Italic.afm";
            string timesBoldItalic = "PdfSharper.Fonts.AFM.Files.Times-BoldItalic.afm";
            string timesNewRoman = "PdfSharper.Fonts.AFM.Files.Times-New-Roman.afm";

            string param1 = "Times Roman";
            string param2 = "TimesRoman";
            string param3 = "Times-Roman";
            string param4 = "Times";
            string param5 = "TiRo";
            string param6 = "TiBo";
            string param7 = "TiIt";
            string param8 = "TiBI";

            string source = AFMSource.GetSourceByNameAndAttributes(param1, false, false);
            Assert.IsTrue(source == timesRoman, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param1, true, false);
            Assert.IsTrue(source == timesBold, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param1, false, true);
            Assert.IsTrue(source == timesItalic, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param1, true, true);
            Assert.IsTrue(source == timesBoldItalic, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param2, false, false);
            Assert.IsTrue(source == timesRoman, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param2, true, false);
            Assert.IsTrue(source == timesBold, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param2, false, true);
            Assert.IsTrue(source == timesItalic, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param2, true, true);
            Assert.IsTrue(source == timesBoldItalic, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param3, false, false);
            Assert.IsTrue(source == timesRoman, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param3, true, false);
            Assert.IsTrue(source == timesBold, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param3, false, true);
            Assert.IsTrue(source == timesItalic, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param3, true, true);
            Assert.IsTrue(source == timesBoldItalic, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param4, false, false);
            Assert.IsTrue(source == timesRoman, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param4, true, false);
            Assert.IsTrue(source == timesBold, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param4, false, true);
            Assert.IsTrue(source == timesItalic, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param4, true, true);
            Assert.IsTrue(source == timesBoldItalic, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param5, false, false);
            Assert.IsTrue(source == timesRoman, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param5, true, false);
            Assert.IsTrue(source == timesBold, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param5, false, true);
            Assert.IsTrue(source == timesItalic, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param5, true, true);
            Assert.IsTrue(source == timesBoldItalic, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param6, false, false);
            Assert.IsTrue(source == timesBold, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param6, true, false);
            Assert.IsTrue(source == timesBold, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param6, false, true);
            Assert.IsTrue(source == timesBold, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param6, true, true);
            Assert.IsTrue(source == timesBold, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param7, false, false);
            Assert.IsTrue(source == timesItalic, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param7, true, false);
            Assert.IsTrue(source == timesItalic, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param7, false, true);
            Assert.IsTrue(source == timesItalic, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param7, true, true);
            Assert.IsTrue(source == timesItalic, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param8, false, false);
            Assert.IsTrue(source == timesBoldItalic, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param8, true, false);
            Assert.IsTrue(source == timesBoldItalic, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param8, false, true);
            Assert.IsTrue(source == timesBoldItalic, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param8, true, true);
            Assert.IsTrue(source == timesBoldItalic, "Source didn't return correctly");
        }

        [TestMethod]
        public void GetSourceByNameAndAttributes_SybolFamily()
        {
            string symbol = "PdfSharper.Fonts.AFM.Files.Symbol.afm";

            string param1 = "Symbol";
            string param2 = "Symb";
            
            string source = AFMSource.GetSourceByNameAndAttributes(param1, false, false);
            Assert.IsTrue(source == symbol, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param1, true, false);
            Assert.IsTrue(source == symbol, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param1, false, true);
            Assert.IsTrue(source == symbol, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param1, true, true);
            Assert.IsTrue(source == symbol, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param2, false, false);
            Assert.IsTrue(source == symbol, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param2, true, false);
            Assert.IsTrue(source == symbol, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param2, false, true);
            Assert.IsTrue(source == symbol, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param2, true, true);
            Assert.IsTrue(source == symbol, "Source didn't return correctly");
        }

        [TestMethod]
        public void GetSourceByNameAndAttributes_ZapDingBatFamily()
        {
            string zapBat = "PdfSharper.Fonts.AFM.Files.ZapfDingbats.afm";

            string param1 = "ZapfDingbats";
            string param2 = "ZaDb";
            string param3 = "ITC Zapf Dingbats";

            string source = AFMSource.GetSourceByNameAndAttributes(param1, false, false);
            Assert.IsTrue(source == zapBat, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param1, true, false);
            Assert.IsTrue(source == zapBat, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param1, false, true);
            Assert.IsTrue(source == zapBat, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param1, true, true);
            Assert.IsTrue(source == zapBat, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param2, false, false);
            Assert.IsTrue(source == zapBat, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param2, true, false);
            Assert.IsTrue(source == zapBat, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param2, false, true);
            Assert.IsTrue(source == zapBat, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param2, true, true);
            Assert.IsTrue(source == zapBat, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param3, false, false);
            Assert.IsTrue(source == zapBat, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param3, true, false);
            Assert.IsTrue(source == zapBat, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param3, false, true);
            Assert.IsTrue(source == zapBat, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param3, true, true);
            Assert.IsTrue(source == zapBat, "Source didn't return correctly");
        }

        [TestMethod]
        public void GetSourceByNameAndAttributes_SequeoFamily()
        {
            string segoe = "PdfSharper.Fonts.AFM.Files.segoeui.afm";
            string segoeBold = "PdfSharper.Fonts.AFM.Files.segoeuib.afm";
            string segoeItacic = "PdfSharper.Fonts.AFM.Files.segoeuii.afm";
            string segoeBoldItalic = "PdfSharper.Fonts.AFM.Files.segoeuiz.afm";

            string param1 = "Segoe UI";

            string source = AFMSource.GetSourceByNameAndAttributes(param1, false, false);
            Assert.IsTrue(source == segoe, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param1, true, false);
            Assert.IsTrue(source == segoeBold, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param1, false, true);
            Assert.IsTrue(source == segoeItacic, "Source didn't return correctly");

            source = AFMSource.GetSourceByNameAndAttributes(param1, true, true);
            Assert.IsTrue(source == segoeBoldItalic, "Source didn't return correctly");
        }
    }
}
