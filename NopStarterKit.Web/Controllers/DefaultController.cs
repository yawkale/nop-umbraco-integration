using System;
using System.Net;
using System.Web.Mvc;
using Newtonsoft.Json;
using Commerce.Api.Adapter;
using Commerce.Api.Adapter.Managers;
using Umbraco.Web.Mvc;
using System.Web.UI;
using Umbraco.Web.Models;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using Nop.Integration.Umbraco.Nop;
using Nop.Integration.Umbraco.Models;

namespace Archimedicx.Cms.Controllers
{
    public class DefaultController : RenderMvcController
    {
        private readonly NopApiService _nopService;

        public DefaultController()
        {
            _nopService = new NopApiService();
        }

        public override ActionResult Index(RenderModel model)
        {
            if (Request.Cookies["NopCustomerId"] == null)
            {
                var customer = new Customer()
                {
                    roles = new List<int>() { 3 },
                    FirstName = "Temp",
                    LastName = "Temp",
                    Password = "Aa123@",
                    Email = "temp@temp.temp"
                };

                var cust = _nopService.CreateCustomer(customer);

                Response.Cookies.Add(new HttpCookie("NopCustomerId") { Value = cust });
            }

            return base.Index(model);
        }
    }
}