using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Integration.Umbraco.Models
{
    public class ShoppingCartRootObject
    {
        [JsonProperty("shopping_carts")]
        public List<ShoppingCartItem> products { get; set; }
    }
}