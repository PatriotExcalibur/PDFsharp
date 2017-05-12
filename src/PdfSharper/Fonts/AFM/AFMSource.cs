using System;
using System.Collections.Generic;

namespace PdfSharper.Fonts.AFM
{
    public class AFMSource
    {
        private static readonly string COURIERBOLD = "PdfSharper.Fonts.AFM.Files.Courier-Bold.afm";
        private static readonly string COURIEROBLIQUE = "PdfSharper.Fonts.AFM.Files.Courier-Oblique.afm";
        private static readonly string COURIER = "PdfSharper.Fonts.AFM.Files.Courier.afm";
        private static readonly string COURIERBOLDOBLIQUE = "PdfSharper.Fonts.AFM.Files.Courier_BoldOblique.afm";
        private static readonly string HELVETICABOLD = "PdfSharper.Fonts.AFM.Files.Helvetica-Bold.afm";
        private static readonly string HELVETICABOLDOBLIQUE = "PdfSharper.Fonts.AFM.Files.Helvetica-BoldOblique.afm";
        private static readonly string HELVETICAOBLIQUE = "PdfSharper.Fonts.AFM.Files.Helvetica-Oblique.afm";
        private static readonly string HELVETICA = "PdfSharper.Fonts.AFM.Files.Helvetica.afm";
        private static readonly string SYMBOL = "PdfSharper.Fonts.AFM.Files.Symbol.afm";
        private static readonly string TIMESBOLD = "PdfSharper.Fonts.AFM.Files.Times-Bold.afm";
        private static readonly string TIMESBOLDITALIC = "PdfSharper.Fonts.AFM.Files.Times-BoldItalic.afm";
        private static readonly string TIMESITALIC = "PdfSharper.Fonts.AFM.Files.Times-Italic.afm";
        private static readonly string TIMESNEWROMAN = "PdfSharper.Fonts.AFM.Files.Times-New-Roman.afm";
        private static readonly string TIMESROMAN = "PdfSharper.Fonts.AFM.Files.Times-Roman.afm";
        private static readonly string ZAPFDINGBATS = "PdfSharper.Fonts.AFM.Files.ZapfDingbats.afm";
        private static readonly string SEGOEUI = "PdfSharper.Fonts.AFM.Files.segoeui.afm";
        private static readonly string SEGOEUIBOLD = "PdfSharper.Fonts.AFM.Files.segoeuib.afm";
        private static readonly string SEGOEUIITALIC = "PdfSharper.Fonts.AFM.Files.segoeuii.afm";
        private static readonly string SEGOEUIBOLDITALIC = "PdfSharper.Fonts.AFM.Files.segoeuiz.afm";

