using Nop.Api.Adapter.Managers;
using System.Net;
using Nop.Api.Adapter.SettingsProvider;

namespace Nop.Api.Adapter
{
    public class AccessProvider
    {
        private readonly ISettingsProvider _settings;

        public AccessProvider(ISettingsProvider settings)
        {
            _settings = settings;
        }
        public static string Token = "";

        //public static string ClientId = WebConfigurationManager.AppSettings["ClientId"];
        //public static string ClientSecret = WebConfigurationManager.AppSettings["ClientSecret"];
        //public static string ServerUrl = WebConfigurationManager.AppSettings["ServerUrl"];
        //public static string RedirectUrl = WebConfigurationManager.AppSettings["RedirectUrl"];

        //public void GetAccessToken()
        //{
        //    var nopAuthorizationManager = new AuthorizationManager(_settings);

        //    var authUrl = nopAuthorizationManager.BuildAuthUrl(_settings.RedirectUrl, new string[] { });

        //    var request = WebRequest.Create(authUrl);

        //    request.Credentials = CredentialCache.DefaultCredentials;

        //    var response = request.GetResponse();
        //}
    }
}
