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
using System.Diagnostics;
using System.IO;
#if NETFX_CORE
using System.Threading.Tasks;
#endif
using PdfSharper.Pdf.Advanced;
using PdfSharper.Pdf.Internal;
using PdfSharper.Pdf.IO;
using PdfSharper.Pdf.AcroForms;
using PdfSharper.Pdf.Security;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable ConvertPropertyToExpressionBody

namespace PdfSharper.Pdf
{
    internal class PdfDocumentEventArgs : EventArgs
    {
        public PdfWriter Writer { get; set; }
    }
    /// <summary>
    /// Represents a PDF document.
    /// </summary>
    [DebuggerDisplay("(Name={Name})")] // A name makes debugging easier
    public sealed class PdfDocument : PdfObject, IDisposable
    {

        internal event EventHandler BeforeSave = (s, e) => { };
        internal event EventHandler<PdfDocumentEventArgs> AfterSave = (s, e) => { };
        internal DocumentState _state;
        internal PdfDocumentOpenMode _openMode;

        internal byte[] fileContents;

#if DEBUG_
        static PdfDocument()
        {
            PSSR.TestResourceMessages();
            //string test = PSSR.ResMngr.GetString("SampleMessage1");
            //test.GetType();
        }
#endif

        /// <summary>
        /// Creates a new PDF document in memory.
        /// To open an existing PDF file, use the PdfReader class.
        /// </summary>
        public PdfDocument()
        {
            //PdfDocument.Gob.AttatchDocument(Handle);

            _creation = DateTime.Now;
            _state = DocumentState.Created;
            _version = 14;
            Initialize();
            Info.CreationDate = _creation;
        }

        /// <summary>
        /// Creates a new PDF document with the specified file name. The file is immediately created and keeps
        /// locked until the document is closed, at that time the document is saved automatically.
        /// Do not call Save() for documents created with this constructor, just call Close().
        /// To open an existing PDF file and import it, use the PdfReader class.
        /// </summary>
        public PdfDocument(string filename)
        {
            //PdfDocument.Gob.AttatchDocument(Handle);

            _creation = DateTime.Now;
            _state = DocumentState.Created;
            _version = 14;
            Initialize();
            Info.CreationDate = _creation;

            // TODO 4STLA: encapsulate the whole c'tor with #if !NETFX_CORE?
#if !NETFX_CORE
            _outStream = new FileStream(filename, FileMode.Create);
#else
            throw new NotImplementedException();
#endif
        }

        /// <summary>
        /// Creates a new PDF document using the specified stream.
        /// The stream won't be used until the document is closed, at that time the document is saved automatically.
        /// Do not call Save() for documents created with this constructor, just call Close().
        /// To open an existing PDF file, use the PdfReader class.
        /// </summary>
        public PdfDocument(Stream outputStream)
        {
            //PdfDocument.Gob.AttatchDocument(Handle);

            _creation = DateTime.Now;
            _state = DocumentState.Created;
            Initialize();
            Info.CreationDate = _creation;

            _outStream = outputStream;
        }

        internal PdfDocument(Lexer lexer)
        {
            UnderConstruction = true;
            //PdfDocument.Gob.AttatchDocument(Handle);

            _creation = DateTime.Now;
            _state = DocumentState.Imported;

            //_info = new PdfInfo(this);
            //_pages = new PdfPages(this);
            //_fontTable = new PdfFontTable();
            //_catalog = new PdfCatalog(this);
            ////_font = new PdfFont();
            //_objects = new PdfObjectTable(this);
            //_trailer = new PdfTrailer(this);
            _irefTable = new PdfCrossReferenceTable(this);
            _lexer = lexer;
        }

        void Initialize()
        {
            //_info = new PdfInfo(this);
            _fontTable = new PdfFontTable(this);
            _imageTable = new PdfImageTable(this);
            _trailer = new PdfTrailer(this);

            _irefTable = new PdfCrossReferenceTable(this);
            _trailer.XRefTable = _irefTable;
            _trailer.CreateNewDocumentIDs();
            _trailers.Add(_trailer);
        }

        //~PdfDocument()
        //{
        //  Dispose(false);
        //}

        /// <summary>
        /// Disposes all references to this document stored in other documents. This function should be called
        /// for documents you finished importing pages from. Calling Dispose is technically not necessary but
        /// useful for earlier reclaiming memory of documents you do not need anymore.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            //GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing)
        {
            if (_state != DocumentState.Disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                }
                //PdfDocument.Gob.DetatchDocument(Handle);
            }
            _state = DocumentState.Disposed;
        }

        /// <summary>
        /// Gets or sets a user defined object that contains arbitrary information associated with this document.
        /// The tag is not used by PdfSharper.
        /// </summary>
        public object Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
        object _tag;

        /// <summary>
        /// Gets or sets a value used to distinguish PdfDocument objects.
        /// The name is not used by PdfSharper.
        /// </summary>
        string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        string _name = NewName();

