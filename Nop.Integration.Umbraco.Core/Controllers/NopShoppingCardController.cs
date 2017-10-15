using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Nop.Integration.Umbraco.Core.Services;
using Nop.Integration.Umbraco.Nop;
using Nop.Integration.Umbraco.ShoppingCart;
using Umbraco.Web.Mvc;
using System.IO;

namespace Nop.Integration.Umbraco.Core.Controllers
{
    public class NopShoppingCardController : SurfaceController
    {
        private readonly NopApiService _nopService;
        private readonly UserContext _userContext;

        public NopShoppingCardController()
        {
            _nopService = new NopApiService();
            _userContext = new UserContext();
        }

        public ActionResult GetShoppingCart()
        {
            var card = _nopService.GetShoppingCart(_userContext.CustomerId());

            var products = card.Products;

            return View("~/Views/Partials/ShoppingCart.cshtml", products);
        }

        public void Update(FormCollection form)
        {
            var toRemove = form.Get("removefromcart")?.Split(',').ToList().ConvertAll(s => int.Parse(s));

            if (toRemove != null)
            {
                Remove(toRemove);
            }
            else
            {
                toRemove = new List<int>();
            }

            var card = _nopService.GetShoppingCart(_userContext.CustomerId()).Products.Where(x => !toRemove.Contains(x.Id));

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

        public JsonResult AddToShoppingCart(int productId, int quantity = 1, FormCollection form = null)
        {
            var attributesName = form.AllKeys.Where(f => f.Contains("product_attribute")).ToList();

            var attributes = attributesName.Select(x => new ShoppingCartProductAttribute() {
                Id = x.Replace("product_attribute_", ""),
                Value = form.Get(x)
            }).ToList();

            var shoppingCart = new CreateShoppingCartItem()
            {
                CustomerId = _userContext.CustomerId(),
                ProductId = productId,
                Quantity = quantity,
                CartType = "ShoppingCart",
                Attributes = attributes
            };

            _nopService.CreateShoppingCart(shoppingCart);

            return RenderMiniCartViewToString();
        }

        public JsonResult RenderMiniCartViewToString()
        {
            var card = _nopService.GetShoppingCart(_userContext.CustomerId());
            var products = card.Products;
            ViewData.Model = products;

            var selectionHtml = "";

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, "MiniShoppingCart");
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                selectionHtml = sw.GetStringBuilder().ToString();
            }

            var productsCount = products.Sum(s => s.Quantity);

            return Json(new
            {
                success = true,
                message = "The product has been added to your \u003ca href=\"/cart\"\u003eshopping cart\u003c/a\u003e",
                updatetopcartsectionhtml = $"({productsCount})",
                updateflyoutcartsectionhtml = selectionHtml
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
