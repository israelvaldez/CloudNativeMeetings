using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Configuration;
using System;

namespace AzureSearchExample
{
    partial class Program
    {
        static void Main(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();

            SearchServiceClient serviceClient = CreateSearchServiceClient(configuration);

            string indexName = configuration["SearchIndexName"];

            RecreateIndex(indexName, serviceClient);

            // TODO: Populate index and do some queries
            ISearchIndexClient indexClient = serviceClient.Indexes.GetClient(indexName);
            Console.WriteLine("Uploading documents...");
            UploadDocuments(indexClient);

            // Some search queries to do on the portal: 
            // 1) * (returns all up to 50 matches)
            // 2) Hotel in new york or san antonio
            // 3) Hotel with pool&$count=true
            // 4) pool&$select=HotelName,Tags&$count=true
            // 5) *&$top=2&$count=true then *&$top=2&$skip=2&$count=true
            // 6) *&$filter=Rating gt 4.5
            // 7) *&$orderby=Rating desc

            SearchParameters parameters;
            DocumentSearchResult<Hotel> results;

            // Query 1
            parameters = new SearchParameters();
            results = indexClient.Documents.Search<Hotel>("*", parameters);

            // Query 2
            parameters = new SearchParameters() {
                Filter = "Rating gt 4.5"
            };
            results = indexClient.Documents.Search<Hotel>("*", parameters);

            Console.WriteLine("Complete.  Press any key to end application...");
        }

        private static SearchServiceClient CreateSearchServiceClient(IConfigurationRoot configuration)
        {
            string searchServiceName = configuration["SearchServiceName"];
            string adminApiKey = configuration["SearchServiceAdminApiKey"];

            return new SearchServiceClient(searchServiceName, new SearchCredentials(adminApiKey));
        }

        private static void RecreateIndex(string indexName, SearchServiceClient client)
        {
            DeleteIndexIfExists(indexName, client);
            CreateIndex(indexName, client);
        }

        private static void DeleteIndexIfExists(string indexName, SearchServiceClient client)
        {
            Console.WriteLine("Deleting index...");
            if (client.Indexes.Exists(indexName))
                client.Indexes.Delete(indexName);
        }

        private static void CreateIndex(string indexName, SearchServiceClient client)
        {
            Console.WriteLine("Creating index...");
            var definition = new Index()
            {
                Name = indexName,
                Fields = FieldBuilder.BuildForType<Hotel>()
            };

            client.Indexes.Create(definition);
        }
    }
}
