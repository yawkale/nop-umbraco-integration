using Commerce.Api.Adapter.Managers;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;

namespace Commerce.Api.Adapter
{
    public class ApiClient
    {
        protected const string DefaultContentType = "application/json";
        private readonly string _accessToken;
        private readonly string _serverUrl;

        public ApiClient(string accessToken, string serverUrl)
        {
            _accessToken = accessToken;
            _serverUrl = serverUrl;
        }

        public object Call(HttpMethods method, string path)
        {
            return Call(method, path, string.Empty);
        }

        public object Call(HttpMethods method, string path, object callParams)
        {
            string requestUriString = string.Format("{0}/{1}", AccessProvider._serverUrl, path);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUriString);

            httpWebRequest.ContentType = DefaultContentType;

            httpWebRequest.Headers.Add("Authorization", string.Format("Bearer {0}", AccessProvider._token));

            httpWebRequest.Method = ((object)method).ToString();

            if (callParams != null)
            {
                if (method == HttpMethods.Get || method == HttpMethods.Delete)
                {
                    return string.Format("{0}?{1}", requestUriString, callParams);
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
                var rep = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();

                dynamic obj = JsonConvert.DeserializeObject(rep);
                //var messageFromServer = obj.error.message;

                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    if (resp.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        AccessProvider provider = new AccessProvider();

                        provider.GetAccessToken();
                        //callParams = "{\r\n  \"shopping_cart_item\": [\r\n    {\r\n      \r\n      \"customer_entered_price\": 0.0000,\r\n      \"quantity\": 1,\r\n      \"shopping_cart_type\": \"ShoppingCart\",\r\n      \"product_id\": 4,\r\n      \"customer_id\": 1,\r\n    }\r\n  ]\r\n}";
                        return Call(method, path, callParams);
                    }
                   
                }
            }

            string encodedData = string.Empty;

            using (Stream responseStream = httpWebResponse.GetResponseStream())
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
    }
}