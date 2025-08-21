using RestaurantService.API.Models;

namespace RestaurantService.API.Data
{
    public class RestaurantRepository: IRestaurantRepository
    {
        private readonly RestaurantDbContext _context;

        public RestaurantRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public Restaurant GetRestaurantById(int id)
        {
            return _context.Restaurants.Find(id);
        }
        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            return _context.Restaurants.ToList();
        }
        public void AddRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
        }
        public void UpdateRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Update(restaurant);
            _context.SaveChanges();
        }
        public void DeleteRestaurant(int id)
        {
            var restaurant = _context.Restaurants.Find(id);
            if(restaurant != null)
            {
                _context.Restaurants.Remove(restaurant);
                _context.SaveChanges();
            }
        }
    }
}
