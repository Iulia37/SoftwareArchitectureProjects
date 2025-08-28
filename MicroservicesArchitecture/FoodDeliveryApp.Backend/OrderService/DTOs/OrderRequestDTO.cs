using OrderService.API.DTOs;

namespace OrderService.API.DTOs
{
    public class OrderRequestDTO
    {
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
