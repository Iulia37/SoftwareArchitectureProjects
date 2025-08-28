using System.ComponentModel.DataAnnotations;

namespace OrderService.API.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int RestaurantId { get; set; }

        [Range(0.01, 100000, ErrorMessage = "Total price must be greater than 0!")]
        public decimal TotalPrice { get; set; }

        public string Status { get; set; }
    }
}
