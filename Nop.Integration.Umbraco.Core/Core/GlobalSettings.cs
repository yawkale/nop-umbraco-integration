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
        
    }

    public static class GlobalSettings
    {
        public static GlobalClientSettings ClientSettings { get; set; }
        public static GlobalUmbracoSettings UmbracoSettings { get; set; }

        static GlobalSettings()
        {
            ClientSettings=new GlobalClientSettings();
            UmbracoSettings=new GlobalUmbracoSettings();
        }

    }
}
