$timestampUrl = "http://timestamp.digicert.com"
Write-Host "TIMESTAMP " $timestampUrl

$signtool = "C:\Program Files (x86)\Windows Kits\8.1\bin\x86\signtool.exe"
Write-Host "SIGNTOOL " $signtool
 
ForEach ($file in (Get-ChildItem "Setup\bin\Release\*.msi"))
{
	Write-Host $file.FullName
	&$signtool sign /n "Nikolay Belykh" /v /fd sha256 /tr $timestampUrl /td sha256 /d "Visio VisioHtmlSidebar Addin" /du "https://unmanagedvisio.com" $file.FullName
}
