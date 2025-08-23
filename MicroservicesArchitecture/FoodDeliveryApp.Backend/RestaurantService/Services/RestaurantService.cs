using Microsoft.EntityFrameworkCore;
using RestaurantService.API.Data;
using RestaurantService.API.Models;

namespace RestaurantService.API.Services
{
    public class RestaurantService: IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IWebHostEnvironment _env;

        public RestaurantService(IRestaurantRepository restaurantRepository, IWebHostEnvironment env)
        {
            _restaurantRepository = restaurantRepository;
            _env = env;
        }
        public Restaurant GetRestaurantById(int id)
        {
            return _restaurantRepository.GetRestaurantById(id);
        }
        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            return _restaurantRepository.GetAllRestaurants();
        }
        public void AddRestaurant(Restaurant restaurant)
        {
            _restaurantRepository.AddRestaurant(restaurant);
        }
        public void UpdateRestaurant(Restaurant updatedRestaurant)
        {
            var restaurant = _restaurantRepository.GetRestaurantById(updatedRestaurant.Id);
            if (restaurant == null)
            {
                throw new ArgumentException("Restaurant not found!");
            }

            restaurant.Name = updatedRestaurant.Name;
            restaurant.Address = updatedRestaurant.Address;
            restaurant.Email = updatedRestaurant.Email;
            restaurant.PhoneNumber = updatedRestaurant.PhoneNumber;
            
            _restaurantRepository.UpdateRestaurant(restaurant);
        }
        public void DeleteRestaurant(int id)
        {
            var restaurant = _restaurantRepository.GetRestaurantById(id);
            if (restaurant == null)
            {
                throw new ArgumentException("Restaurant not found!");
            }
            _restaurantRepository.DeleteRestaurant(id);
        }

        public string UploadImage(int restaurantId, IFormFile image)
        {
            var restaurant = _restaurantRepository.GetRestaurantById(restaurantId);
            if (restaurant == null)
                throw new Exception("Restaurant not found!");

            var uploads = Path.Combine(_env.WebRootPath, "images");
            Directory.CreateDirectory(uploads);

            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(image.FileName)}";
            var filePath = Path.Combine(uploads, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }
            restaurant.ImageUrl = $"/images/{fileName}";
            _restaurantRepository.UpdateRestaurant(restaurant);

            return restaurant.ImageUrl;
        }
    }
}
