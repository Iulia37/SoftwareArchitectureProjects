using RestaurantService.API.Data;
using RestaurantService.API.Models;

namespace RestaurantService.API.Services
{
    public class MenuItemService: IMenuItemService
    {
        private readonly IMenuItemRepository _menuItemRepo;

        public MenuItemService(IMenuItemRepository menuItemRepo)
        {
            _menuItemRepo = menuItemRepo;
        }

        public MenuItem getMenuItemById(int id)
        {
            return _menuItemRepo.getMenuItemById(id);
        }

        public IEnumerable<MenuItem> getMenuItemsByRestaurantId(int restaurantId)
        {
            return _menuItemRepo.getMenuItemsByRestaurantId(restaurantId);
        }

        public void addMenuItem(MenuItem menuItem)
        {
            _menuItemRepo.addMenuItem(menuItem);
        }

        public void updateMenuItem(MenuItem updatedMenuItem)
        {
            var menuItem = _menuItemRepo.getMenuItemById(updatedMenuItem.Id);

            if(menuItem == null)
            {
                throw new ArgumentException("Menu item not found!");
            }

            menuItem.Name = updatedMenuItem.Name;
            menuItem.Description = updatedMenuItem.Description;
            menuItem.Price = updatedMenuItem.Price;

            _menuItemRepo.updateMenuItem(menuItem);
        }

        public void deleteMenuItem(int id)
        {
            var menuItem = _menuItemRepo.getMenuItemById(id);
            if(menuItem == null)
            {
                throw new ArgumentException("Menu item not found!");
            }
            _menuItemRepo.deleteMenuItem(id);
        }
    }
}
