using System;
using System.Collections.Generic;
using System.Globalization;
using Nop.Integration.Umbraco.Nop;
using Nop.Integration.Umbraco.Orders;

namespace Nop.Integration.Umbraco.Core.Services
{
    public class OrderService
    {
        private readonly NopApiService _nopService;
        private readonly UserContext _userContext;


        public OrderService()
        {
            _nopService = new NopApiService();
            _userContext = new UserContext();
        }

        public List<Orders.Order> GetOrders()
        {
            var orders = _nopService.GetAllOrders();

            return orders;
        }

        public string CreateOrder()
        {

            int customerIdParseResult;
            int.TryParse(_userContext.CustomerId(), out customerIdParseResult);
            var orderItems = new List<OrderItem> { new OrderItem() { Quantity = 2, ProductId = 38 }, new OrderItem() { Quantity = 1, ProductId = 2 } };
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


            var stubOrder = new Orders.Order
            {
                CustomerId = customerIdParseResult,
                OrderItems = orderItems,
                ShippingMethod = "Shipping.FixedRate",
                ShippingRateComputationMethodSystemName = " Shipping Rate Computation Method System Name",
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                CreatedOnUtc = DateTime.UtcNow,
                PaidDateUtc = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
                PaymentMethodSystemName = "Payments.Manual"

            };

            var order = _nopService.CreateOrder(stubOrder);
            return order.Id;
        }
    }
}