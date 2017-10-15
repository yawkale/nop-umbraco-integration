using Newtonsoft.Json;

namespace Nop.Integration.Umbraco.Products
{
    [JsonObject(Title = "product")]
    public class PostProductObject
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
