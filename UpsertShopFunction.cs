using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Webstore.Functions.Models;

namespace Webstore.Functions
{
    public static class UpsertShopFunction
    {
        [FunctionName("UpsertShopFunction")]
        public static async Task Run([CosmosDBTrigger(
            databaseName: "WebStore",
            collectionName: "customers",
            ConnectionStringSetting = "CosmosDBConnection",
            LeaseCollectionName = "leases",
            CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> input,
            [CosmosDB(
            databaseName: "WebStore",
            collectionName: "shops",
            ConnectionStringSetting = "CosmosDBConnection")]IAsyncCollector<Shop>shopItemsOut,
            ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                foreach (var item in input)
                {
                    Customer customer = JsonConvert.DeserializeObject<Customer>(item.ToString());

                    // Any changes to the input documents go here 
                    Shop shop = new Shop()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Address = customer.Address,
                        Total = customer.Total
                    };
                    await shopItemsOut.AddAsync(shop);
                }

                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id " + input[0].Id);
            }
        }
    }
}
