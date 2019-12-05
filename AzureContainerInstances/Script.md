# Pre-requisites

1. Have an Azure Subscription
1. Install the Azure CLI
    1. Help over here: https://docs.microsoft.com/en-us/cli/azure/acr?view=azure-cli-latest
1. Install Visual Studio 2019
1. Install .Net Core 3.0
1. Install Docker Desktop

# Instructions

1. Create a new Asp.Net Core project with Docker support with Visual Studio
    1. If you're on an Epicor PC your only alternative for this demo is Linux!
1. Build the container:
    ```
        docker build -t epicor.com/containerinstancedemo-dev:latest .
    ```
1. Run the container locally to test it:
    ```
        docker run -p 4557:80 -it epicor.com/containerinstancedemo-dev:latest
    ```
1. Create Azure Container Registry
    ```
        az login
        az account set --subscription <Susbcription Name>
        az group create --name <Resource Group Name> --location <Azure Datacenter location>
        az acr create --name <Container Registry Name> --resource-group <Resource Group Name> --sku <SKU, Classic,Basic,Standard,Premium>
    ```
1. Deploy image to Container registry
    ```
        az acr show --name <container registry name> --query loginServer --output table
            The above returns the container registry loginServer that ends with azurecr.io
        az acr login --name <container registry name>
        az acr repository list --name <Container Registry Name>
        Build image (optional): docker build -t epicor.com/containerinstancedemo-dev:latest .
        docker tag <Image ID> <container registry login server>/containerinstancedemo-dev:latest
        docker images
        docker push <container registry login server>/containerinstancedemo-dev:latest
        az acr repository list --name <Container Registry Name>
    ```
1. Create container instance from container image in the registry
    ```
        az acr credential show --name <Container Registry Name>
        az container create --resource-group <Resource Group Name> --name <Container Instance Name> --image <container registry login server>/containerinstancedemo-dev:latest --os-type Linux --cpu 1 --memory 1 --ports 80 443 --dns-name-label <DNS Prefix Label> --registry-username <Container Registry Name> --registry-password <Password from az acr credential>
        #az acr update --name <Container Registry Name> --admin-enabled true do the following if you didn't used the --admin-enabled true argument for this demo
        #az acr credential show --name <Container Registry Name>
    ```
1. Run quick test
    ```
        Navigate to your container FQDN <DNS Prefix Label>.westus.azurecontainer.io
    ```
1. Kill your container as soon as you're done with it