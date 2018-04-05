using Newtonsoft.Json;

namespace Nop.Integration.Umbraco.Customer
{
    public class DataSearchModel
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }
}
