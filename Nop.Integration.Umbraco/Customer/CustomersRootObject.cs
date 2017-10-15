using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nop.Integration.Umbraco.Order
{
    public class CustomersRootObject
    {
        [JsonProperty("customers")]
        public List<Customer.Customer> Customers { get; set; }
    }
}