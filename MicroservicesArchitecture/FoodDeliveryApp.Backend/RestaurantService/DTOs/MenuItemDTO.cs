using System.ComponentModel.DataAnnotations;

namespace RestaurantService.API.DTOs
{
    public class MenuItemDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must have between 2 and 50 characters!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required!")]
        [Range(0.01, 1000, ErrorMessage = "Price must be between 0.01 and 1000!")]
        public decimal Price { get; set; }

        public int RestaurantId { get; set; }
    }
}
