using Microsoft.EntityFrameworkCore;
using RestaurantService.API.Data;
using RestaurantService.API.Models;
using System;

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
            var restaurant = _restaurantRepository.GetRestaurantById(id);
            if(restaurant == null)
            {
                throw new ArgumentException("Restaurant not found!");
            }
            return restaurant;
        }
        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            var restaurants = _restaurantRepository.GetAllRestaurants();
            return restaurants;
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
            restaurant.ImageUrl = updatedRestaurant.ImageUrl;

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

        public string UploadImage(IFormFile image)
        {
            var allowedExt = new[] { ".jpg", ".jpeg", ".png" };
            var ext = Path.GetExtension(image.FileName).ToLowerInvariant();

            if (!allowedExt.Contains(ext))
            {
                throw new ArgumentException("Invalid file type. Only .jpg, .jpeg, .png are allowed.");
            }

            var imagesRoot = Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "images");
            Directory.CreateDirectory(imagesRoot);

            var fileName = $"{Guid.NewGuid():N}{ext}";
            var savePath = Path.Combine(imagesRoot, fileName);

            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            var relativeUrl = $"/images/{fileName}";
            return relativeUrl;
        }
    }
}
