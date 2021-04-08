using Newtonsoft.Json;

namespace Webstore.Functions.Models
{
    public class Shop
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("total")]
        public decimal Total { get; set; }

    }
}



