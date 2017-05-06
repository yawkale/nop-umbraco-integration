using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Web.Mvc;
using Nop.Integration.Umbraco.Nop;
using Umbraco.Web.WebApi;

namespace Nop.Integration.Umbraco.Core.Controllers
{
    public class NopProductsApiController : UmbracoApiController
    {
        private readonly NopApiService _nopService;
        

        public NopProductsApiController()
        {
            _nopService = new NopApiService();
        }

        [HttpGet]
        public List<Product.Product> GetProducts(FormDataCollection queryStrings)
        {
            var products = _nopService.GetAllProducts();

            return products;
        }
    }
}
