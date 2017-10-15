using System.Web;
using Nop.Integration.Umbraco.Core.Core;

namespace Nop.Integration.Umbraco.Core.Services
{
    public class UserContext
    {
        public string CustomerId()
        {
            return HttpContext.Current.Request.Cookies[GlobalSettings.ClientSettings.CustomerIdCookieName]?.Value;
        }
        public void SetCustomerId(int customerId)
        {
            HttpContext.Current.Response.SetCookie(new HttpCookie(GlobalSettings.ClientSettings.CustomerIdCookieName) { Value = customerId.ToString() });
        }

        public string CustomerType()
        {
            return HttpContext.Current.Request.Cookies["NopCustomerType"]?.Value;
        }

        
    }
}
