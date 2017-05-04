using Nop.Integration.Umbraco.Core.Services;
using Nop.Integration.Umbraco.Models;
using Nop.Integration.Umbraco.Nop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace NopStarterKit.Web.Controllers
{
    public class NopOrdersApiController : UmbracoApiController
    {
        private readonly NopApiService _nopService;
        private readonly UserContext _userContext;

        public NopOrdersApiController()
        {
            _nopService = new NopApiService();
            _userContext = new UserContext();
        }

        public List<Order> GetOrders()
        {
            var orders = _nopService.GetAllOrders();

            return orders;
        }
    }
}
