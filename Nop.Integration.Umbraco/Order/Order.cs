using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Integration.Umbraco.Models
{
    public class Order
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("order_total")]
        public decimal OrderTotal { get; set; }

        [JsonProperty("paid_date_utc")]
        public string PaidDateUtc { get; set; }

        [JsonProperty("payment_status")]
        public string PaymentStatus { get; set; }

        public Customer Customer { get; set; }
    }
}