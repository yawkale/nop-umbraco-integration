using System;
using Nop.Api.Adapter.Parameters;
using Nop.Api.Adapter.SettingsProvider;

namespace Nop.Api.Adapter.Managers
{
    public class AuthorizationManager
    {
        private readonly ISettingsProvider _settings;
        private readonly ApiAuthorizer _apiAuthorizer;

        public AuthorizationManager(ISettingsProvider settings)
        {
            _settings = settings;
            _apiAuthorizer = new ApiAuthorizer(_settings.ClientId, _settings.ClientSecret, _settings.ServerUrl);
        }

        public string BuildAuthUrl(string redirectUrl, string[] requestedPermissions, string state = null)
        {
            var returnUrl = new Uri(redirectUrl);

            // get the Authorization URL and redirect the user
            var authUrl = _apiAuthorizer.GetAuthorizationUrl(returnUrl.ToString(), requestedPermissions, state);

            return authUrl;
        }

        public string GetAuthorizationData(AuthParameters authParameters)
        {
            if (!string.IsNullOrEmpty(authParameters.Error))
            {
                throw new Exception(authParameters.Error);
            }

            // make sure we have the necessary parameters
            ValidateParameter("code", authParameters.Code);
            ValidateParameter("storeUrl", authParameters.ServerUrl);
            ValidateParameter("clientId", authParameters.ClientId);
            ValidateParameter("clientSecret", authParameters.ClientSecret);
            ValidateParameter("RedirectUrl", authParameters.RedirectUrl);
            ValidateParameter("GrantType", authParameters.GrantType);

            // get the access token
            var accessToken = _apiAuthorizer.AuthorizeClient(authParameters.Code, authParameters.GrantType, authParameters.RedirectUrl);

            return accessToken;
        }

        public string RefreshAuthorizationData(AuthParameters authParameters)
        {
            if (!string.IsNullOrEmpty(authParameters.Error))
            {
                throw new Exception(authParameters.Error);
            }

            // make sure we have the necessary parameters
            ValidateParameter("storeUrl", authParameters.ServerUrl);
            ValidateParameter("clientId", authParameters.ClientId);
            ValidateParameter("GrantType", authParameters.GrantType);
            ValidateParameter("RefreshToken", authParameters.RefreshToken);

            // get the access token
            var accessToken = _apiAuthorizer.RefreshToken(authParameters.RefreshToken, authParameters.GrantType);

            return accessToken;
        }

        private void ValidateParameter(string parameterName, string parameterValue)
        {
            if (string.IsNullOrWhiteSpace(parameterValue))
            {
                throw new Exception($"{parameterName} parameter is missing");
            }
        }
    }
}