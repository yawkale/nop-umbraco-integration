using Newtonsoft.Json;

namespace Nop.Integration.Umbraco.Order
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

        public Customer.Customer Customer { get; set; }
    }
}