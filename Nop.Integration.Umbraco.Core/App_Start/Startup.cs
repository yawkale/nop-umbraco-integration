using Archimedicx.Cms.Controllers;
using Newtonsoft.Json;
using Nop.Integration.Umbraco.Core.Services;
using Nop.Integration.Umbraco.Models;
using Nop.Integration.Umbraco.Nop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace UteamTemplate.App_Start
{
    public partial class Startup : IApplicationEventHandler
    {
        private readonly NopApiService _nopService;
        private readonly UserContext _userContext;
        private const string CustomerId = "NopCustomerId";
        private const string PropertyTypeAlias = "nopCustomerId";
        protected const string SkriftSectionAlias = "skrift";

        public Startup()
        {
            _nopService = new NopApiService();
            _userContext = new UserContext();
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

            Section section = applicationContext.Services.SectionService.GetByAlias(SkriftSectionAlias);
            
            if (section != null) return;

            applicationContext.Services.SectionService.MakeNew("Skrift", SkriftSectionAlias, "icon-newspaper");
            
            //applicationContext.Services.UserService.AddSectionToAllUsers(DashboardAlias);
        }

        private void MemberService_Saved(Umbraco.Core.Services.IMemberService sender, Umbraco.Core.Events.SaveEventArgs<Umbraco.Core.Models.IMember> e)
        {
            foreach (var member in e.SavedEntities)
            {
                if (string.IsNullOrEmpty(member.GetValue<string>(PropertyTypeAlias)))
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

                    if (string.IsNullOrEmpty(_userContext.CustomerId()))
                    {
                        customerId = _nopService.CreateCustomer(customer);
                        HttpContext.Current.Response.SetCookie(new HttpCookie(CustomerId) { Value = customerId });
                    }
                    else
                    {
                        var nopCustomerId = _userContext.CustomerId();
                        customerId = _nopService.UpdateCustomer(customer, nopCustomerId);
                    }

                    member.SetValue(PropertyTypeAlias, customerId);
                }
            }
        }
    }
}