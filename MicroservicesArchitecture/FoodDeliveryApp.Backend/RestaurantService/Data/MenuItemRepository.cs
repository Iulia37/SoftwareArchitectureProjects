using RestaurantService.API.Models;

namespace RestaurantService.API.Data
{
    public class MenuItemRepository: IMenuItemRepository
    {
        private readonly RestaurantDbContext _context;

        public MenuItemRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public MenuItem getMenuItemById(int id)
        {
            return _context.MenuItems.Find(id);
        }

        public IEnumerable<MenuItem> getMenuItemsByRestaurantId(int restaurantId)
        {
            return _context.MenuItems.Where(m => m.RestaurantId == restaurantId);
        }

        public void addMenuItem(MenuItem menuItem)
        {
            _context.MenuItems.Add(menuItem);
            _context.SaveChanges();
        }

        public void updateMenuItem(MenuItem menuItem)
        {
            _context.MenuItems.Update(menuItem);
            _context.SaveChanges();
        }

        public void deleteMenuItem(int id)
        {
            var menuItem = _context.MenuItems.Find(id);
            if (menuItem != null)
            {
                _context.MenuItems.Remove(menuItem);
                _context.SaveChanges();
            }
        }
    }
}
