$location = "centralus"
$resouceGroupName = "resource-group-from-git"
$subscriptionId = "fb9cd3d4-ab06-4da7-a325-c3a0032539f8"; # <-- Visual Studio Professional

Set-AzContext -Subscription $subscriptionId

Test-AzDeployment -Location $location `
    -TemplateUri https://raw.githubusercontent.com/Azure/azure-docs-json-samples/master/azure-resource-manager/emptyRG.json `
    -rgName $resouceGroupName `
    -rgLocation $location

New-AzDeployment `
    -Name 'rg-deplyment' `
    -Location $location `
    -TemplateUri https://raw.githubusercontent.com/Azure/azure-docs-json-samples/master/azure-resource-manager/emptyRG.json `
    -rgName $resouceGroupName `
    -rgLocation $location;  