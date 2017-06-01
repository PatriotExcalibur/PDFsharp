using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfSharper.Pdf.Advanced
{
    /// <summary>
    /// Tagged pdfs will have this structure, this is a very early implementation.
    /// </summary>
    public class PdfStructure : PdfDictionary
    {
        private Dictionary<int, int> _allReferences;

        public Dictionary<int, int> AllReferences
        {
            get
            {
                lock (this)
                {
                    if (_allReferences == null)
                    {
                        _allReferences = PdfTraversalUtility.TransitiveClosure(this)
                            .ToDictionary(kvp => kvp.Key.ObjectNumber, kvp => kvp.Value);
                    }
                }
                return _allReferences;
            }
        }

        /// <summary>
        /// Constructs the structure from an existing document.
        /// Currently only used to cache the refs.
        /// </summary>
        /// <param name="dict"></param>
        public PdfStructure(PdfDictionary dict) : base(dict)
        {
        }
    }
}
