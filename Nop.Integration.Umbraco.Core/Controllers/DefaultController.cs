using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Nop.Integration.Umbraco.Core.Core;
using Nop.Integration.Umbraco.Core.Services;
using Nop.Integration.Umbraco.Nop;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace Nop.Integration.Umbraco.Core.Controllers
{
    public class DefaultController : RenderMvcController
    {
        private readonly NopApiService _nopService;
        private readonly UserContext _userContext;
        private static string CustomerTypeCookieName => GlobalSettings.ClientSettings.CustomerTypeCookieName;


        public DefaultController()
        {
            _nopService = new NopApiService();
            _userContext = new UserContext();
        }

        public override ActionResult Index(RenderModel model)
        {
            var currentUser = Members.GetCurrentMember();
          
            if (currentUser != null)
            {
                Response.Cookies.Add(new HttpCookie(CustomerTypeCookieName) { Value = "active" });
                SetCurrentMemberNopId(currentUser);
            }
            else if (_userContext.CustomerId() == null || _userContext.CustomerType() != "temporary")
            {
                Response.Cookies.Add(new HttpCookie(CustomerTypeCookieName) { Value = "temporary" });
                CreateTemporalNopCustomer();
            }

            return base.Index(model);
        }

        public void CreateTemporalNopCustomer()
        {
            var customer = new Customer.Customer()
            {
                roles = new List<int>() { 3 },
                FirstName = "Temp",
                LastName = "Temp",
                Password = Guid.NewGuid().ToString(),
                Email = "temp@temp.temp"
            };

            var customerId = _nopService.CreateCustomer(customer);

            _userContext.SetCustomerId(int.Parse( customerId));
           
        }


        public void SetCurrentMemberNopId(IPublishedContent member)
        {
            var nopCustomerId = Convert.ToInt32( member.GetPropertyValue(GlobalSettings.UmbracoSettings.MemberIdPropertyAlias));

            if (nopCustomerId==0)
            {
                CreateNopCustomer(member);
            }
            else
            {
                _userContext.SetCustomerId(nopCustomerId);
               
            }
        }

        public void CreateNopCustomer(IPublishedContent member)
        {
            var memberService = Services.MemberService;

            var currentMember = memberService.GetById(member.Id);

            var customer = new Customer.Customer()
            {
                roles = new List<int>() { 3 },
                FirstName = currentMember.Name,
                LastName = currentMember.Name,
                Password = Guid.NewGuid().ToString(),
                Email = currentMember.Email
            };

            var customerId = _nopService.CreateCustomer(customer);

            currentMember.SetValue(GlobalSettings.UmbracoSettings.MemberIdPropertyAlias, customerId);

            memberService.Save(currentMember);
        }
    }
}