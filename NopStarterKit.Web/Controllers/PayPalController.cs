using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Owin.Security.Provider;
using PayPal.Manager;
using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace NopStarterKit.Web.Controllers
{
    public class PayPalController : SurfaceController
    {
        // GET: PayPal
        public ActionResult PayPalCreateRequest()
        {
            Dictionary<string, string> config = ConfigManager.Instance.GetProperties();
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(config);

            var setExpressCheckoutRequestType = new SetExpressCheckoutRequestType
            {
                SetExpressCheckoutRequestDetails = new SetExpressCheckoutRequestDetailsType() 
                {
                    OrderTotal = new BasicAmountType(CurrencyCodeType.USD, "20"),
                    CancelURL = ConfigurationManager.AppSettings["PAYPAL_CANCEL_URL"],
                    ReturnURL = ConfigurationManager.AppSettings["PAYPAL_RETURN_URL"],
                }
            };

            SetExpressCheckoutReq request = new SetExpressCheckoutReq
            {
                SetExpressCheckoutRequest = setExpressCheckoutRequestType

            };

            var response = service.SetExpressCheckout(request);
            if (!response.Ack.ToString().Trim().ToUpper().Equals(AckCodeType.FAILURE.ToString()) && !response.Ack.ToString().Trim().ToUpper().Equals(AckCodeType.FAILUREWITHWARNING.ToString()))
            {
               var redirectUrl = ConfigurationManager.AppSettings["PAYPAL_REDIRECT_URL"] + "_express-checkout&token=" + response.Token;
               return Redirect(redirectUrl);
            }

            return ErrorTransaction();
        }

        public ActionResult GetExpressCheckout(string token)
        {
            Dictionary<string, string> config = ConfigManager.Instance.GetProperties();
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(config);
            GetExpressCheckoutDetailsReq request = new GetExpressCheckoutDetailsReq
            {
                GetExpressCheckoutDetailsRequest = new GetExpressCheckoutDetailsRequestType
                {
                    Token = token
                }
            };

            var response = service.GetExpressCheckoutDetails(request);
            if (response.Ack.ToString().Trim().ToUpper().Equals(AckCodeType.SUCCESS.ToString()))
                return SuccessTransaction();
            return ErrorTransaction();
        }
        public RedirectToUmbracoPageResult HandleCancelExpressCheckout()
        {
            var page = Umbraco.TypedContentAtXPath("//onePageCheckout").FirstOrDefault();
            return RedirectToUmbracoPage(page);
        }

        public ActionResult ErrorTransaction()
        {
            return Content("Error");
        }

        public ActionResult SuccessTransaction()
        {
            return Content("OK");
        }

       
    }
}