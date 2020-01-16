$templateFile = "C:\Projects\CNA\CNA-4317\templates\app-service.json";
$parametersTemplateFile = "C:\Projects\CNA\CNA-4317\templates\app-service.parameters.json";
$resourceGroupName = "rg-demo";
$location = "centralus"
$subscriptionId = "fb9cd3d4-ab06-4da7-a325-c3a0032539f8"; # <-- Visual Studio Professional

Set-AzContext -Subscription $subscriptionId

$resourceGroup = Get-AzResourceGroup -Name $resourceGroupName -Location $location -ErrorAction SilentlyContinue

if (!$resourceGroup)
{
    New-AzResourceGroup -Name $resourceGroupName -Location $location
    "Resource Group $resourceGroupName was created";
}
else
{
    "Resource Group $resourceGroupName already exists";
}

Test-AzResourceGroupDeployment -ResourceGroupName $resourceGroupName -TemplateFile $templateFile -TemplateParameterFile $parametersTemplateFile;

$results = New-AzResourceGroupDeployment -ResourceGroupName $resourceGroupName -TemplateFile $templateFile -TemplateParameterFile $parametersTemplateFile;

Write-Host "Outputs:"
foreach($key in $results.Outputs.Keys)
{
    Write-Host "Key: $key | Value: $($results.Outputs[$key].Value)"
}