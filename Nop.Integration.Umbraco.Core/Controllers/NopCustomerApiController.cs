using System.Collections.Generic;
using System.Web.Mvc;
using Nop.Integration.Umbraco.Nop;
using Umbraco.Web.WebApi;
using Nop.Integration.Umbraco.Customer;
using System.Linq;

namespace Nop.Integration.Umbraco.Core.Controllers
{
    public class NopCustomerApiController : UmbracoApiController
    {
        private readonly NopApiService _nopService;
        

        public NopCustomerApiController()
        {
            _nopService = new NopApiService();
        }

        [HttpGet]
        public IEnumerable<DataSearchModel> GetCustomers()
        {
            var response = _nopService.GetCustomers();

            var data = response.Customers.Select(c => new DataSearchModel()
            {
                Label = $"{c.FirstName} {c.LastName} ({c.Email})",
                Value = c.Id
            });

            return data;
        }
    }
}