        /// <summary>
        /// Get a new default name for a new document.
        /// </summary>
        static string NewName()
        {
#if DEBUG_
            if (PdfDocument.nameCount == 57)
                PdfDocument.nameCount.GetType();
#endif
            return "Document " + _nameCount++;
        }
        static int _nameCount;

        internal bool CanModify
        {
            //get {return _state == DocumentState.Created || _state == DocumentState.Modifyable;}
            get { return true; }
        }

        public PdfLinearizationParameters LinearizationParamaters { get; internal set; }

        public bool IsLinearized { get { return LinearizationParamaters != null; } }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            if (!CanModify)
                throw new InvalidOperationException(PSSR.CannotModify);

            if (_outStream != null)
            {
                // Get security handler if document gets encrypted
                PdfStandardSecurityHandler securityHandler = null;
                if (SecuritySettings.DocumentSecurityLevel != PdfDocumentSecurityLevel.None)
                    securityHandler = SecuritySettings.SecurityHandler;

                PdfWriter writer = new PdfWriter(_outStream, securityHandler);
                try
                {
                    DoSave(writer);
                }
                finally
                {
                    writer.Close();
                }
            }
        }

#if true //!NETFX_CORE
        /// <summary>
        /// Saves the document to the specified path. If a file already exists, it will be overwritten.
        /// </summary>
        public void Save(string path)
        {
            if (!CanModify)
                throw new InvalidOperationException(PSSR.CannotModify);

#if !NETFX_CORE
            using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
            {
                Save(stream);
            }
#else
            var task = SaveAsync(path, true);

            ////var file = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync("MyWav.wav", Windows.Storage.CreationCollisionOption.ReplaceExisting);
            ////var stream = file.OpenStreamForWriteAsync();
            ////var writer = new StreamWriter(stream);
            ////Save(stream);

            //var ms = new MemoryStream();
            //Save(ms, false);
            //byte[] pdf = ms.ToArray();
            //ms.Close();
#endif
        }
#endif

#if NETFX_CORE
        /// <summary>
        /// Saves the document to the specified path. If a file already exists, it will be overwritten.
        /// </summary>
        public async Task SaveAsync(string path, bool closeStream)
        {
            if (!CanModify)
                throw new InvalidOperationException(PSSR.CannotModify);

            // Just march through...

            var file = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync("My1st.pdf", Windows.Storage.CreationCollisionOption.ReplaceExisting);
            var stream = await file.OpenStreamForWriteAsync();
            using (var writer = new StreamWriter(stream))
            {
                Save(stream, false);
            }

            //var ms = new MemoryStream();
            //Save(ms, false);
            //byte[] pdf = ms.ToArray();
            //ms.Close();
            //await stream.WriteAsync(pdf, 0, pdf.Length);
            //stream.Close();
        }
