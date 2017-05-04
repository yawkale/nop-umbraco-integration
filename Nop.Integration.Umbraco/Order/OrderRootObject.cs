using Newtonsoft.Json;
using Nop.Integration.Umbraco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Integration.Umbraco.Models
{
    public class OrdersRootObject
    {
        [JsonProperty("orders")]
        public List<Order> Orders { get; set; }
    }
}