using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nelibur.ObjectMapper;
using RestaurantService.API.DTOs;
using RestaurantService.API.Models;
using RestaurantService.API.Services;

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
            if(!restaurants.Any())
            {
                return BadRequest("No restaurants found!");
            }
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public ActionResult GetRestaurantById(int id)
        {
            try
            {
                var restaurant = _restaurantService.GetRestaurantById(id);
                return Ok(restaurant);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult AddRestaurant([FromBody] RestaurantDTO restaurantDto)
        {
            try
            {
                var restaurant = TinyMapper.Map<Restaurant>(restaurantDto);
                _restaurantService.AddRestaurant(restaurant);

                return CreatedAtAction(nameof(GetRestaurantById),
                    new { id = restaurant.Id },
                    restaurant);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateRestaurant([FromBody] RestaurantDTO updatedRestaurant)
        {
            try
            {
                _restaurantService.UpdateRestaurant(TinyMapper.Map<Restaurant>(updatedRestaurant));
                return Ok();
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

        [HttpPost("image")]
        public IActionResult Upload([FromForm] FileUploadDto upload)
        {

            if (upload.File == null || upload.File.Length == 0)
                return BadRequest("No file uploaded!");

            try
            {
                return Ok(new { imageUrl = _restaurantService.UploadImage(upload.File) });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
