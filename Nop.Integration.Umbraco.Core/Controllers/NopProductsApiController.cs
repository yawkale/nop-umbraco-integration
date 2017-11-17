using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Web.Mvc;
using Nop.Integration.Umbraco.Nop;
using Umbraco.Web.WebApi;
using Nop.Integration.Umbraco.Products;

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
        public List<Product> GetProducts(FormDataCollection queryStrings)
        {
            var products = _nopService.GetAllProducts();

            return products;
        }

        [HttpPost]
        public string Create([System.Web.Http.FromBody]string name)
        {
            var product = new PostProductObject()
            {
                Name = name
            };

            var productId = _nopService.CreateProduct(product);

            return productId;
        }

        [HttpPost]
        public void Update(PostProductObject product)
        {
            _nopService.UpdateProduct(product);
        }
    }
}
