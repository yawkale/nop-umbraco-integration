using Newtonsoft.Json;
using Nop.Integration.Umbraco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Integration.Umbraco.Models
{
    public class ProductRootObject
    {
        [JsonProperty("products")]
        public List<Product> Products { get; set; }
    }
}