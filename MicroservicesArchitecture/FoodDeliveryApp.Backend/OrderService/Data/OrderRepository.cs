using OrderService.API.Models;

namespace OrderService.API.Data
{
    public class OrderRepository: IOrderRepository
    {
        private readonly OrderDbContext _context;

        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders.Find(id);
        }

        public IEnumerable<Order> GetAllOrdersByUserId(int id)
        {
            return _context.Orders.Where(o => o.UserId == id);
        }

        public void AddOrder(Order order, OrderItem[] orderItems)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            foreach (var oi in orderItems)
            {
                oi.OrderId = order.Id;
                _context.OrderItems.Add(oi);
            }
            _context.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public void DeleteOrder(int id)
        {
            var order = _context.Orders.Find(id);
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        public IEnumerable<OrderItem> GetOrderItemsByOrderId(int orderId)
        {
            return _context.OrderItems.Where(oi => oi.OrderId == orderId);
        }
    }
}
