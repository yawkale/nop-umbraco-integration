using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nop.Integration.Umbraco.Product
{
    [JsonObject(Title = "attribute")]
    public class ProductAttributeMapping
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("product_attribute_id")]
        public int ProductAttributeId { get; set; }

        [JsonProperty("product_attribute_name")]
        public string ProductAttributeName { get; set; }

        [JsonProperty("text_prompt")]
        public string TextPrompt { get; set; }

        [JsonProperty("is_required")]
        public bool IsRequired { get; set; }

        [JsonProperty("attribute_control_type_id")]
        public int AttributeControlTypeId { get; set; }

        [JsonProperty("display_order")]
        public int DisplayOrder { get; set; }

        [JsonProperty("default_value")]
        public string DefaultValue { get; set; }

        [JsonProperty("attribute_control_type_name")]
        public string AttributeControlType { get; set; }

        [JsonProperty("attribute_values")]
        public List<ProductAttributeValue> ProductAttributeValues { get; set; }
    }
}