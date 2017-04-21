using Commerce.Api.Adapter.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Commerce.Api.Adapter
{
    public class AccessProvider
    {
        public static string _token = "";
        public static string _clientId = WebConfigurationManager.AppSettings["ClientId"];
        public static string _clientSecret = WebConfigurationManager.AppSettings["ClientSecret"];
        public static string _serverUrl = WebConfigurationManager.AppSettings["ServerUrl"];
        public static string _redirectUrl = WebConfigurationManager.AppSettings["RedirectUrl"];

        public void GetAccessToken()
        {
            var nopAuthorizationManager = new AuthorizationManager();

            string authUrl = nopAuthorizationManager.BuildAuthUrl(_redirectUrl, new string[] { });

            WebRequest request = WebRequest.Create(authUrl);

            request.Credentials = CredentialCache.DefaultCredentials;

            WebResponse response = request.GetResponse();
        }
    }
}
