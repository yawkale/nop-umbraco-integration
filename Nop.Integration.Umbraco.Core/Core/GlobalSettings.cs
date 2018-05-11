namespace Nop.Integration.Umbraco.Core.Core
{
    public class GlobalClientSettings
    {
        private readonly IConfigurationProvider _configurationProvider;

        public GlobalClientSettings(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public string CustomerIdCookieName => _configurationProvider.GetCongurationValue("CustomerIdCookieName", "NopCustomerId");
        public string CustomerTypeCookieName => _configurationProvider.GetCongurationValue("NopCustomerType", "NopCustomerType");
    }

    public class GlobalUmbracoSettings
    {
        private readonly IConfigurationProvider _configurationProvider;

        public GlobalUmbracoSettings(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }
        public string ProductDocumentTypeAlias => _configurationProvider.GetCongurationValue("ProductDocumentTypeAlias", "product");
        public string ProductIdPropertyAlias => _configurationProvider.GetCongurationValue("ProductIdPropertyAlias", "nopProductId");
        public string CategoryDocumentTypeAlias => _configurationProvider.GetCongurationValue("CategoryDocumentTypeAlias", "category");
        public string CategoryIdPropertyAlias => _configurationProvider.GetCongurationValue("CategoryIdPropertyAlias", "nopCategoryId");
        public string MemberIdPropertyAlias => _configurationProvider.GetCongurationValue("MemberIdPropertyAlias", "nopCustomerId");

        public int NopStoreId => _configurationProvider.GetCongurationValue("NopStoreId", 1);
        public bool CreateProductLimitToStore => _configurationProvider.GetCongurationValue("CreateProductLimitToStore", false);
        public bool GetProductLimitToStore => _configurationProvider.GetCongurationValue("GetProductLimitToStore", false);
    }

    public class PayPalSettings
    {
        private readonly IConfigurationProvider _configurationProvider;

        public PayPalSettings(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }
        public string PayPalRedirectUrl => "https://www.sandbox.paypal.com/webscr&cmd=";
        public string PayPalCancelUrl => "http://localhost:64146/umbraco/surface/PayPal/HandleCancelExpressCheckout";
        public string PayPalReturnUrl => "http://localhost:64146/umbraco/surface/PayPal/GetExpressCheckout";
        //public string MemberIdPropertyAlias => "nopCustomerId";
    }

    public static class GlobalSettings
    {
        public static GlobalClientSettings ClientSettings { get; set; }
        public static GlobalUmbracoSettings UmbracoSettings { get; set; }
        public static PayPalSettings PayPalSettings { get; set; }

        static GlobalSettings()
        {
            IConfigurationProvider configurationProvider = new WebConfigurationProvider();
            ClientSettings = new GlobalClientSettings(configurationProvider);
            UmbracoSettings = new GlobalUmbracoSettings(configurationProvider);
            PayPalSettings = new PayPalSettings(configurationProvider);
        }

    }
}
