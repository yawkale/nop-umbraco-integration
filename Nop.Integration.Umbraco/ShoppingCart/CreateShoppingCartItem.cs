using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nop.Integration.Umbraco.ShoppingCart
{
    public class CreateShoppingCartItem
    {   
        public CreateShoppingCartItem()
        {
            Attributes = new List<ShoppingCartProductAttribute>();
        }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("product_id")]
        public int ProductId { get; set; }

        [JsonProperty("shopping_cart_type")]
        public string CartType { get; set; }

        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty("product_attributes")]
        public List<ShoppingCartProductAttribute> Attributes { get; set; }
    }
}