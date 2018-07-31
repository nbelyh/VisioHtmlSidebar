"C:\Program Files (x86)\Windows Kits\10\App Certification Kit\signtool.exe" ^
sign /n "Nikolay Belykh" /v ^
/fd sha256 ^
/tr http://tsa.startssl.com/rfc3161 /td sha256 ^
/d "Visio HTML/FormData Edit Addin" ^
/du "http://unmanagedvisio.com" ^
"Setup\bin\Debug\*.msi"
