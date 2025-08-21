using RestaurantService.API.Models;

namespace RestaurantService.API.Data
{
    public interface IRestaurantRepository
    {
        public Restaurant GetRestaurantById(int id);
        public IEnumerable<Restaurant> GetAllRestaurants();
        public void AddRestaurant(Restaurant restaurant);
        public void UpdateRestaurant(Restaurant restaurant);
        public void DeleteRestaurant(int id);
    }
}
