using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Integration.Umbraco.Models
{
    public class CreateShoppingCartItem
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
        public string CustomerId { get; set; }
    }
}