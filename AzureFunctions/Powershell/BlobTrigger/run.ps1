# Input bindings are passed in via param block.
param([byte[]] $InputBlob, $TriggerMetadata)

Push-OutputBinding -Name OutputBlob  `
-Value ([HttpResponseContext]@{
    Body="PowerShell Blob trigger function Processed blob! Name: $($TriggerMetadata.Name) Size: $($InputBlob.Length) bytes"
    ContentType='application/octet-stream'
})