#endif

        /// <summary>
        /// Saves the document to the specified stream.
        /// </summary>
        public void Save(Stream stream, bool closeStream)
        {
            if (!CanModify)
                throw new InvalidOperationException(PSSR.CannotModify);

            // TODO: more diagnostic checks
            string message = "";
            if (!CanSave(ref message))
                throw new PdfSharpException(message);

            // Get security handler if document gets encrypted.
            PdfStandardSecurityHandler securityHandler = null;
            if (SecuritySettings.DocumentSecurityLevel != PdfDocumentSecurityLevel.None)
                securityHandler = SecuritySettings.SecurityHandler;

            PdfWriter writer = null;
            try
            {
                writer = new PdfWriter(stream, securityHandler);
                DoSave(writer);
            }
            finally
            {
                if (stream != null)
                {
                    if (closeStream)
#if UWP
                        stream.Dispose();
#else
                        stream.Close();
#endif
                    else
                        stream.Position = 0; // Reset the stream position if the stream is kept open.
                }
                if (writer != null)
                    writer.Close(closeStream);
            }
        }

        /// <summary>
        /// Saves the document to the specified stream.
        /// The stream is not closed by this function.
        /// (Older versions of PDFsharp closes the stream. That was not very useful.)
        /// </summary>
        public void Save(Stream stream)
        {
            Save(stream, false);
        }

        /// <summary>
        /// Implements saving a PDF file.
        /// </summary>
        void DoSave(PdfWriter writer)
        {
            this.BeforeSave(this, EventArgs.Empty);

            if (_pages == null || _pages.Count == 0)
            {
                if (_outStream != null)
                {
                    // Give feedback if the wrong constructor was used.
                    throw new InvalidOperationException("Cannot save a PDF document with no pages. Do not use \"public PdfDocument(string filename)\" or \"public PdfDocument(Stream outputStream)\" if you want to open an existing PDF document from a file or stream; use PdfReader.Open() for that purpose.");
                }
                throw new InvalidOperationException("Cannot save a PDF document with no pages.");
            }

            try
            {
                bool encrypt = _securitySettings.DocumentSecurityLevel != PdfDocumentSecurityLevel.None;
                if (encrypt)
                {
                    PdfStandardSecurityHandler securityHandler = _securitySettings.SecurityHandler;
                    if (securityHandler.Reference == null)
                        _irefTable.Add(securityHandler);
                    else
                        Debug.Assert(_irefTable.Contains(securityHandler.ObjectID));
                    _trailer.Elements[PdfTrailer.Keys.Encrypt] = _securitySettings.SecurityHandler.Reference;
                }
                else
                    _trailer.Elements.Remove(PdfTrailer.Keys.Encrypt);

                PrepareForSave();

                PdfTrailer writeableTrailer = _trailers.SingleOrDefault(t => t.IsReadOnly == false && t.Next == null);
                if (writeableTrailer != null)
                {
                    writeableTrailer.Info.ModificationDate = DateTime.Now;
                }

                if (encrypt)
                    _securitySettings.SecurityHandler.PrepareEncryption();

                if (fileContents == null)
                {
                    writer.WriteFileHeader(this);
                }

                if (fileContents != null)
                {
                    writer.Stream.Write(fileContents, 0, fileContents.Length);

                    if (writeableTrailer != null)
                    {
                        WriteTrailer(writer, writeableTrailer);
                    }
                }
                else
                {
                    if (IsLinearized) //write out objects in their original order
                    {
                        Linearize(); //update object placement 
                        var firstPageCrossReferenceTable = _trailers.OfType<PdfCrossReferenceStream>()
                                                    .OrderBy(t => t.StartXRef)
                                                    .FirstOrDefault();

                        LinearizationParamaters.Reference.Position = writer.Position;
                        LinearizationParamaters.Write(writer);
                        writer.WriteRaw(new string(' ', 19));
                        writer.WriteRaw("\r\n");

                        firstPageCrossReferenceTable.StartXRef = writer.Position;
                        firstPageCrossReferenceTable.Reference.Position = firstPageCrossReferenceTable.StartXRef;
                        firstPageCrossReferenceTable.Write(writer);

                        writer.WriteEof(this, 0); //the first linear xref always starts at 0
                                                  //padding for when the stream is updated with the object positions
                        writer.WriteRaw(new string(' ', _irefTable._maxObjectNumber));
                        writer.WriteRaw("\r\n");

                        PdfObject viewerPrefs = Catalog.Elements.GetObject(PdfCatalog.Keys.ViewerPreferences);
                        WriteOptionalObject(writer, viewerPrefs);

                        if (Catalog.PageMode != PdfPageMode.UseNone) //ok now what?
                        {
                        }

                        PdfObject threads = Catalog.Elements.GetObject(PdfCatalog.Keys.Threads);
                        if (threads != null)
                        {
                            throw new NotSupportedException("Linearized Documents with threads are not supported yet.");
                        }

                        PdfObject openAction = Catalog.Elements.GetObject(PdfCatalog.Keys.OpenAction);
                        WriteOptionalObject(writer, openAction);

                        int writerStartPosition = writer.Position;
                        LinearizationParamaters.Elements.GetArray(PdfLinearizationParameters.Keys.Hint).Elements[0] = new PdfInteger(writer.Position);

                        LinearizationParamaters.HintStream.Reference.Position = writer.Position;
                        LinearizationParamaters.HintStream.Write(writer);

                        LinearizationParamaters.Elements.GetArray(PdfLinearizationParameters.Keys.Hint).Elements[1] = new PdfInteger(writer.Position - writerStartPosition);

                        Catalog.Reference.Position = writer.Position;
                        Catalog.Write(writer);

                        if (firstPageCrossReferenceTable.Elements.ContainsKey(PdfCrossReferenceStream.Keys.Encrypt))
                        {
                            throw new NotSupportedException("Encrypted linearized documents not supported");
                        }

                        foreach (PdfReference iref in firstPageCrossReferenceTable.XRefTable.AllReferences)
                        {
                            if (iref == LinearizationParamaters.Reference ||
                                iref == firstPageCrossReferenceTable.Reference ||
                                iref == viewerPrefs?.Reference ||
                                iref == openAction?.Reference ||
                                iref == LinearizationParamaters.HintStream.Reference ||
                                iref == Catalog.Reference ||
                                !iref.ContainingStreamID.IsEmpty)
                            {
                                continue;
                            }

                            iref.Position = writer.Position;
                            iref.Value.Write(writer);
                        }

                        //write the object streams?

                        LinearizationParamaters.EndOfFirstPage = writer.Position;

                        var trailer = firstPageCrossReferenceTable.Prev;
                        while (trailer != null)
                        {
                            if (trailer != firstPageCrossReferenceTable)
                            {
                                WriteTrailer(writer, trailer, firstPageCrossReferenceTable);
                            }
                            trailer = trailer.Next;
                        }

                        LinearizationParamaters.Length = writer.Position;
                        writer.Stream.Seek(LinearizationParamaters.Reference.Position, SeekOrigin.Begin);
                        LinearizationParamaters.Write(writer);

                        //update positions of objects in start stream since it is written before the objects
                        writer.Stream.Seek(firstPageCrossReferenceTable.StartXRef, SeekOrigin.Begin);
                        firstPageCrossReferenceTable.Write(writer);
                        writer.WriteEof(this, 0); //the first linear xref always starts at 0
                        writer.Stream.Seek(0, SeekOrigin.End);
                    }
                    else
                    {
                        var trailer = _trailers.SingleOrDefault(t => t.Prev == null);
                        while (trailer != null)
                        {
                            WriteTrailer(writer, trailer);
                            trailer = trailer.Next;
                        }
                    }
                }


                //if (encrypt)
                //{
                //  state &= ~DocumentState.SavingEncrypted;
                //  //_securitySettings.SecurityHandler.EncryptDocument();
                //}
            }
            finally
            {
                if (writer != null)
                {
                    this.AfterSave(this, new PdfDocumentEventArgs() { Writer = writer });
                    writer.Stream.Flush();
                    // DO NOT CLOSE WRITER HERE
                    //writer.Close();
                }
            }
        }

        private static void WriteOptionalObject(PdfWriter writer, PdfObject pdfObject)
        {
            if (pdfObject != null)
            {
                pdfObject.Reference.Position = writer.Position;
                pdfObject.Write(writer);
            }
        }

        internal PdfTrailer GetWritableTrailer(PdfObjectID forObjectID)
        {
            if (_trailers.Count == 1)
            {
                return _trailers.FirstOrDefault();
            }

            if (_trailers.All(t => t.IsReadOnly == false))
            {
                foreach (PdfTrailer trailer in _trailers)
                {
                    if ((trailer.XRefTable.Contains(forObjectID) || trailer.XRefTable._maxObjectNumber + 1 == forObjectID.ObjectNumber)
                        && (trailer.Next == null || !trailer.Next.XRefTable.Contains(forObjectID)))
                    {
                        return trailer;
                    }
                }

                return MakeNewTrailer();


            }

            if (_trailers.All(t => t.IsReadOnly))
            {
                return MakeNewTrailer();
            }

            return _trailers.SingleOrDefault(t => t.IsReadOnly == false);
        }
        internal PdfTrailer MakeNewTrailer()
        {
            PdfTrailer endingTrailer = new PdfTrailer(this);
            endingTrailer.XRefTable = new PdfCrossReferenceTable(this);

            PdfDictionary trailerInfo = Info.Clone();
            trailerInfo.Document = this;
            PdfReference infoReference = new PdfReference(Info.ObjectID, -1);
            infoReference.Value = trailerInfo;

            endingTrailer.XRefTable.Add(infoReference);

            endingTrailer.Elements.SetReference(PdfTrailer.Keys.Info, infoReference);

            //TODO: Document trailer root ok?
            endingTrailer.Elements.SetReference(PdfTrailer.Keys.Root, _trailer.Root);

            string documentID = _trailer.GetDocumentID(0);
            endingTrailer.SetDocumentID(0, documentID);

            var mostRecent = _trailers.SingleOrDefault(t => t.Next == null);
            endingTrailer.Prev = mostRecent;
            mostRecent.Next = endingTrailer;

            _trailers.Insert(0, endingTrailer);

            return endingTrailer;
        }

        private void WriteTrailer(PdfWriter writer, PdfTrailer trailer, PdfCrossReferenceStream linearXRefTrailer = null)
        {
            if (trailer is PdfCrossReferenceStream)
            {
                WriteCrossReferenceTrailer(writer, trailer as PdfCrossReferenceStream, linearXRefTrailer);
                return;
            }
            PdfReference[] irefs = trailer.XRefTable.AllReferences;
            int count = irefs.Length;
            for (int idx = 0; idx < count; idx++)
            {
                PdfReference iref = irefs[idx];
#if DEBUG_
                    if (iref.ObjectNumber == 378)
                        GetType();
#endif
                iref.Position = writer.Position;
                iref.Value.Write(writer);
            }
            trailer.StartXRef = writer.Position;
            trailer.XRefTable.WriteObject(writer);
            writer.WriteRaw("trailer\r\n");

            if (trailer.IsReadOnly == false)
            {
                trailer.Elements.SetInteger("/Size", trailer.XRefTable._maxObjectNumber + 1); //0 record isn't in count
            }

            if (trailer.Prev != null)
            {
                Debug.Assert(trailer.Prev.StartXRef != -1, "Previous trailer was not written yet");
                trailer.Elements.SetInteger("/Prev", trailer.Prev.StartXRef);
            }

            trailer.Write(writer);
            writer.WriteEof(this, trailer.StartXRef);
        }

        private void WriteCrossReferenceTrailer(PdfWriter writer, PdfCrossReferenceStream trailer, PdfCrossReferenceStream linearXRefTrailer)
        {
            //TODO: grouping of sequential objects
            PdfReference[] irefs = trailer.XRefTable.AllReferences;
            int count = irefs.Length;
            for (int idx = 0; idx < count; idx++)
            {
                PdfReference iref = irefs[idx];
                if (!iref.ContainingStreamID.IsEmpty ||
                    iref.Value == trailer)
                {
                    continue;
                }

                iref.Position = writer.Position;
                iref.Value.Write(writer);
            }

            if (trailer.Next == null || trailer.Next != linearXRefTrailer)
            {
                trailer.StartXRef = writer.Position;
            }
            else
            {
                LinearizationParamaters.MainCrossReferenceStreamOffset = writer.Position;
                linearXRefTrailer.Elements.SetInteger(PdfCrossReferenceStream.Keys.Prev, writer.Position);
                trailer.StartXRef = linearXRefTrailer.StartXRef;
            }

            trailer.Reference.Position = writer.Position;
            trailer.Write(writer);
            writer.WriteEof(this, trailer.StartXRef);
        }


        public void Linearize()
        {
            if (!IsLinearized)
            {
                throw new NotSupportedException("We can only update linearized documents at this moment.");
            }

            var firstPageCrossReferenceTable = _trailers.OfType<PdfCrossReferenceStream>()
                                        .OrderBy(t => t.StartXRef)
                                        .FirstOrDefault();

            PdfReference[] firstPageRefs = GatherFirstPageAndDocumentReferences();

            Dictionary<PdfReference, PdfCrossReferenceStream> refLookup = _trailers.OfType<PdfCrossReferenceStream>()
                                                                                   .SelectMany(pcrs => pcrs.XRefTable.AllReferences.Select(iref => new
                                                                                   {
                                                                                       Reference = iref,
                                                                                       Trailer = pcrs
                                                                                   }))
                                                                                   .GroupBy(anon => anon.Reference, (a, b) => b.OrderByDescending(x => x.Trailer.Reference.Position).FirstOrDefault())
                                                                                   .ToDictionary(k => k.Reference, v => v.Trailer);

            foreach (PdfReference iref in firstPageRefs)
            {
                if (!firstPageCrossReferenceTable.XRefTable.Contains(iref.ObjectID))
                {
                    PdfCrossReferenceStream containingStream = refLookup[iref];
                    PdfCrossReferenceStream.CrossReferenceStreamEntry irefEntry = containingStream.Entries.FirstOrDefault(e => e.ObjectNumber == iref.ObjectNumber);

                    containingStream.RemoveReference(iref);
                    refLookup[iref] = firstPageCrossReferenceTable;

                    if (!iref.ContainingStreamID.IsEmpty)
                    {
                        //move object between object streams?
                        PdfObjectStream sourceObjectStream = _irefTable[iref.ContainingStreamID].Value as PdfObjectStream;
                        sourceObjectStream.RemoveObject(iref);
                    }

                    firstPageCrossReferenceTable.AddReference(iref);
                }
            }
        }

        private PdfReference[] GatherFirstPageAndDocumentReferences()
        {
            HashSet<PdfItem> exclusions = new HashSet<PdfItem>();

            for (int i = 1; i < PageCount; i++)
            {
                exclusions.Add(Pages[i].Reference);
            }

            if (AcroForm != null)
            {
                foreach (var field in AcroForm.Fields) //top level fields are not part of the page annotations array
                {
                    exclusions.Add(field.Reference);
                }

                PdfDictionary defaultFormResources = AcroForm.Elements.GetDictionary(PdfAcroForm.Keys.DR);
                if (defaultFormResources != null)
                {
                    PdfReference[] formResourceReferences = PdfTraversalUtility.TransitiveClosure(defaultFormResources);
                    foreach (var iref in formResourceReferences)
                    {
                        exclusions.Add(iref);
                    }
                }
            }
            exclusions.Add(Pages.Reference);

            var firstPageObjects = PdfTraversalUtility.TransitiveClosure(Pages[0], exclusions).ToList();

            var fieldsWithKids = firstPageObjects.Select(iref => iref.Value)
                                                 .OfType<PdfAcroField>()
                                                 .Where(af => af.HasKids)
                                                 .Select(af => af.Reference)
                                                 .ToList();

            firstPageObjects = firstPageObjects.Except(fieldsWithKids).ToList();

            firstPageObjects.Add(LinearizationParamaters.Reference);
            firstPageObjects.Add(LinearizationParamaters.HintStream.Reference);

            firstPageObjects.AddRange(Catalog.Elements.Select(e => e.Value)
                                                      .OfType<PdfReference>()
                                                      .Where(iref => iref.ObjectNumber > LinearizationParamaters.Reference.ObjectNumber));


            firstPageObjects.Add(Catalog.Reference);

            PdfDictionary names = Catalog.Elements.GetDictionary(PdfCatalog.Keys.Names);
            if (names != null)
            {
                PdfReference[] nameRefererences = PdfTraversalUtility.TransitiveClosure(names).Where(iref => iref.ObjectNumber < Pages[0].ObjectNumber).ToArray();
                firstPageObjects.AddRange(nameRefererences);
            }

            //find the document level javascripts that are not referenced anywhere
            var adbeJS = _irefTable.AllReferences.Where(iref => iref.ObjectNumber > LinearizationParamaters.Reference.ObjectNumber &&
                                                                iref.ObjectNumber < Pages[0].ObjectNumber)
                                                 .Select(iref => iref.Value).OfType<PdfDictionary>()
                                                .Where(pdfd => pdfd.Elements.Count == 2 &&
                                                                   pdfd.Elements.ContainsKey(PdfDictionary.PdfStream.Keys.Filter) &&
                                                                   pdfd.Elements.ContainsKey(PdfDictionary.PdfStream.Keys.Length))
                                                .Select(pdfd => pdfd.Reference)
                                                .ToList();

            firstPageObjects.AddRange(adbeJS);


            var firstPageCrossReferenceTable = _trailers.OfType<PdfCrossReferenceStream>()
                                        .OrderBy(t => t.StartXRef)
                                        .FirstOrDefault();

            firstPageObjects.Add(firstPageCrossReferenceTable.Reference);
            firstPageObjects.AddRange(firstPageCrossReferenceTable.ObjectStreams.Select(os => os.Reference));

            return firstPageObjects.Distinct().OrderBy(iref => iref.ObjectNumber).ToArray();
        }

        /// <summary>
        /// Dispatches PrepareForSave to the objects that need it.
        /// </summary>
        internal override void PrepareForSave()
        {
            PdfDocumentInformation info = Info;

            // Add patch level to producer if it is not '0'.
            string pdfSharpProducer = VersionInfo.Producer;
            if (!ProductVersionInfo.VersionPatch.Equals("0"))
                pdfSharpProducer = ProductVersionInfo.Producer2;

            // Set Creator if value is undefined.
            if (info.Elements[PdfDocumentInformation.Keys.Creator] == null)
                info.Creator = pdfSharpProducer;

            // Keep original producer if file was imported.
            if (_openMode == PdfDocumentOpenMode.Modify)
            {
                string producer = info.Producer;
                if (producer.Length == 0)
                    producer = pdfSharpProducer;
                else
                {
                    // Prevent endless concatenation if file is edited with PDFsharp more than once.
                    if (!producer.StartsWith(VersionInfo.Title))
                        producer = pdfSharpProducer;
                }
                info.Elements.SetString(PdfDocumentInformation.Keys.Producer, producer);
            }

            // Prepare used fonts.
            if (_fontTable != null)
                _fontTable.PrepareForSave();

            // Let catalog do the rest.
            Catalog.PrepareForSave();
        }

        /// <summary>
        /// Determines whether the document can be saved.
        /// </summary>
        public bool CanSave(ref string message)
        {
            if (!SecuritySettings.CanSave(ref message))
                return false;

            return true;
        }

        internal bool HasVersion(string version)
        {
            return String.Compare(Catalog.Version, version) >= 0;
        }

        /// <summary>
        /// Gets the document options used for saving the document.
        /// </summary>
        public PdfDocumentOptions Options
        {
            get
            {
                if (_options == null)
                    _options = new PdfDocumentOptions(this);
                return _options;
            }
        }
        PdfDocumentOptions _options;

        /// <summary>
        /// Gets PDF specific document settings.
        /// </summary>
        public PdfDocumentSettings Settings
        {
            get
            {
                if (_settings == null)
                    _settings = new PdfDocumentSettings(this);
                return _settings;
            }
        }
        PdfDocumentSettings _settings;

        /// <summary>
        /// NYI Indicates whether large objects are written immediately to the output stream to relieve
        /// memory consumption.
        /// </summary>
        internal bool EarlyWrite
        {
            get { return false; }
        }

        /// <summary>
        /// Gets or sets the PDF version number. Return value 14 e.g. means PDF 1.4 / Acrobat 5 etc.
        /// </summary>
        public int Version
        {
            get { return _version; }
            set
            {
                if (!CanModify)
                    throw new InvalidOperationException(PSSR.CannotModify);
                if (value < 12 || value > 17) // TODO not really implemented
                    throw new ArgumentException(PSSR.InvalidVersionNumber, "value");
                _version = value;
            }
        }
        internal int _version;

        /// <summary>
        /// Gets the number of pages in the document.
        /// </summary>
        public int PageCount
        {
            get
            {
                if (CanModify)
                    return Pages.Count;
                // PdfOpenMode is InformationOnly
                PdfDictionary pageTreeRoot = (PdfDictionary)Catalog.Elements.GetObject(PdfCatalog.Keys.Pages);
                return pageTreeRoot.Elements.GetInteger(PdfPages.Keys.Count);
            }
        }

        /// <summary>
        /// Gets the file size of the document.
        /// </summary>
        public long FileSize
        {
            get { return _fileSize; }
        }
        internal long _fileSize; // TODO: make private

        /// <summary>
        /// Gets the full qualified file name if the document was read form a file, or an empty string otherwise.
        /// </summary>
        public string FullPath
        {
            get { return _fullPath; }
        }
        internal string _fullPath = String.Empty; // TODO: make private

        /// <summary>
        /// Gets a Guid that uniquely identifies this instance of PdfDocument.
        /// </summary>
        public Guid Guid
        {
            get { return _guid; }
        }
        Guid _guid = Guid.NewGuid();

        internal DocumentHandle Handle
        {
            get
            {
                if (_handle == null)
                    _handle = new DocumentHandle(this);
                return _handle;
            }
        }
        DocumentHandle _handle;

        /// <summary>
        /// Returns a value indicating whether the document was newly created or opened from an existing document.
        /// Returns true if the document was opened with the PdfReader.Open function, false otherwise.
        /// </summary>
        public bool IsImported
        {
            get { return (_state & DocumentState.Imported) != 0; }
        }

        /// <summary>
        /// Returns a value indicating whether the document is read only or can be modified.
        /// </summary>
        public bool IsReadOnly
        {
            get { return (_openMode != PdfDocumentOpenMode.Modify); }
        }

        internal Exception DocumentNotImported()
        {
            return new InvalidOperationException("Document not imported.");
        }

        /// <summary>
        /// Gets information about the document.
        /// </summary>
        public PdfDocumentInformation Info
        {
            get
            {
                if (_info == null)
                    _info = _trailer.Info;
                return _info;
            }
        }
        PdfDocumentInformation _info;  // never changes if once created

        /// <summary>
        /// This function is intended to be undocumented.
        /// </summary>
        public PdfCustomValues CustomValues
        {
            get
            {
                if (_customValues == null)
                    _customValues = PdfCustomValues.Get(Catalog.Elements);
                return _customValues;
            }
            set
            {
                if (value != null)
                    throw new ArgumentException("Only null is allowed to clear all custom values.");
                PdfCustomValues.Remove(Catalog.Elements);
                _customValues = null;
            }
        }
        PdfCustomValues _customValues;

        /// <summary>
        /// Get the pages dictionary.
        /// </summary>
        public PdfPages Pages
        {
            get
            {
                if (_pages == null)
                {
                    UnderConstruction = true;
                    try
                    {
                        _pages = Catalog.Pages;
                    }
                    finally
                    {
                        UnderConstruction = false;
                    }
                }
                return _pages;
            }
        }
        PdfPages _pages;  // never changes if once created

        /// <summary>
        /// Gets or sets a value specifying the page layout to be used when the document is opened.
        /// </summary>
        public PdfPageLayout PageLayout
        {
            get { return Catalog.PageLayout; }
            set
            {
                if (!CanModify)
                    throw new InvalidOperationException(PSSR.CannotModify);
                Catalog.PageLayout = value;
            }
        }

        /// <summary>
        /// Gets or sets a value specifying how the document should be displayed when opened.
        /// </summary>
        public PdfPageMode PageMode
        {
            get { return Catalog.PageMode; }
            set
            {
                if (!CanModify)
                    throw new InvalidOperationException(PSSR.CannotModify);
                Catalog.PageMode = value;
            }
        }

        /// <summary>
        /// Gets the viewer preferences of this document.
        /// </summary>
        public PdfViewerPreferences ViewerPreferences
        {
            get { return Catalog.ViewerPreferences; }
        }

        /// <summary>
        /// Gets the root of the outline (or bookmark) tree.
        /// </summary>
        public PdfOutlineCollection Outlines
        {
            get { return Catalog.Outlines; }
        }

        internal bool UnderConstruction { get; set; }

        /// <summary>
        /// Get the AcroForm dictionary.
        /// </summary>
        public PdfAcroForm AcroForm
        {
            get { return Catalog.AcroForm; }
        }

        /// <summary>
        /// Gets or sets the default language of the document.
        /// </summary>
        public string Language
        {
            get { return Catalog.Language; }
            set { Catalog.Language = value; }
            //get { return Catalog.Elements.GetString(PdfCatalog.Keys.Lang); }
            //set { Catalog.Elements.SetString(PdfCatalog.Keys.Lang, value); }
        }

        /// <summary>
        /// Gets the security settings of this document.
        /// </summary>
        public PdfSecuritySettings SecuritySettings
        {
            get { return _securitySettings ?? (_securitySettings = new PdfSecuritySettings(this)); }
        }
        internal PdfSecuritySettings _securitySettings;

        /// <summary>
        /// Gets the document font table that holds all fonts used in the current document.
        /// </summary>
        internal PdfFontTable FontTable
        {
            get { return _fontTable ?? (_fontTable = new PdfFontTable(this)); }
        }
        PdfFontTable _fontTable;

        /// <summary>
        /// Gets the document image table that holds all images used in the current document.
        /// </summary>
        internal PdfImageTable ImageTable
        {
            get
            {
                if (_imageTable == null)
                    _imageTable = new PdfImageTable(this);
                return _imageTable;
            }
        }
        PdfImageTable _imageTable;

        /// <summary>
        /// Gets the document form table that holds all form external objects used in the current document.
        /// </summary>
        internal PdfFormXObjectTable FormTable  // TODO: Rename to ExternalDocumentTable.
        {
            get { return _formTable ?? (_formTable = new PdfFormXObjectTable(this)); }
        }
        PdfFormXObjectTable _formTable;

        /// <summary>
        /// Gets the document ExtGState table that holds all form state objects used in the current document.
        /// </summary>
        internal PdfExtGStateTable ExtGStateTable
        {
            get { return _extGStateTable ?? (_extGStateTable = new PdfExtGStateTable(this)); }
        }
        PdfExtGStateTable _extGStateTable;

        /// <summary>
        /// Gets the PdfCatalog of the current document.
        /// </summary>
        internal PdfCatalog Catalog
        {
            get { return _catalog ?? (_catalog = _trailer.Root); }
        }
        PdfCatalog _catalog;  // never changes if once created

        /// <summary>
        /// Gets the PdfInternals object of this document, that grants access to some internal structures
        /// which are not part of the public interface of PdfDocument.
        /// </summary>
        public new PdfInternals Internals
        {
            get { return _internals ?? (_internals = new PdfInternals(this)); }
        }
        PdfInternals _internals;

        /// <summary>
        /// Creates a new page and adds it to this document.
        /// Depending of the IsMetric property of the current region the page size is set to 
        /// A4 or Letter respectively. If this size is not appropriate it should be changed before
        /// any drawing operations are performed on the page.
        /// </summary>
        public PdfPage AddPage()
        {
            if (!CanModify)
                throw new InvalidOperationException(PSSR.CannotModify);
            return Catalog.Pages.Add();
        }

        /// <summary>
        /// Adds the specified page to this document. If the page is from an external document,
        /// it is imported to this document. In this case the returned page is not the same
        /// object as the specified one.
        /// </summary>
        public PdfPage AddPage(PdfPage page)
        {
            if (!CanModify)
                throw new InvalidOperationException(PSSR.CannotModify);
            return Catalog.Pages.Add(page);
        }

        /// <summary>
        /// Creates a new page and inserts it in this document at the specified position.
        /// </summary>
        public PdfPage InsertPage(int index)
        {
            if (!CanModify)
                throw new InvalidOperationException(PSSR.CannotModify);
            return Catalog.Pages.Insert(index);
        }

        /// <summary>
        /// Inserts the specified page in this document. If the page is from an external document,
        /// it is imported to this document. In this case the returned page is not the same
        /// object as the specified one.
        /// </summary>
        public PdfPage InsertPage(int index, PdfPage page)
        {
            if (!CanModify)
                throw new InvalidOperationException(PSSR.CannotModify);
            return Catalog.Pages.Insert(index, page);
        }

        /// <summary>
        /// Gets the security handler.
        /// </summary>
        public PdfStandardSecurityHandler SecurityHandler
        {
            get { return _trailer.SecurityHandler; }
        }

        internal List<PdfTrailer> _trailers = new List<PdfTrailer>();

        internal PdfTrailer _trailer; //always the last one
        internal PdfCrossReferenceTable _irefTable;
        internal Stream _outStream;

        // Imported Document
        internal Lexer _lexer;

        internal DateTime _creation;

        /// <summary>
        /// Occurs when the specified document is not used anymore for importing content.
        /// </summary>
        internal void OnExternalDocumentFinalized(PdfDocument.DocumentHandle handle)
        {
            if (tls != null)
            {
                //PdfDocument[] documents = tls.Documents;
                tls.DetachDocument(handle);
            }

            if (_formTable != null)
                _formTable.DetachDocument(handle);
        }

        //internal static GlobalObjectTable Gob = new GlobalObjectTable();

        /// <summary>
        /// Gets the ThreadLocalStorage object. It is used for caching objects that should created
        /// only once.
        /// </summary>
        internal static ThreadLocalStorage Tls
        {
            get { return tls ?? (tls = new ThreadLocalStorage()); }
        }
        [ThreadStatic]
        static ThreadLocalStorage tls;

        [DebuggerDisplay("(ID={ID}, alive={IsAlive})")]
        internal class DocumentHandle
        {
            public DocumentHandle(PdfDocument document)
            {
                _weakRef = new WeakReference(document);
                ID = document._guid.ToString("B").ToUpper();
            }

            public bool IsAlive
            {
                get { return _weakRef.IsAlive; }
            }

            public PdfDocument Target
            {
                get { return _weakRef.Target as PdfDocument; }
            }
            readonly WeakReference _weakRef;

            public string ID;

            public override bool Equals(object obj)
            {
                DocumentHandle handle = obj as DocumentHandle;
                if (!ReferenceEquals(handle, null))
                    return ID == handle.ID;
                return false;
            }

            public override int GetHashCode()
            {
                return ID.GetHashCode();
            }

            public static bool operator ==(DocumentHandle left, DocumentHandle right)
            {
                if (ReferenceEquals(left, null))
                    return ReferenceEquals(right, null);
                return left.Equals(right);
            }

            public static bool operator !=(DocumentHandle left, DocumentHandle right)
            {
                return !(left == right);
            }
        }


        public override int GetHashCode()
        {
            return Guid.GetHashCode();
        }
    }
}