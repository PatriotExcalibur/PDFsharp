# PDFsharper
A clone of the pdfsharp .NET library for processing PDF integrating community fixes 
and adding some of the features we need.

Open to community PRs!


The current target for 1.6 is the end of june with the following features
* Incremental update support
  (digitally signed documents stay valid)
  This also allows making further changes to the document without invalidating the digital signature - a must for multi signature documents.
* Adding text fields
* Full support for the 14 system fonts that do not require embedding
* All text field alignment options, (left, center, right) in combo with multiline.
* Form flattening


Features we need and plan to support in the next 12 months. (1.7)
* Unembed fonts
* Compressed object stream writing
* Linearization
