using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Nop.Integration.Umbraco.Core.Core;
using Nop.Integration.Umbraco.Services.Order;
using PayPal.Manager;
using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;
using Umbraco.Web.Mvc;

namespace NopStarterKit.Web.Controllers
{
    public class PayPalController : SurfaceController
    {
        OrderService _orderService;
        // GET: PayPal
        public ActionResult PayPalCreateRequest(string orderId)
        {
            var config = ConfigManager.Instance.GetProperties();
            var service = new PayPalAPIInterfaceServiceService(config);

            var setExpressCheckoutRequestType = new SetExpressCheckoutRequestType
            {
                SetExpressCheckoutRequestDetails = new SetExpressCheckoutRequestDetailsType() 
                {
                    OrderTotal = new BasicAmountType(CurrencyCodeType.USD, "20"),
                    CancelURL = GlobalSettings.PayPalSettings.PayPalCancelUrl,
                    ReturnURL = GlobalSettings.PayPalSettings.PayPalReturnUrl + "/?orderId="+orderId,
                }
            };

            var request = new SetExpressCheckoutReq
            {
                SetExpressCheckoutRequest = setExpressCheckoutRequestType

            };

            var response = service.SetExpressCheckout(request);
            if (!response.Ack.ToString().Trim().ToUpper().Equals(AckCodeType.FAILURE.ToString()) && !response.Ack.ToString().Trim().ToUpper().Equals(AckCodeType.FAILUREWITHWARNING.ToString()))
            {
                var redirectUrl = GlobalSettings.PayPalSettings.PayPalRedirectUrl + "_express-checkout&token=" + response.Token;
                return Redirect(redirectUrl);
            }

            return ErrorTransaction();
        }

        public ActionResult GetExpressCheckout(string token, string orderId)
        {
            var config = ConfigManager.Instance.GetProperties();
            var service = new PayPalAPIInterfaceServiceService(config);
            var request = new GetExpressCheckoutDetailsReq
            {
                GetExpressCheckoutDetailsRequest = new GetExpressCheckoutDetailsRequestType
                {
                    Token = token
                }
            };

            var response = service.GetExpressCheckoutDetails(request);
            if (response.Ack.ToString().Trim().ToUpper().Equals(AckCodeType.SUCCESS.ToString()))
            {

                _orderService = new OrderService();
                var isMarked = _orderService.MarkOrderAsPaid(orderId);

                // TODO process this exception
                if (!isMarked)
                    throw new Exception("Order was not marked as paid");
                return SuccessTransaction();
            }
           
            return ErrorTransaction();
        }
        public RedirectToUmbracoPageResult HandleCancelExpressCheckout()
        {
            var page = Umbraco.TypedContentAtXPath("//onePageCheckout").FirstOrDefault();
            return RedirectToUmbracoPage(page);
        }

        public ActionResult ErrorTransaction()
        {
            var page = Umbraco.TypedContentAtXPath("//errorPage").FirstOrDefault();
            return RedirectToUmbracoPage(page);
        }

        public ActionResult SuccessTransaction()
        {
            var page = Umbraco.TypedContentAtXPath("//successPage").FirstOrDefault();
            return RedirectToUmbracoPage(page);
        }
    }
}