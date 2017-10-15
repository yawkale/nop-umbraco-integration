using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
