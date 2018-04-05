using System.Web.Configuration;

namespace Nop.Api.Adapter.SettingsProvider
{
    public class WebConfigurationSettingsProvider: ISettingsProvider
    {
        public string ClientId => WebConfigurationManager.AppSettings["ClientId"];
        public string ClientSecret => WebConfigurationManager.AppSettings["ClientSecret"];
        public string ServerUrl => WebConfigurationManager.AppSettings["ServerUrl"];
        public string RedirectUrl => WebConfigurationManager.AppSettings["RedirectUrl"];
    }
}
