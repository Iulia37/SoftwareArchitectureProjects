using OrderService.API.Models;

namespace OrderService.API.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
