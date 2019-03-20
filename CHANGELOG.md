# 1.7.1
#### 03/20/2019
- Documents with no form do not cause exceptions in font handling
- Optional objects that are written directly to the document are referenced directly
- Better handling of compressed document revisions
- Font references are moved to the page when flattening
- Text field appearance streams are repaired upon save, prevents rendering bugs from generating blank elements when the document is viewed
- Flattening now hanldes Text, Checkbox and button fields