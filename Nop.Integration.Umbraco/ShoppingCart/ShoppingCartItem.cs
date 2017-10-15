using Newtonsoft.Json;

namespace Nop.Integration.Umbraco.ShoppingCart
{
    public class ShoppingCartItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("product_id")]
        public int ProductId { get; set; }

        [JsonProperty("shopping_cart_type")]
        public string CartType { get; set; }

        [JsonProperty("customer_id")]
        public int CustomerId { get; set; }

        [JsonProperty("product")]
        public Products.Product Product { get; set; }
    }
}