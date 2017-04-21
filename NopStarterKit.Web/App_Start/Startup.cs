using Archimedicx.Cms.Controllers;
using Newtonsoft.Json;
using Nop.Integration.Umbraco.Models;
using Nop.Integration.Umbraco.Nop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Web.Mvc;

namespace UteamTemplate.App_Start
{
    public partial class Startup : IApplicationEventHandler
    {
        private readonly NopApiService _nopService;

        public Startup()
        {
            _nopService = new NopApiService();
        }

        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
           
        }

        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {

        }

        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            Umbraco.Core.Services.MemberService.Saved += MemberService_Saved;

            DefaultRenderMvcControllerResolver.Current.SetDefaultControllerType(typeof(DefaultController));
        }

        private void MemberService_Saved(Umbraco.Core.Services.IMemberService sender, Umbraco.Core.Events.SaveEventArgs<Umbraco.Core.Models.IMember> e)
        {
            var propertyTypeAlias = "nopCustomerId";
            
            foreach (var member in e.SavedEntities)
            {
                if (string.IsNullOrEmpty(member.GetValue<string>(propertyTypeAlias)))
                {
                    var customer = new Customer()
                    {
                        roles = new List<int>() { 3 },
                        FirstName = member.Name,
                        LastName = member.Name,
                        Password = Guid.NewGuid().ToString(),
                        Email = member.Email
                    };

                    string customerId = "";
                    
                    if (HttpContext.Current.Request.Cookies["NopCustomerId"] == null)
                    {
                        customerId = _nopService.CreateCustomer(customer);
                        HttpContext.Current.Response.SetCookie(new HttpCookie("NopCustomerId") { Value = customerId });
                    }
                    else
                    {
                        var nopCustomerId = HttpContext.Current.Request.Cookies.Get("NopCustomerId").Value;
                        customerId = _nopService.UpdateCustomer(customer, nopCustomerId);
                    }

                    member.SetValue(propertyTypeAlias, customerId);
                }
            }
        }
    }
}