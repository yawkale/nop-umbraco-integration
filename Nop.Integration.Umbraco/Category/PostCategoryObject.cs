using Newtonsoft.Json;

namespace Nop.Integration.Umbraco.Category
{
    [JsonObject(Title = "category")]
    public class PostCategoryObject
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
