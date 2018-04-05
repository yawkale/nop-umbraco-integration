using Newtonsoft.Json;

namespace Nop.Integration.Umbraco.Category
{
    public class Category
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
