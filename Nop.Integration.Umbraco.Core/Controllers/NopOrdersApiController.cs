using System.Collections.Generic;
using Nop.Integration.Umbraco.Nop;
using Umbraco.Web.WebApi;

namespace Nop.Integration.Umbraco.Core.Controllers
{
    public class NopOrdersApiController : UmbracoApiController
    {
        private readonly NopApiService _nopService;
        

        public NopOrdersApiController()
        {
            _nopService = new NopApiService();
        }

        public List<Order.Order> GetOrders()
        {
            var orders = _nopService.GetAllOrders();

            return orders;
        }
    }
}
