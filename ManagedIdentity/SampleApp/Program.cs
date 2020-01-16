using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;

namespace SampleApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
               .ConfigureAppConfiguration((context, config) =>
                {
                    var builtConfig = config.Build();

                    if (builtConfig["UseVault"] == "true")
                    {
                        var vaultUrl = $"https://{builtConfig["KeyVaultName"]}.vault.azure.net/";

                        if (builtConfig["VaultSecurity"] == "AzureAD")
                        {
                            config.AddAzureKeyVault(vaultUrl, builtConfig["ClientId"], builtConfig["Secret"], new DefaultKeyVaultSecretManager());
                        }
                        else if (builtConfig["VaultSecurity"] == "SystemAssignedIdentity")
                        {
                            var azureServiceTokenProvider = new AzureServiceTokenProvider();

                            var keyVaultClient = new KeyVaultClient(
                                new KeyVaultClient.AuthenticationCallback(
                                    azureServiceTokenProvider.KeyVaultTokenCallback));

                            config.AddAzureKeyVault(vaultUrl,
                                keyVaultClient,
                                new DefaultKeyVaultSecretManager());
                        }
                        else if (builtConfig["VaultSecurity"] == "UserManagementIdentity")
                        {
                            var tokenProvider = new AzureServiceTokenProvider($"RunAs=App;AppId={builtConfig["AppId"]}");

                            var client = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(tokenProvider.KeyVaultTokenCallback));

                            config.AddAzureKeyVault(vaultUrl, client, new DefaultKeyVaultSecretManager());
                        }
                    }
                })
                .UseStartup<Startup>();
    }
}