        private static Dictionary<string, string[]> _fieldFontNameMap = new Dictionary<string, string[]>()
        {
            //courier family
			{ "Courier", new string[] { COURIER, COURIERBOLD, COURIEROBLIQUE, COURIERBOLDOBLIQUE } },
            { "Courier New", new string[] { COURIER, COURIERBOLD, COURIEROBLIQUE, COURIERBOLDOBLIQUE } },
            { "CourierStd", new string[] { COURIER, COURIERBOLD, COURIEROBLIQUE, COURIERBOLDOBLIQUE } },
            { "Cour", new string[] { COURIER, COURIERBOLD, COURIEROBLIQUE, COURIERBOLDOBLIQUE } },
            { "CoOb", new string[] { COURIEROBLIQUE, COURIEROBLIQUE, COURIEROBLIQUE, COURIEROBLIQUE } },
            { "CoBo", new string[] { COURIERBOLD, COURIERBOLD, COURIERBOLD, COURIERBOLD } },
            { "CoBO", new string[] { COURIERBOLDOBLIQUE, COURIERBOLDOBLIQUE, COURIERBOLDOBLIQUE, COURIERBOLDOBLIQUE } },


            //Helvetica family
            { "Helvetica", new string[] { HELVETICA, HELVETICABOLD, HELVETICAOBLIQUE, HELVETICABOLDOBLIQUE} },
            { "Helv", new string[] { HELVETICA, HELVETICABOLD, HELVETICAOBLIQUE, HELVETICABOLDOBLIQUE} },
            { "HeBo", new string[] { HELVETICABOLD, HELVETICABOLD, HELVETICABOLD, HELVETICABOLD } },
            { "HeOb", new string[] { HELVETICAOBLIQUE, HELVETICAOBLIQUE, HELVETICAOBLIQUE, HELVETICAOBLIQUE } },
            { "HeBO", new string[] { HELVETICABOLDOBLIQUE, HELVETICABOLDOBLIQUE, HELVETICABOLDOBLIQUE, HELVETICABOLDOBLIQUE } },
            { "Arial", new string[] { HELVETICA, HELVETICABOLD, HELVETICAOBLIQUE, HELVETICABOLDOBLIQUE} },
            { "ArialMT", new string[] { HELVETICA, HELVETICABOLD, HELVETICAOBLIQUE, HELVETICABOLDOBLIQUE} },
            

            //Times Roman Family
            { "Times Roman", new string[] { TIMESROMAN, TIMESBOLD, TIMESITALIC, TIMESBOLDITALIC} },
            { "TimesRoman", new string[] { TIMESROMAN, TIMESBOLD, TIMESITALIC, TIMESBOLDITALIC} },
            { "Times-Roman", new string[] { TIMESROMAN, TIMESBOLD, TIMESITALIC, TIMESBOLDITALIC} },
            { "Times", new string[] { TIMESROMAN, TIMESBOLD, TIMESITALIC, TIMESBOLDITALIC} },
            { "TiRo", new string[] { TIMESROMAN, TIMESBOLD, TIMESITALIC, TIMESBOLDITALIC} },
            { "TiBo", new string[] { TIMESBOLD, TIMESBOLD, TIMESBOLD, TIMESBOLD } },
            { "TiIt", new string[] { TIMESITALIC, TIMESITALIC, TIMESITALIC, TIMESITALIC } },
            { "TiBI", new string[] { TIMESBOLDITALIC, TIMESBOLDITALIC, TIMESBOLDITALIC, TIMESBOLDITALIC } },


            //Times New Roman Family
            { "Times New Roman", new string[] { TIMESNEWROMAN, TIMESNEWROMAN, TIMESNEWROMAN, TIMESNEWROMAN } },
            { "TimesNewRoman", new string[] { TIMESNEWROMAN, TIMESNEWROMAN, TIMESNEWROMAN, TIMESNEWROMAN } },
            { "TimesNewRomanPSMT", new string[] { TIMESNEWROMAN, TIMESNEWROMAN, TIMESNEWROMAN, TIMESNEWROMAN } },
            
            
            //Symbol family
            { "Symbol", new string[] { SYMBOL, SYMBOL, SYMBOL, SYMBOL } },
            { "Symb", new string[] { SYMBOL, SYMBOL, SYMBOL, SYMBOL } },


            //Dingbat family 
            { "ZapfDingbats", new string[] { ZAPFDINGBATS, ZAPFDINGBATS, ZAPFDINGBATS, ZAPFDINGBATS } },
            { "ZaDb", new string[] { ZAPFDINGBATS, ZAPFDINGBATS, ZAPFDINGBATS, ZAPFDINGBATS } },
            { "ITC Zapf Dingbats", new string[] { ZAPFDINGBATS, ZAPFDINGBATS, ZAPFDINGBATS, ZAPFDINGBATS } },

            //Segoeui Family
            { "Segoe UI", new string[] { SEGOEUI, SEGOEUIBOLD, SEGOEUIITALIC, SEGOEUIBOLDITALIC } }
        };

        public static string GetSourceByNameAndAttributes(string name, bool isBold, bool isItalic)
        {
            int subIndex = 0;
            if (isBold)
                subIndex++;

            if (isItalic)
                subIndex += 2;

            string[] source = null;

            if (_fieldFontNameMap.TryGetValue(name, out source))
            {
                return source[subIndex];
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
