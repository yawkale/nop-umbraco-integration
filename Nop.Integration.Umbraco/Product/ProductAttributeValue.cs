using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Integration.Umbraco.Models
{
    [JsonObject(Title = "attribute_value")]
    public class ProductAttributeValue
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("type_id")]
        public int? AttributeValueTypeId { get; set; }

        [JsonProperty("associated_product_id")]
        public int? AssociatedProductId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("color_squares_rgb")]
        public string ColorSquaresRgb { get; set; }

        [JsonProperty("image_squares_image")]
        public Image ImageSquaresImage { get; set; }

        [JsonProperty("price_adjustment")]
        public decimal? PriceAdjustment { get; set; }

       
        [JsonProperty("weight_adjustment")]
        public decimal? WeightAdjustment { get; set; }

        [JsonProperty("cost")]
        public decimal? Cost { get; set; }

        [JsonProperty("quantity")]
        public int? Quantity { get; set; }

        [JsonProperty("is_pre_selected")]
        public bool? IsPreSelected { get; set; }

        [JsonProperty("display_order")]
        public int? DisplayOrder { get; set; }

        [JsonIgnore]
        public int? PictureId { get; set; }

        [JsonProperty("product_image_id")]
        public int? ProductPictureId { get; set; }

        [JsonProperty("type")]
        public string AttributeValueType { get; set; }
    }
}