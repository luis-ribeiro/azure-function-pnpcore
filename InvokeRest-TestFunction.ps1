$form = @{
    Files     = [System.IO.FileInfo]::new('C:\Users\luisr\Downloads\bulldozer inspection.jpg')
    country = "PT"
    plant = "GIGA"
    machine = "Bulldozer"
}

$Uri = 'https://[Set your url].azurewebsites.net/api/FunctionStoreDocument?code=[Set your code]'

Invoke-RestMethod -Uri $Uri -Method Post -Form $form 