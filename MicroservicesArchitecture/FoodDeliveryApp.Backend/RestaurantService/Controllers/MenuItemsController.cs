using Microsoft.AspNetCore.Mvc;
using RestaurantService.API.Models;
using RestaurantService.API.Services;
using System.Security.Claims;

namespace RestaurantService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemsController : Controller
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemsController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        [HttpGet("{restaurantId}")]
        public ActionResult GetMenuItemsByRestaurantId(int restaurantId)
        {
            var menuItems = _menuItemService.getMenuItemsByRestaurantId(restaurantId);
            return Ok(menuItems);
        }

        [HttpPost]
        public IActionResult AddMenuItem([FromBody] MenuItem newItem)
        {
            try
            {
                _menuItemService.addMenuItem(newItem);
                return CreatedAtAction(nameof(GetMenuItemsByRestaurantId), 
                                       new { restaurantId = newItem.RestaurantId }, newItem);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("edit/{id}")]
        public IActionResult EditMenuItem([FromBody] MenuItem updatedItem)
        {
            try
            {
                _menuItemService.updateMenuItem(updatedItem);
                return Ok("Menu item updated successfully!");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMenuItem(int id)
        {
            var menuItem = _menuItemService.getMenuItemById(id);
            if (menuItem == null)
                return BadRequest("Not found!");

            _menuItemService.deleteMenuItem(id);
            return NoContent();
        }
    }
}
