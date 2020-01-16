$templateFile = "C:\Projects\CNA\CNA-4317\templates\resource-group.json";
$parametersTemplateFile = "C:\Projects\CNA\CNA-4317\templates\resource-group.parameters.json";
$location = "centralus"
$subscriptionId = "fb9cd3d4-ab06-4da7-a325-c3a0032539f8"; # <-- Visual Studio Professional

Set-AzContext -Subscription $subscriptionId

Test-AzDeployment -Location $location -TemplateFile $templateFile -TemplateParameterFile $parametersTemplateFile;

New-AzDeployment `
  -Name demoDeployment `
  -Location $location `
  -TemplateFile $templateFile -TemplateParameterFile $parametersTemplateFile;