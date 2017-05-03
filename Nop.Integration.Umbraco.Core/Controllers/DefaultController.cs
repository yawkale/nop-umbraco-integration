using System;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Web.Models;
using System.Web;
using System.Collections.Generic;
using Nop.Integration.Umbraco.Nop;
using Nop.Integration.Umbraco.Models;
using Umbraco.Core.Models;

namespace Archimedicx.Cms.Controllers
{
    public class DefaultController : RenderMvcController
    {
        private readonly NopApiService _nopService;
        private const string CustomerType = "NopCustomerType";
        private const string CustomerId = "NopCustomerId";

        public DefaultController()
        {
            _nopService = new NopApiService();
        }

        public override ActionResult Index(RenderModel model)
        {
            var currentUser = Members.GetCurrentMember();

            if (currentUser != null)
            {
                Response.Cookies.Add(new HttpCookie(CustomerType) { Value = "active" });
                SetCurrentMemberNopId(currentUser);
            }
            else if (Request.Cookies[CustomerId] == null || Request.Cookies[CustomerType]?.Value != "temporary")
            {
                Response.Cookies.Add(new HttpCookie(CustomerType) { Value = "temporary" });
                CreateTemporalNopCustomer();
            }

            return base.Index(model);
        }

        public void CreateTemporalNopCustomer()
        {
            var customer = new Customer()
            {
                roles = new List<int>() { 3 },
                FirstName = "Temp",
                LastName = "Temp",
                Password = Guid.NewGuid().ToString(),
                Email = "temp@temp.temp"
            };

            var cust = _nopService.CreateCustomer(customer);

            Response.Cookies.Add(new HttpCookie("NopCustomerId") { Value = cust });
        }


        public void SetCurrentMemberNopId(IPublishedContent member)
        {
            var nopCustomerId = member.GetProperty("nopCustomerId")?.Value.ToString();

            if (string.IsNullOrEmpty(nopCustomerId.ToString()))
            {
                CreateNopCustomer(member);
            }
            else
            {
                Response.Cookies.Add(new HttpCookie("NopCustomerId") { Value = nopCustomerId });
            }

        }

        public void CreateNopCustomer(IPublishedContent member)
        {
            var memberService = Services.MemberService;

            var currentMember = memberService.GetById(member.Id);

            var customer = new Customer()
            {
                roles = new List<int>() { 3 },
                FirstName = currentMember.Name,
                LastName = currentMember.Name,
                Password = Guid.NewGuid().ToString(),
                Email = currentMember.Email
            };

            var customerId = _nopService.CreateCustomer(customer);

            currentMember.SetValue("NopCustomerId", customerId);

            memberService.Save(currentMember);
        }
    }
}