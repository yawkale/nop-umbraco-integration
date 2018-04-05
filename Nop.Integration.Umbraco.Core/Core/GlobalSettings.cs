namespace Nop.Integration.Umbraco.Core.Core
{
    public class GlobalClientSettings
    {
        public string CustomerIdCookieName => "NopCustomerId";
    }

    public class GlobalUmbracoSettings
    {
        public string CustomerIdFieldName => "NopCustomerId";
        public string ProductDocumentTypeAlias => "product";
        public string ProductIdPropertyAlias => "nopProductId";
        public string CategoryDocumentTypeAlias => "category";
        public string CategoryIdPropertyAlias => "nopCategoryId";
        public string MemberIdPropertyAlias => "nopCustomerId";
        public string NopStoreId => "1";
        public bool CreateProductLImitToStore => false;
        public bool GetProductLimitToStore => false;

    }

    public class PayPalSettings
    {
        public string PayPalRedirectUrl => "https://www.sandbox.paypal.com/webscr&cmd=";
        public string PayPalCancelUrl => "http://localhost:64146/umbraco/surface/PayPal/HandleCancelExpressCheckout";
        public string PayPalReturnUrl => "http://localhost:64146/umbraco/surface/PayPal/GetExpressCheckout";
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
