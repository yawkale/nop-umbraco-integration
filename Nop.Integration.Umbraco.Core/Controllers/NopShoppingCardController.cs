using Nop.Integration.Umbraco.Models;
using Nop.Integration.Umbraco.Nop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace NopStarterKit.Web.Controllers
{
    public class NopShoppingCardController : SurfaceController
    {
        private readonly NopApiService _nopService;

        public NopShoppingCardController()
        {
            _nopService = new NopApiService();
        }

        public ActionResult GetShoppingCart()
        {
            var card = _nopService.GetShoppingCart(Request.Cookies["NopCustomerId"]?.Value);

            var products = card.products;

            return View("~/Views/Partials/ShoppingCart.cshtml", products);
        }

        public void Update(FormCollection form)
        {
            var toRemove = form.Get("removefromcart")?.Split(',').ToList().ConvertAll(s => Int32.Parse(s));

            if (toRemove != null)
            {
                Remove(toRemove);
            }
            else
            {
                toRemove = new List<int>();
            }

            var card = _nopService.GetShoppingCart(Request.Cookies["NopCustomerId"]?.Value).products.Where(x => !toRemove.Contains(x.Id));

            var updatedCard = card.Select(i => { i.Quantity = int.Parse(form.Get(i.Id.ToString())); return i; });

            foreach (var item in updatedCard)
            {
                _nopService.UpdateShoppingCart(item);
            }
        }

        private void Remove(List<int> shoppingCartItemIds)
        {
            foreach (var item in shoppingCartItemIds)
            {
                _nopService.RemoveShoppingCartItem(item);
            }
        }

        public JsonResult AddToShoppingCart(int productId, int quantity = 1)
        {
            var shoppingCart = new CreateShoppingCartItem()
            {
                CustomerId = Request.Cookies["NopCustomerId"]?.Value,
                ProductId = productId,
                Quantity = quantity,
                CartType = "ShoppingCart",
            };

            _nopService.CreateShoppingCart(shoppingCart);

            return Json(new
            {
                success = true,
                message = "The product has been added to your \u003ca href=\"/cart\"\u003eshopping cart\u003c/a\u003e",
                updatetopcartsectionhtml = "(0)",
                updateflyoutcartsectionhtml = ""
            });

            //\u003cdiv id =\"flyout-cart\" class=\"flyout-cart\"\u003e\r\n    \u003cdiv class=\"mini-shopping-cart\"\u003e\r\n        \u003cdiv class=\"count\"\u003e\r\nThere are \u003ca href=\"/cart\"\u003e19 item(s)\u003c/a\u003e in your cart.        \u003c/div\u003e\r\n            \u003cdiv class=\"items\"\u003e\r\n                    \u003cdiv class=\"item first\"\u003e\r\n                            \u003cdiv class=\"picture\"\u003e\r\n                                \u003ca href=\"/apple-macbook-pro-13-inch\" title=\"Show details for Apple MacBook Pro 13-inch\"\u003e\r\n                                    \u003cimg alt=\"Picture of Apple MacBook Pro 13-inch\" src=\"http://localhost:59368/content/images/thumbs/0000024_apple-macbook-pro-13-inch_70.jpeg\" title=\"Show details for Apple MacBook Pro 13-inch\" /\u003e\r\n                                \u003c/a\u003e\r\n                            \u003c/div\u003e\r\n                        \u003cdiv class=\"product\"\u003e\r\n                            \u003cdiv class=\"name\"\u003e\r\n                                \u003ca href=\"/apple-macbook-pro-13-inch\"\u003eApple MacBook Pro 13-inch\u003c/a\u003e\r\n                            \u003c/div\u003e\r\n                            \u003cdiv class=\"price\"\u003eUnit price: \u003cspan\u003e$1,800.00\u003c/span\u003e\u003c/div\u003e\r\n                            \u003cdiv class=\"quantity\"\u003eQuantity: \u003cspan\u003e10\u003c/span\u003e\u003c/div\u003e\r\n                        \u003c/div\u003e\r\n                    \u003c/div\u003e\r\n                    \u003cdiv class=\"item\"\u003e\r\n                            \u003cdiv class=\"picture\"\u003e\r\n                                \u003ca href=\"/nike-floral-roshe-customized-running-shoes\" title=\"Show details for Nike Floral Roshe Customized Running Shoes\"\u003e\r\n                                    \u003cimg alt=\"Picture of Nike Floral Roshe Customized Running Shoes\" src=\"http://localhost:59368/content/images/thumbs/0000051_nike-floral-roshe-customized-running-shoes_70.jpg\" title=\"Show details for Nike Floral Roshe Customized Running Shoes\" /\u003e\r\n                                \u003c/a\u003e\r\n                            \u003c/div\u003e\r\n                        \u003cdiv class=\"product\"\u003e\r\n                            \u003cdiv class=\"name\"\u003e\r\n                                \u003ca href=\"/nike-floral-roshe-customized-running-shoes\"\u003eNike Floral Roshe Customized Running Shoes\u003c/a\u003e\r\n                            \u003c/div\u003e\r\n                                \u003cdiv class=\"attributes\"\u003e\r\n                                    Size: 8\u003cbr /\u003eColor: White/Blue\u003cbr /\u003ePrint: Natural\r\n                                \u003c/div\u003e\r\n                            \u003cdiv class=\"price\"\u003eUnit price: \u003cspan\u003e$40.00\u003c/span\u003e\u003c/div\u003e\r\n                            \u003cdiv class=\"quantity\"\u003eQuantity: \u003cspan\u003e1\u003c/span\u003e\u003c/div\u003e\r\n                        \u003c/div\u003e\r\n                    \u003c/div\u003e\r\n                    \u003cdiv class=\"item\"\u003e\r\n                            \u003cdiv class=\"picture\"\u003e\r\n                                \u003ca href=\"/fahrenheit-451-by-ray-bradbury\" title=\"Show details for Fahrenheit 451 by Ray Bradbury\"\u003e\r\n                                    \u003cimg alt=\"Picture of Fahrenheit 451 by Ray Bradbury\" src=\"http://localhost:59368/content/images/thumbs/0000068_fahrenheit-451-by-ray-bradbury_70.jpeg\" title=\"Show details for Fahrenheit 451 by Ray Bradbury\" /\u003e\r\n                                \u003c/a\u003e\r\n                            \u003c/div\u003e\r\n                        \u003cdiv class=\"product\"\u003e\r\n                            \u003cdiv class=\"name\"\u003e\r\n                                \u003ca href=\"/fahrenheit-451-by-ray-bradbury\"\u003eFahrenheit 451 by Ray Bradbury\u003c/a\u003e\r\n                            \u003c/div\u003e\r\n                            \u003cdiv class=\"price\"\u003eUnit price: \u003cspan\u003e$27.00\u003c/span\u003e\u003c/div\u003e\r\n                            \u003cdiv class=\"quantity\"\u003eQuantity: \u003cspan\u003e1\u003c/span\u003e\u003c/div\u003e\r\n                        \u003c/div\u003e\r\n                    \u003c/div\u003e\r\n                    \u003cdiv class=\"item\"\u003e\r\n                            \u003cdiv class=\"picture\"\u003e\r\n                                \u003ca href=\"/digital-storm-vanquish-3-custom-performance-pc\" title=\"Show details for Digital Storm VANQUISH 3 Custom Performance PC\"\u003e\r\n                                    \u003cimg alt=\"Picture of Digital Storm VANQUISH 3 Custom Performance PC\" src=\"http://localhost:59368/content/images/thumbs/0000022_digital-storm-vanquish-3-custom-performance-pc_70.jpeg\" title=\"Show details for Digital Storm VANQUISH 3 Custom Performance PC\" /\u003e\r\n                                \u003c/a\u003e\r\n                            \u003c/div\u003e\r\n                        \u003cdiv class=\"product\"\u003e\r\n                            \u003cdiv class=\"name\"\u003e\r\n                                \u003ca href=\"/digital-storm-vanquish-3-custom-performance-pc\"\u003eDigital Storm VANQUISH 3 Custom Performance PC\u003c/a\u003e\r\n                            \u003c/div\u003e\r\n                            \u003cdiv class=\"price\"\u003eUnit price: \u003cspan\u003e$1,259.00\u003c/span\u003e\u003c/div\u003e\r\n                            \u003cdiv class=\"quantity\"\u003eQuantity: \u003cspan\u003e1\u003c/span\u003e\u003c/div\u003e\r\n                        \u003c/div\u003e\r\n                    \u003c/div\u003e\r\n                    \u003cdiv class=\"item\"\u003e\r\n                            \u003cdiv class=\"picture\"\u003e\r\n                                \u003ca href=\"/adobe-photoshop-cs4\" title=\"Show details for Adobe Photoshop CS4\"\u003e\r\n                                    \u003cimg alt=\"Picture of Adobe Photoshop CS4\" src=\"http://localhost:59368/content/images/thumbs/0000032_adobe-photoshop-cs4_70.jpeg\" title=\"Show details for Adobe Photoshop CS4\" /\u003e\r\n                                \u003c/a\u003e\r\n                            \u003c/div\u003e\r\n                        \u003cdiv class=\"product\"\u003e\r\n                            \u003cdiv class=\"name\"\u003e\r\n                                \u003ca href=\"/adobe-photoshop-cs4\"\u003eAdobe Photoshop CS4\u003c/a\u003e\r\n                            \u003c/div\u003e\r\n                            \u003cdiv class=\"price\"\u003eUnit price: \u003cspan\u003e$75.00\u003c/span\u003e\u003c/div\u003e\r\n                            \u003cdiv class=\"quantity\"\u003eQuantity: \u003cspan\u003e1\u003c/span\u003e\u003c/div\u003e\r\n                        \u003c/div\u003e\r\n                    \u003c/div\u003e\r\n            \u003c/div\u003e\r\n            \u003cdiv class=\"totals\"\u003eSub-Total: \u003cstrong\u003e$23,112.00\u003c/strong\u003e\u003c/div\u003e\r\n            \u003cdiv class=\"buttons\"\u003e\r\n                    \u003cinput type=\"button\" value=\"Go to cart\" class=\"button-1 cart-button\" onclick=\"setLocation(\u0027/cart\u0027)\" /\u003e\r\n                            \u003c/div\u003e\r\n    \u003c/div\u003e\r\n\u003c/div\u003e\r\n"
        }
    }
}
