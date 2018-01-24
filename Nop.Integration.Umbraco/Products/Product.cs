using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nop.Integration.Umbraco.Products
{
    public class Product
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("images")]
        public List<Image.Image> Images { get; set; }

        [JsonProperty("attributes")]
        public List<ProductAttributeMapping> Attributes { get; set; }

        [JsonProperty("order_minimum_quantity")]
        public int MinQuantity { get; set; }

        [JsonProperty("is_gift_card")]
        public bool IsGiftCard { get; set; }

        [JsonProperty("is_download")]
        public bool IsDownload { get; set; }

        [JsonProperty("customer_enters_price")]
        public bool CustomerEnterPrice { get; set; }

        [JsonProperty("is_rental")]
        public bool IsRental { get; set; }

        [JsonProperty("has_tier_prices")]
        public bool HasTierPrices { get; set; }
        
        [JsonProperty("sku", NullValueHandling = NullValueHandling.Ignore)]
        public string SKU { get; set; }

        public bool Redirect
        {
            get
            {
                return (Attributes != null &&  Attributes.Any() || MinQuantity > 1 || IsGiftCard || IsDownload || CustomerEnterPrice || IsRental || HasTierPrices) ? true : false;
            }
        }
    }
}