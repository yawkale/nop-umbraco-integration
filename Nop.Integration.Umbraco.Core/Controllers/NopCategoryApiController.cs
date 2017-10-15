using System.Collections.Generic;
using System.Web.Mvc;
using Nop.Integration.Umbraco.Nop;
using Umbraco.Web.WebApi;
using Nop.Integration.Umbraco.Customer;
using System.Linq;
using Nop.Integration.Umbraco.Category;

namespace Nop.Integration.Umbraco.Core.Controllers
{
    public class NopCategoryApiController : UmbracoApiController
    {
        private readonly NopApiService _nopService;
        

        public NopCategoryApiController()
        {
            _nopService = new NopApiService();
        }

        [HttpGet]
        public IEnumerable<DataSearchModel> GetCategories()
        {
            var response = _nopService.GetCategories();

            var data = response.Categories.Select(c => new DataSearchModel()
            {
                Label = c.Name,
                Value = c.Id
            });

            return data;
        }

        [HttpPost]
        public string Create([System.Web.Http.FromBody]string name)
        {
            var category = new PostCategoryObject()
            {
                Name = name
            };

            var categoryId = _nopService.CreateCategory(category);

            return categoryId;
        }

        [HttpPost]
        public string Update(PostCategoryObject category)
        {
            var categoryId = _nopService.UpdateCategory(category);

            return categoryId;
        }
    }
}
