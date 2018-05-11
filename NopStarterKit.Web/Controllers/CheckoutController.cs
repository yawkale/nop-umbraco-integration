using System.Web.Mvc;
using Nop.Integration.Umbraco.Core.Services;
using Nop.Integration.Umbraco.Services.Order;
using Umbraco.Web.Mvc;

namespace NopStarterKit.Web.Controllers
{
    public class CheckoutController : SurfaceController
    {
        private readonly OrderService _orderService;
        private readonly UserContext _userContext;
        public CheckoutController()
        {
            _orderService = new OrderService();
            _userContext = new UserContext();
        }

        // GET: Checkout
        public ActionResult ConfirmOrder()
        {
            int customerIdParseResult;
            int.TryParse(_userContext.CustomerId(), out customerIdParseResult);
            var id = _orderService.PlaceOrder(customerIdParseResult);
            return RedirectToAction("PayPalCreateRequest", "PayPal", new { orderId  = id });
        }

    }
}