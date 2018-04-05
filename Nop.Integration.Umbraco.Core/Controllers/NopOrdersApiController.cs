using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using Nop.Integration.Umbraco.Core.Services;
using Nop.Integration.Umbraco.Nop;
using Nop.Integration.Umbraco.Orders;
using Umbraco.Web.WebApi;

namespace Nop.Integration.Umbraco.Core.Controllers
{
    public class NopOrdersApiController : UmbracoApiController
    {
        private readonly NopApiService _nopService;
        private readonly UserContext _userContext;


        public NopOrdersApiController()
        {
            _nopService = new NopApiService();
            _userContext = new UserContext();
        }

        public List<Orders.Order> GetOrders()
        {
            var orders = _nopService.GetAllOrders();

            return orders;
        }

        [HttpPost]
        public void Update(Orders.Order order)
        {
            _nopService.UpdateOrder(order);
        }

        public void GetOrder(int id)
        {
            _nopService.GetOrder(id);
        }

        public ActionResult CreateOrder()
        {
            // uncomment for postman customerIdParseResult = 1
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
                CreatedOnUtc = DateTime.UtcNow,
                 
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
                BillingAddress= billingAddress,
                CreatedOnUtc = DateTime.UtcNow,
                PaidDateUtc = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
                PaymentMethodSystemName = "Payments.Manual"
                

            };

            var order = _nopService.CreateOrder(stubOrder);
         
            return new EmptyResult();
        }



    }
}
