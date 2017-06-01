using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfSharper.Pdf.Advanced
{
    internal class PdfTraversalUtility
    {
        /// <summary>
        /// Calculates the transitive closure of the specified PdfObject with the specified depth, i.e. all indirect objects
        /// recursively reachable from the specified object in up to maximally depth steps.
        /// </summary>
        internal static KeyValuePair<PdfReference, int>[] TransitiveClosure(PdfObject pdfObject, HashSet<PdfItem> exclusions = null)
        {
            HashSet<PdfItem> overflow = new HashSet<PdfItem>();
            Dictionary<PdfItem, int> objectmap = new Dictionary<PdfItem, int>();

            if (exclusions != null)
            {
                foreach (PdfItem item in exclusions)
                {
                    objectmap.Add(item, 1);
                }
            }

            int nestingLevel = 0;
            TransitiveClosureImplementation(pdfObject, objectmap, overflow, nestingLevel);
            TryAgain:
            if (overflow.Count > 0)
            {
                PdfObject[] array = new PdfObject[overflow.Count];
                overflow.CopyTo(array, 0);
                overflow = new HashSet<PdfItem>();
                for (int idx = 0; idx < array.Length; idx++)
                {
                    PdfObject obj = array[idx];
                    TransitiveClosureImplementation(obj, objectmap, overflow, nestingLevel);
                }
                goto TryAgain;
            }

            if (exclusions != null)
            {
                foreach (PdfItem item in exclusions)
                {
                    objectmap.Remove(item);
                }
            }
            return objectmap.Select(kvp => new KeyValuePair<PdfReference, int>((PdfReference)kvp.Key, kvp.Value)).ToArray();
        }


        private static void TransitiveClosureImplementation(PdfObject pdfObject, Dictionary<PdfItem, int> objectMap, HashSet<PdfItem> overflow, int nestingLevel)
        {
            try
            {
                nestingLevel++;
                if (nestingLevel >= 1000)
                {
                    overflow.Add(pdfObject);
                    return;
                }

                IEnumerable enumerable = null; //(IEnumerator)pdfObject;
                PdfDictionary dict;
                PdfArray array;
                if ((dict = pdfObject as PdfDictionary) != null)
                    enumerable = dict.Elements.Values;
                else if ((array = pdfObject as PdfArray) != null)
                    enumerable = array.Elements;

                if (enumerable != null)
                {
                    foreach (PdfItem item in enumerable)
                    {
                        PdfReference iref = item as PdfReference;
                        if (iref != null)
                        {
                            if (!objectMap.ContainsKey(iref))
                            {
                                PdfObject value = iref.Value;

                                // Ignore unreachable objets.
                                if (iref.Document != null)
                                {
                                    // ... from trailer hack
                                    if (value == null)
                                    {
                                        //iref = ObjectTable[iref.ObjectID];
                                        //Debug.Assert(iref.Value != null);
                                        //value = iref.Value;
                                    }
                                    objectMap.Add(iref, 1);
                                    if (value is PdfArray || value is PdfDictionary)
                                        TransitiveClosureImplementation(value, objectMap, overflow, nestingLevel);
                                }
                            }
                            else
                            {
                                objectMap[iref]++;
                            }
                        }
                        else
                        {
                            PdfObject subObject = item as PdfObject;
                            if (subObject != null && (subObject is PdfDictionary || subObject is PdfArray))
                                TransitiveClosureImplementation(subObject, objectMap, overflow, nestingLevel);
                        }
                    }
                }

                if (pdfObject is PdfCrossReferenceStream)
                {

                    objectMap.Add(pdfObject.Reference, 1);

                    PdfCrossReferenceStream xRefStream = pdfObject as PdfCrossReferenceStream;
                    foreach (var entry in xRefStream.Entries)
                    {
                        if (entry.Type == 2)
                        {
                            PdfReference iref = xRefStream.XRefTable.AllReferences.FirstOrDefault(ir => ir.ObjectNumber == entry.Field2);
                            if (!objectMap.ContainsKey(iref))
                            {
                                objectMap.Add(iref, 1);
                            }
                            else
                            {
                                objectMap[iref]++;
                            }
                        }

                    }
                }
            }
            finally
            {
                nestingLevel--;
            }
        }
    }
}
