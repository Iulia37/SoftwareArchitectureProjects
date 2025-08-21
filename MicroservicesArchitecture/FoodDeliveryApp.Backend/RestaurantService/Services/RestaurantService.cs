using RestaurantService.API.Data;
using RestaurantService.API.Models;

namespace RestaurantService.API.Services
{
    public class RestaurantService: IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
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
    }
}
