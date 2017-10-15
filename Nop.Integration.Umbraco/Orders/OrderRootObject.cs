using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nop.Integration.Umbraco.Orders
{
    public class OrdersRootObject
    {
        [JsonProperty("orders")]
        public List<Order> Orders { get; set; }
    }
}