using System;
using System.Collections.Generic;
using System.Web;
using Nop.Integration.Umbraco.Core.Controllers;
using Nop.Integration.Umbraco.Core.Services;
using Nop.Integration.Umbraco.Nop;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web.Mvc;

namespace Nop.Integration.Umbraco.Core
{
    public class Startup : IApplicationEventHandler
    {
        private readonly NopApiService _nopService;
        private readonly UserContext _userContext;
        private const string CustomerId = "NopCustomerId";
        private const string PropertyTypeAlias = "nopCustomerId";
        protected const string NopDashboardSectionAlias = "nopdashboard";

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
            MemberService.Saved += MemberService_Saved;

            DefaultRenderMvcControllerResolver.Current.SetDefaultControllerType(typeof(DefaultController));

            Section section = applicationContext.Services.SectionService.GetByAlias(NopDashboardSectionAlias);
            
            if (section != null) return;

            applicationContext.Services.SectionService.MakeNew("NopDashboard", NopDashboardSectionAlias, "icon-newspaper");
            
            //applicationContext.Services.UserService.AddSectionToAllUsers(DashboardAlias);
         }

        private void MemberService_Saved(IMemberService sender, global::Umbraco.Core.Events.SaveEventArgs<IMember> e)
        {
            foreach (var member in e.SavedEntities)
            {
                if (string.IsNullOrEmpty(member.GetValue<string>(PropertyTypeAlias)))
                {
                    var customer = new Customer.Customer()
                    {
                        roles = new List<int>() { 3 },
                        FirstName = member.Name,
                        LastName = member.Name,
                        Password = Guid.NewGuid().ToString(),
                        Email = member.Email
                    };

                    string customerId;

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