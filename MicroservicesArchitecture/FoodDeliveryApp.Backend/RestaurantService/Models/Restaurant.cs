using System.ComponentModel.DataAnnotations;

namespace RestaurantService.API.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string ImageUrl { get; set; }
    }
}
