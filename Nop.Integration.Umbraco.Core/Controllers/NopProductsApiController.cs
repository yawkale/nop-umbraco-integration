using Nop.Integration.Umbraco.Core.Services;
using Nop.Integration.Umbraco.Models;
using Nop.Integration.Umbraco.Nop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Mvc;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace NopStarterKit.Web.Controllers
{
    public class NopProductsApiController : UmbracoApiController
    {
        private readonly NopApiService _nopService;
        private readonly UserContext _userContext;

        public NopProductsApiController()
        {
            _nopService = new NopApiService();
            _userContext = new UserContext();
        }

        [HttpGet]
        public List<Product> GetProducts(FormDataCollection queryStrings)
        {
            var products = _nopService.GetAllProducts();

            return products;
        }
    }
}
