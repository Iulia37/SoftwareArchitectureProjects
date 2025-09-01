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
        public Order GetOrderById(int id, int currentUserId)
        {
            var order = _orderRepository.GetOrderById(id);
            if(order == null)
            {
                throw new ArgumentException("Order not found!");
            }
            if(order.UserId != currentUserId)
            {
                throw new ArgumentException("You are not authorized to view this order!");
            }
            return order;
        }

        public IEnumerable<Order> GetAllOrdersByUserId(int id, int currentUserId)
        {
            if(id != currentUserId)
            {
                throw new ArgumentException("You are not authorized to view these orders!");
            }
            return _orderRepository.GetAllOrdersByUserId(id);
        }

        public void AddOrder(Order order, OrderItem[] orderItems)
        {
            order.OrderDate = DateTime.Now;
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
            order.UserId = updatedOrder.UserId;
            order.OrderDate = updatedOrder.OrderDate;

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
            var items = _orderRepository.GetOrderItemsByOrderId(orderId);
            if(items == null || !items.Any())
            {
                throw new ArgumentException("No items found for this order!");
            }
            return items;
        }
    }
}
