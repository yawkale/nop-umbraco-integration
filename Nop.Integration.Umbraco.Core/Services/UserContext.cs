using System.Web;

namespace Nop.Integration.Umbraco.Core.Services
{
    public class UserContext
    {
        public string CustomerId()
        {
            return HttpContext.Current.Request.Cookies["NopCustomerId"]?.Value;
        }

        public string CustomerType()
        {
            return HttpContext.Current.Request.Cookies["NopCustomerType"]?.Value;
        }
    }
}
