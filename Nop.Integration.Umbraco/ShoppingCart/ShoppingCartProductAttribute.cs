using Newtonsoft.Json;

namespace Nop.Integration.Umbraco.ShoppingCart
{
    public class ShoppingCartProductAttribute
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
