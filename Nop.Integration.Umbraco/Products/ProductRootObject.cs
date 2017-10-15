using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nop.Integration.Umbraco.Products
{
    public class ProductRootObject
    {
        [JsonProperty("products")]
        public List<Product> Products { get; set; }
    }
}