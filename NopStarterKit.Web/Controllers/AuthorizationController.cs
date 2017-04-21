using System;
using System.Net;
using System.Web.Mvc;
using Newtonsoft.Json;
using Commerce.Api.Adapter;
using Commerce.Api.Adapter.Managers;
using Umbraco.Web.Mvc;

namespace NopStarterKit.Web.Controllers
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
                    string clientId = AccessProvider._clientId;
                    string clientSecret = AccessProvider._clientSecret;
                    string serverUrl = AccessProvider._serverUrl;
                    string redirectUrl = AccessProvider._redirectUrl;

                    var authParameters = new AuthParameters()
                    {
                        ClientId = clientId,
                        ClientSecret = clientSecret,
                        ServerUrl = serverUrl,
                        RedirectUrl = redirectUrl,
                        GrantType = "authorization_code",
                        Code = code
                    };

                    var nopAuthorizationManager = new Commerce.Api.Adapter.Managers.AuthorizationManager();

                    string responseJson = nopAuthorizationManager.GetAuthorizationData(authParameters);

                    Nop.Integration.Umbraco.Models.Authorization authorizationModel = JsonConvert.DeserializeObject<Nop.Integration.Umbraco.Models.Authorization>(responseJson);

                    AccessProvider._token = authorizationModel.AccessToken;
                    //Session["accessToken"] = authorizationModel.AccessToken;
                }
                catch(Exception e)
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