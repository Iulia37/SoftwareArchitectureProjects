using Microsoft.AspNetCore.Mvc;
using Nelibur.ObjectMapper;
using RestaurantService.API.Models;
using RestaurantService.API.Services;
using RestaurantService.API.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace RestaurantService.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemsController : Controller
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemsController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult GetMenuItemById(int id)
        {
            try
            {
                var menuItem = _menuItemService.getMenuItemById(id);
                return Ok(menuItem);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("restaurant/{restaurantId}")]
        public ActionResult GetMenuItemsByRestaurantId(int restaurantId)
        {
            var menuItems = _menuItemService.getMenuItemsByRestaurantId(restaurantId);
            return Ok(menuItems);
        }

        [HttpPost]
        public IActionResult AddMenuItem([FromBody] MenuItemDTO newItem)
        {
            try
            {
                var menuItems = TinyMapper.Map<MenuItem>(newItem);
                _menuItemService.addMenuItem(menuItems);
                return CreatedAtAction(nameof(GetMenuItemsByRestaurantId), 
                                       new { restaurantId = menuItems.RestaurantId }, menuItems);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditMenuItem([FromBody] MenuItemDTO updatedItem)
        {
            try
            {
                var menuItem = TinyMapper.Map<MenuItem>(updatedItem);
                _menuItemService.updateMenuItem(menuItem);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMenuItem(int id)
        {
            try
            {
                _menuItemService.deleteMenuItem(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
