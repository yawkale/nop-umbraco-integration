using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Integration.Umbraco.Models
{
    public class Product
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("images")]
        public List<Image> Images { get; set; }
        
        [JsonProperty("attributes")]
        public List<ProductAttributeMapping> Attributes { get; set; }
    }
}