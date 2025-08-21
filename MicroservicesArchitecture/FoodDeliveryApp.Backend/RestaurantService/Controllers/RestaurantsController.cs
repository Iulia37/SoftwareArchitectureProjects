using Microsoft.AspNetCore.Mvc;
using RestaurantService.API.Models;
using RestaurantService.API.DTOs;
using RestaurantService.API.Services;
using Nelibur.ObjectMapper;

namespace RestaurantService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantsController : Controller
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantsController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public ActionResult GetAllRestaurants()
        {
            var restaurants = _restaurantService.GetAllRestaurants();
            if (restaurants == null || !restaurants.Any())
            {
                return BadRequest("No restaurants found!");
            }
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public ActionResult GetRestaurantById(int id)
        {
            var restaurant = _restaurantService.GetRestaurantById(id);
            if (restaurant == null)
            {
                return BadRequest("Restaurant not found!");
            }
            return Ok(restaurant);
        }

        [HttpPost]
        public ActionResult AddRestaurant([FromBody] RestaurantDTO restaurant)
        {
            try
            {
                _restaurantService.AddRestaurant(TinyMapper.Map<Restaurant>(restaurant));
                return CreatedAtAction(nameof(GetRestaurantById), new { id = restaurant.Id }, restaurant);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("edit/{id}")]
        public ActionResult UpdateRestaurant([FromBody] RestaurantDTO updatedRestaurant)
        {
            try
            {
                _restaurantService.UpdateRestaurant(TinyMapper.Map<Restaurant>(updatedRestaurant));
                return Ok("Restaurant updated successfully!");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteRestaurant(int id)
        {
            try
            {
                _restaurantService.DeleteRestaurant(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
