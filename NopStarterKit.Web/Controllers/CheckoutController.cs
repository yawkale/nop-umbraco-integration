using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Integration.Umbraco.Core.Services;
using Nop.Integration.Umbraco.Services.Order;
using Umbraco.Web.Mvc;

namespace NopStarterKit.Web.Controllers
{
    public class CheckoutController : SurfaceController
    {
        private OrderService orderService;
        private UserContext userContext;
        public CheckoutController()
        {
            this.orderService = new OrderService();
            this.userContext = new UserContext();
        }

        // GET: Checkout
        public ActionResult ConfirmOrder()
        {
            int customerIdParseResult;
            int.TryParse(userContext.CustomerId(), out customerIdParseResult);
            var id = orderService.PlaceOrder(customerIdParseResult);
            return RedirectToAction("PayPalCreateRequest", "PayPal", new { orderId  = id });
        }

    }
}