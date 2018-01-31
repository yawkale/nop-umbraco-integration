using Nop.Integration.Umbraco.Nop;
using Nop.Integration.Umbraco.ShoppingCart;

namespace Nop.Integration.Umbraco.Services.ShoppingCart
{
    public class ShoppingCartService
    {
        private readonly NopApiService _nopService;

        public ShoppingCartService()
        {
            _nopService = new NopApiService();
        }

        public ShoppingCartRootObject GetShoppingCart(string customerId)
        {
            var cart = _nopService.GetShoppingCart(customerId);
            return cart;
        }
    }
}
