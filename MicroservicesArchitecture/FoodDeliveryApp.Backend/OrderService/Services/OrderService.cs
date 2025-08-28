using OrderService.API.Data;
using OrderService.API.Models;

namespace OrderService.API.Services
{
    public class OrderService: IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public Order GetOrderById(int id)
        {
            var order = _orderRepository.GetOrderById(id);
            if(order == null)
            {
                throw new ArgumentException("Order not found!");
            }
            return order;
        }

        public IEnumerable<Order> GetAllOrdersByUserId(int id)
        {
            return _orderRepository.GetAllOrdersByUserId(id);
        }

        public void AddOrder(Order order, OrderItem[] orderItems)
        {
            _orderRepository.AddOrder(order, orderItems);
        }

        public void UpdateOrder(Order updatedOrder)
        {
            var order = _orderRepository.GetOrderById(updatedOrder.Id);
            if (order == null)
            {
                throw new ArgumentException("Order not found!");
            }

            order.TotalPrice = updatedOrder.TotalPrice;
            order.Status = updatedOrder.Status;
            order.RestaurantId = updatedOrder.RestaurantId;

            _orderRepository.UpdateOrder(order);
        }

        public void DeleteOrder(int id)
        {
            var order = _orderRepository.GetOrderById(id);
            if(order == null)
            {
                throw new ArgumentException("Order not found!");
            }

            _orderRepository.DeleteOrder(id);
        }

        public IEnumerable<OrderItem> GetOrderItemsByOrderId(int orderId)
        {
            return _orderRepository.GetOrderItemsByOrderId(orderId);
        }
    }
}
