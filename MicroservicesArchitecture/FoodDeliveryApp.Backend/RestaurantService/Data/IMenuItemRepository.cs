using RestaurantService.API.Models;

namespace RestaurantService.API.Data
{
    public interface IMenuItemRepository
    {
        public MenuItem getMenuItemById(int id);
        public IEnumerable<MenuItem> getMenuItemsByRestaurantId(int restaurantId);

        public void addMenuItem(MenuItem menuItem);

        public void updateMenuItem(MenuItem menuItem);

        public void deleteMenuItem(int id);
    }
}
