using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nop.Integration.Umbraco.ShoppingCart
{
    public class ShoppingCartRootObject
    {
        [JsonProperty("shopping_carts")]
        public List<ShoppingCartItem> Products { get; set; }
    }
}