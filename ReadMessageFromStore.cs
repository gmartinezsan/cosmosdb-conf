using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Webstore.Functions.Models;

namespace Webstore.Functions
{
    public static class ReadMessageFromStore
    {
        [FunctionName("ReadMessageFromStore")]
        public static void Run([QueueTrigger("webstore-queue", Connection = "QueueConnection")] Customer customerMessage,
        [CosmosDB(
        databaseName: "WebStore",
        collectionName: "customers",
        ConnectionStringSetting = "CosmosDBConnection")]out Customer customerDocument,
        ILogger log)
        {
            // The code for the data transformation should be added here.
            customerMessage.CustomerName = customerMessage.CustomerName.ToUpperInvariant();
            // add the id for the Address document
            customerMessage.Address.Id = Guid.NewGuid().ToString();
            customerDocument = customerMessage;

            log.LogInformation($"C# Queue trigger function inserted one document.");
        }
    }
}
