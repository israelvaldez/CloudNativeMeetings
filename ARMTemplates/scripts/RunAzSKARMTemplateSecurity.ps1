#Install-Module -Name AzSK -Force

Get-AzSKARMTemplateSecurityStatus -ARMTemplatePath "C:\Git\feliperepo\ARMTemplateDemo\ARMTemplateDemo\templates\storage-account.json" #-SkipControlsFromFile "C:\Git\feliperepo\ARMTemplateDemo\ARMTemplateDemo\exclusions\exclusionfile.csv"



