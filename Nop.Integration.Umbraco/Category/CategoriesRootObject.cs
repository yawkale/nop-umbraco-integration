using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nop.Integration.Umbraco.Order
{
    public class CategoriesRootObject
    {
        [JsonProperty("categories")]
        public List<Category.Category> Categories { get; set; }
    }
}