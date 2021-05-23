# CidMapper
## Description of the problem
### Background 
PDF often contains text encoded in various ways, using different font formats that can reduce the size of the PDF file and improve font rendering. One of these approaches is Embedding CID Fonts. The characters actually used in the document are embedded in the PDF file as "virtual" fonts, using a special internal format and CID (Character ID) encoding of the characters.
Since Adobe is the standard-setter for PDF, it also provides predefined character maps for specific language scripts (so called CJK-fonts – Chinese, Japanese, Korean).
### Tasks
1. Initialize this program with data from Adobe's CMap file.
2. Map byte-encoded sequence of CIDs using CID to UTF8 mapping and return decoded string.
3. Encode string as a sequence of bytes representing CIDs.
4. Ensure unit tests are passed.
### Ordering entity
The task was carried out as part of the recruitment for the position of developer at Trotec Laser.
## Solution author
### Name
Sebastian Bobrowski 
### Contact information
sebastian.bobrowski@outlook.com, s.bobrowski@aol.com
## Instructions for use
It is enough to run the project with tests.
