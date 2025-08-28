using OrderService.API.Models;

namespace OrderService.API.Data
{
    public interface IOrderRepository
    {
        public Order GetOrderById(int id);

        public IEnumerable<Order> GetAllOrdersByUserId(int id);

        public void AddOrder(Order order, OrderItem[] orderItems);

        public void UpdateOrder(Order order);

        public void DeleteOrder(int id);

        public IEnumerable<OrderItem> GetOrderItemsByOrderId(int orderId);
    }
}
