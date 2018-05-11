using System.Linq;
using System.Web.Configuration;

namespace Nop.Integration.Umbraco.Core.Core
{
    public class GlobalClientSettings
    {
        public string CustomerIdCookieName => GetSetting("CustomerIdCookieName", "NopCustomerId");

        public string CustomerTypeCookieName => GetSetting("NopCustomerType", "NopCustomerType");

        private string GetSetting(string key, string defaultvalue)
        {
            if (key == null)
                return defaultvalue;

            if (!WebConfigurationManager.AppSettings.AllKeys.Contains(key))
                return defaultvalue;

            return WebConfigurationManager.AppSettings[key];
        }
    }

    public class GlobalUmbracoSettings
    {
        public string ProductDocumentTypeAlias => GetSetting("ProductDocumentTypeAlias", "product");
        public string ProductIdPropertyAlias => GetSetting("ProductIdPropertyAlias", "nopProductId");
        public string CategoryDocumentTypeAlias => GetSetting("CategoryDocumentTypeAlias", "category");
        public string CategoryIdPropertyAlias => GetSetting("CategoryIdPropertyAlias", "nopCategoryId");
        public string MemberIdPropertyAlias => GetSetting("MemberIdPropertyAlias", "nopCustomerId");
        private string GetSetting(string key, string defaultvalue)
        {
            if (key == null)
                return defaultvalue;

            if (!WebConfigurationManager.AppSettings.AllKeys.Contains(key))
                return defaultvalue;

            return WebConfigurationManager.AppSettings[key];
        }
    }

    public static class GlobalSettings
    {
        public static GlobalClientSettings ClientSettings { get; set; }
        public static GlobalUmbracoSettings UmbracoSettings { get; set; }
        public static PayPalSettings PayPalSettings { get; set; }

        static GlobalSettings()
        {
            ClientSettings = new GlobalClientSettings();
            UmbracoSettings = new GlobalUmbracoSettings();
            PayPalSettings = new PayPalSettings();
        }

    }
}
