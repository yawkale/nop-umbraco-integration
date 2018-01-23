using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Integration.Umbraco.Core.Services;
using Umbraco.Web.Mvc;

namespace NopStarterKit.Web.Controllers
{
    public class CheckoutController : SurfaceController
    {
        private OrderService orderService;

        public CheckoutController()
        {
            this.orderService = new OrderService();
        }

        // GET: Checkout
        public ActionResult ConfirmOrder()
        {
            var orderId = orderService.CreateOrder();
            return RedirectToAction("PayPalCreateRequest", "PayPal");
        }
    }
}