using System;
using System.Net;
using System.Web.Mvc;
using Newtonsoft.Json;
using Nop.Api.Adapter;
using Nop.Api.Adapter.Managers;
using Nop.Api.Adapter.Parameters;
using Nop.Api.Adapter.SettingsProvider;
using Umbraco.Web.Mvc;

namespace Nop.Integration.Umbraco.Core.Controllers
{
    public class AuthorizationController : SurfaceController
    {
        private readonly ISettingsProvider _settings;

        public AuthorizationController()
        {
            _settings = new WebConfigurationSettingsProvider();
        }
        [HttpGet]
        [AllowAnonymous]
        public void GetAccessToken(string code, string state)
        {

            if (ModelState.IsValid && !string.IsNullOrEmpty(code))
            {
                try
                {
                    var clientId = _settings.ClientId;
                    var clientSecret = _settings.ClientSecret;
                    var serverUrl = _settings.ServerUrl;
                    var redirectUrl = _settings.RedirectUrl;

                    var authParameters = new AuthParameters()
                    {
                        ClientId = clientId,
                        ClientSecret = clientSecret,
                        ServerUrl = serverUrl,
                        RedirectUrl = redirectUrl,
                        GrantType = "authorization_code",
                        Code = code
                    };

                    var nopAuthorizationManager = new AuthorizationManager(_settings);

                    var responseJson = nopAuthorizationManager.GetAuthorizationData(authParameters);

                    var authorizationModel = JsonConvert.DeserializeObject<Authorization.Authorization>(responseJson);

                    AccessProvider.Token = authorizationModel.AccessToken;
                }
                catch (Exception e)
                {
                    throw new Exception("Get access token error",e);
                }
            }
        }

        private ActionResult BadRequest(string message = "Bad Request")
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, message);
        }
    }
}