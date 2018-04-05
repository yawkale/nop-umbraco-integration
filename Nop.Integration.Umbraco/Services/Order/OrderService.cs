using System;
using System.Collections.Generic;
using System.Globalization;
using Nop.Integration.Umbraco.Nop;
using Nop.Integration.Umbraco.Payment;

namespace Nop.Integration.Umbraco.Services.Order
{
    public class OrderService
    {
        private readonly NopApiService _nopService;
        private readonly OrderProcessingService _orderProcessingService;


        public OrderService()
        {
            _nopService = new NopApiService();
  
            _orderProcessingService = new OrderProcessingService();

        }

        public Orders.Order UpdateOrder(Orders.Order order)
        {
           return _nopService.UpdateOrder(order);
        }

        public List<Orders.Order> GetOrders()
        {
            var orders = _nopService.GetAllOrders();

            return orders;
        }

        public string PlaceOrder(int userId)
        {
            var placingOrder = _orderProcessingService.PreparePlaceOrderDetails(userId);
          
            var placedOrder = _nopService.CreateOrder(placingOrder);         
            return placedOrder.Id;
        }

        public bool CanMarkOrderAsPaid(Orders.Order order)
        {
            if (order == null)
                throw new ArgumentNullException("order");

            // OrderStatus.Cancelled
            if (order.OrderStatus == "40")
                return false;

            if (order.PaymentStatus == PaymentStatus.Paid.ToString() ||
                order.PaymentStatus == PaymentStatus.Refunded.ToString() ||
                order.PaymentStatus == PaymentStatus.Voided.ToString())
                return false;

            return true;
        }

        public Orders.Order GetOrderById(int id)
        {      
           return  _nopService.GetOrder(id);       
        }

        public bool MarkOrderAsPaid(string orderId)
        {
            int Id = 0;
            Int32.TryParse(orderId, out Id);
            var order = GetOrderById(Id);

            if (!CanMarkOrderAsPaid(order)) return false;
            order.PaymentStatus = PaymentStatus.Paid.ToString();
            order.PaidDateUtc = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
            UpdateOrder(order);
            return true;
        }
    }
}