using System;
using System.Net;
using System.Web.Mvc;
using Newtonsoft.Json;
using Nop.Api.Adapter;
using Nop.Api.Adapter.Managers;
using Umbraco.Web.Mvc;

namespace Nop.Integration.Umbraco.Core.Controllers
{
    public class AuthorizationController : SurfaceController
    {
        [HttpGet]
        [AllowAnonymous]
        public void GetAccessToken(string code, string state)
        {

            if (ModelState.IsValid && !string.IsNullOrEmpty(code))
            {
                try
                {
                    string clientId = AccessProvider.ClientId;
                    string clientSecret = AccessProvider.ClientSecret;
                    string serverUrl = AccessProvider.ServerUrl;
                    string redirectUrl = AccessProvider.RedirectUrl;

                    var authParameters = new AuthParameters()
                    {
                        ClientId = clientId,
                        ClientSecret = clientSecret,
                        ServerUrl = serverUrl,
                        RedirectUrl = redirectUrl,
                        GrantType = "authorization_code",
                        Code = code
                    };

                    var nopAuthorizationManager = new AuthorizationManager();

                    string responseJson = nopAuthorizationManager.GetAuthorizationData(authParameters);

                    var authorizationModel = JsonConvert.DeserializeObject<Authorization.Authorization>(responseJson);

                    AccessProvider.Token = authorizationModel.AccessToken;
                }
                catch (Exception e)
                {
                    throw new Exception("error");
                }
            }
        }

        private ActionResult BadRequest(string message = "Bad Request")
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, message);
        }
    }
}