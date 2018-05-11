using System.Collections.Generic;
using System.Web.Mvc;
using Nop.Integration.Umbraco.Nop;
using Umbraco.Web.WebApi;
using Nop.Integration.Umbraco.Customer;
using System.Linq;
using Nop.Integration.Umbraco.Category;
using Nop.Integration.Umbraco.Core.Core;

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
            var storeId = GlobalSettings.UmbracoSettings.NopStoreId;
            var isGetProductLimitToStore = GlobalSettings.UmbracoSettings.GetProductLimitToStore;

            var response = _nopService.GetCategories();
            var categories = storeId != 0 && isGetProductLimitToStore ? response.Categories.Where(category => category.StoreIds.Contains(storeId)).ToList() : response.Categories;

            var data = categories.Select(c => new DataSearchModel()
            {
                Label = c.Name,
                Value = c.Id
            });

            return data;
        }

        [HttpPost]
        public string Create([System.Web.Http.FromBody]string name)
        {
            var storeId = GlobalSettings.UmbracoSettings.NopStoreId;
            var isCreateProductLimitToStore = GlobalSettings.UmbracoSettings.CreateProductLimitToStore;

            var category = new PostCategoryObject()
            {
                Name = name
            };


            if (storeId != 0 && isCreateProductLimitToStore)
                category.StoreIds = new[] { storeId };

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
