namespace PdfSharper.Pdf.Advanced
{
    public class PdfLinearizationParameters : PdfDictionary
    {
        public PdfLinearizationParameters(PdfDictionary dict)
            : base(dict)
        { }


        public int Version
        {
            get
            {
                return Elements.GetInteger(Keys.Linearized);
            }
        }

        public int Length
        {
            get
            {
                return Elements.GetInteger(Keys.Length);
            }
            set
            {
                Elements.SetInteger(Keys.Length, value);
            }
        }

        public int FirstPageObjectID
        {
            get
            {
                return Elements.GetInteger(Keys.FirstPageObject);
            }
            set
            {
                Elements.SetInteger(Keys.FirstPageObject, value);
            }
        }


        public int EndOfFirstPage
        {
            get
            {
                return Elements.GetInteger(Keys.EndOfFirstPage);
            }
            set
            {
                Elements.SetInteger(Keys.EndOfFirstPage, value);
            }
        }

        public int PageCount
        {
            get
            {
                return Elements.GetInteger(Keys.PageCount);
            }
            set
            {
                Elements.SetInteger(Keys.PageCount, value);
            }
        }

        public int MainCrossReferenceStreamOffset
        {
            get
            {
                return Elements.GetInteger(Keys.MainCrossReferenceStreamOffset);
            }
            set
            {
                Elements.SetInteger(Keys.MainCrossReferenceStreamOffset, value);
            }
        }

        public PdfDictionary HintStream { get; internal set; }

        public class Keys : KeysBase
        {
            /// <summary>
            /// (Required) The version of linearization applied to this document.
            /// See Appendix F, page 1026.
            /// </summary>
            [KeyInfo(KeyType.Name | KeyType.Required)]
            public const string Linearized = "/Linearized";

            /// <summary>
            /// File Length
            /// </summary>
            [KeyInfo(KeyType.Integer | KeyType.Required)]
            public const string Length = "/L";

            /// <summary>
            /// Primary hint stream and offset
            /// </summary>
            [KeyInfo(KeyType.Array | KeyType.Required)]
            public const string Hint = "/H";

            /// <summary>
            /// Object number of the first page's page object
            /// </summary>
            [KeyInfo(KeyType.Integer | KeyType.Required)]
            public const string FirstPageObject = "/O";

            /// <summary>
            /// Offset of end of the first page.
            /// </summary>
            [KeyInfo(KeyType.Name | KeyType.Required)]
            public const string EndOfFirstPage = "/E";

            [KeyInfo(KeyType.Integer | KeyType.Required)]
            public const string PageCount = "/N";

            /// <summary>
            /// Offset of first entry in main cross-reference table
            /// </summary>
            [KeyInfo(KeyType.Name | KeyType.Required)]
            public const string MainCrossReferenceStreamOffset = "/T";
        }
    }
}
