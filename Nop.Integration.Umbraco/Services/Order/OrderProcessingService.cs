using System;
using System.Collections.Generic;
using Nop.Integration.Umbraco.Orders;
using Nop.Integration.Umbraco.Products;
using Nop.Integration.Umbraco.Services.ShoppingCart;

namespace Nop.Integration.Umbraco.Services.Order
{
    public class OrderProcessingService
    {
        private ShoppingCartService shoppingCartService;

        public OrderProcessingService()
        {
            this.shoppingCartService = new ShoppingCartService();
        }

        public Orders.Order PreparePlaceOrderDetails(int userId)
        {

            var orderItems = new List<OrderItem>();/* { new OrderItem() { Quantity = 2, ProductId = 38 }, new OrderItem() { Quantity = 1, ProductId = 2 } };*/
            var cart = shoppingCartService.GetShoppingCart(userId.ToString());
            var products = cart.Products;

            // TODO make it with mapper
            foreach (var product in products)
            {
                OrderItem item = new OrderItem
                {
                    Quantity = product.Quantity,
                    ProductId = product.ProductId,
                    Product = product.Product
                };

                orderItems.Add(item);
            }

            // TODO correct address
            #region Adresses
            var shippingAddress = new Address
            {
                Address1 = "21 West 52nd Street",
                Email = "alex@uteam.co.il",
                FirstName = "John",
                LastName = "Smith",
                City = "New York",
                PhoneNumber = "12345678",
                ZipPostalCode = "10021",
                CountryId = 1,
                CreatedOnUtc = DateTime.UtcNow
            };

            var billingAddress = new Address
            {
                Address1 = "21 West 52nd Street",
                Email = "alex@uteam.co.il",
                FirstName = "John",
                LastName = "Smith",
                City = "New York",
                PhoneNumber = "12345678",
                ZipPostalCode = "10021",
                CountryId = 1,
                CreatedOnUtc = DateTime.UtcNow,

            };
            #endregion

            var order = new Orders.Order
            {
                CustomerId = userId,
                OrderItems = orderItems,
                ShippingMethod = "Shipping.FixedRate",
                ShippingRateComputationMethodSystemName = "Shipping Rate Computation Method System Name",
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                CreatedOnUtc = DateTime.UtcNow,
                PaymentMethodSystemName = "Payments.Manual"
            };
            return order;
        }
    }
}
