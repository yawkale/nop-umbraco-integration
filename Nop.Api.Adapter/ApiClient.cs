using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using Nop.Api.Adapter.Managers;
using Nop.Api.Adapter.SettingsProvider;

namespace Nop.Api.Adapter
{
    public class ApiClient
    {
        private readonly ISettingsProvider _settings;
        protected const string DefaultContentType = "application/json";

        public ApiClient(ISettingsProvider settings)
        {
            _settings = settings;
        }
        public object Call(HttpMethods method, string path)
        {
            return Call(method, path, string.Empty);
        }

        public object Call(HttpMethods method, string path, object callParams)
        {
            var requestUriString = $"{_settings.ServerUrl}/{path}";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUriString);

            httpWebRequest.ContentType = DefaultContentType;

            httpWebRequest.Headers.Add("Authorization", $"Bearer {AccessProvider.Token}");

            httpWebRequest.Method = ((object)method).ToString();

            if (callParams != null)
            {
                if (method == HttpMethods.Get || method == HttpMethods.Delete)
                {
                    return $"{requestUriString}?{callParams}";
                }

                if (method == HttpMethods.Post || method == HttpMethods.Put)
                {
                    using (new MemoryStream())
                    {
                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            streamWriter.Write(callParams);
                            streamWriter.Close();
                        }
                    }
                }
            }

            HttpWebResponse httpWebResponse = null;

            try
            {
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            }
            catch(WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    if (resp.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        

                        GetAccessToken();
                        
                        return Call(method, path, callParams);
                    }
                }
                throw new Exception($"Error call api {ex.Status}, request is {requestUriString}",ex);
            }

            var encodedData = string.Empty;

            using (var responseStream = httpWebResponse.GetResponseStream())
            {
                if (responseStream != null)
                {
                    var streamReader = new StreamReader(responseStream);
                    encodedData = streamReader.ReadToEnd();
                    streamReader.Close();
                }
            }

            return encodedData;
        }

        public object Get(string path)
        {
            return Get(path, null);
        }

        public object Get(string path, NameValueCollection callParams)
        {
            return Call(HttpMethods.Get, path, callParams);
        }

        public object Post(string path, object data)
        {
            return Call(HttpMethods.Post, path, data);
        }

        public object Put(string path, object data)
        {
            return Call(HttpMethods.Put, path, data);
        }

        public object Delete(string path)
        {
            return Call(HttpMethods.Delete, path, null);
        }
        private void GetAccessToken()
        {
            var nopAuthorizationManager = new AuthorizationManager(_settings);

            var authUrl = nopAuthorizationManager.BuildAuthUrl(_settings.RedirectUrl, new string[] { });

            var request = WebRequest.Create(authUrl);

            request.Credentials = CredentialCache.DefaultCredentials;

            var response = request.GetResponse();
        }
    }
}