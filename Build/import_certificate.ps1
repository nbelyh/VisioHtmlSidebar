param ($pfx, $pw)

$pfxpass = $pw | ConvertTo-SecureString -AsPlainText -Force
Import-PfxCertificate -FilePath $pfx -CertStoreLocation Cert:\CurrentUser\My -Password $pfxpass 
