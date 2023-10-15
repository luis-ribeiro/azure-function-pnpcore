#[Note: current latest release - 2.2.0 from Jun 30 will throw an error. Use  nightly build prerelease until the fix is pushed in to major version.]
#Install-Module PnP.PowerShell -Scope CurrentUser -AllowPrerelease -SkipPublisherCheck -Force

#register new app
$tenant = "m365x80744553.onmicrosoft.com"
$appName = "AzureFunctionPnpCoreApp"
$CertificatePassword = ConvertTo-SecureString -String '1234.abcd' -AsPlainText
$targetSite = "https://m365x80744553.sharepoint.com/sites/document-center"


$newApp = Register-PnPAzureADApp `
    -ApplicationName $appName `
    -Tenant $tenant `
    -SharePointApplicationPermissions Sites.Selected `
    -Store CurrentUser `
    -CertificatePassword $certificatePassword `
    -Interactive -Verbose

$newApp 

#grant the permissions on the app to the target sp site

Connect-PnPOnline -Url $targetSite  -Interactive

Grant-PnPAzureADAppSitePermission -AppId $newApp."AzureAppId/ClientId"  `
    -DisplayName $appName `
    -Permissions Write `
    -Site $targetSite

Disconnect-PnPOnline

#test the app 

Connect-PnPOnline `
    -Url $targetSite  `
    -ClientId $newApp."AzureAppId/ClientId" `
    -Tenant $tenant `
    -Thumbprint $newApp."Certificate Thumbprint"

(Get-PnPWeb).Title

Disconnect-PnPOnline

