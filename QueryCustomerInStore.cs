using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Webstore.Functions.Models;

namespace Webstore.Functions
{
    public static class QueryCustomerInStore
    {
        [FunctionName("QueryCustomerInStore")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "{collectionName}/{partitionKey}/{id}")] HttpRequest req,
            [CosmosDB(
                databaseName: "WebStore",
                collectionName: "{collectionName}",
                ConnectionStringSetting = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{partitionKey}")] Customer customerItem,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (customerItem == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(customerItem);
        }
    }
}
