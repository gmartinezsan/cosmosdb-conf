using Newtonsoft.Json;

namespace Webstore.Functions.Models
{
    public class Address
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("zipcode")]
        public string zipcode { get; set; }
    }
}

