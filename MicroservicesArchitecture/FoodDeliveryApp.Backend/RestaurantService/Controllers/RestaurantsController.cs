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
        private readonly IWebHostEnvironment _env;

        public RestaurantsController(IRestaurantService restaurantService, IWebHostEnvironment env)
        {
            _restaurantService = restaurantService;
            _env = env;
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

        [HttpPost("image")]
        public IActionResult Upload([FromForm] FileUploadDto upload)
        {
            var file = upload.File;

            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded!");

            var allowedExt = new[] { ".jpg", ".jpeg", ".png" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExt.Contains(ext))
                return BadRequest("Invalid file type. Allowed: .jpg, .jpeg, .png");

            var imagesRoot = Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "images");
            Directory.CreateDirectory(imagesRoot);

            var fileName = $"{Guid.NewGuid():N}{ext}";
            var savePath = Path.Combine(imagesRoot, fileName);

            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var relativeUrl = $"/images/{fileName}";
            return Ok(new { imageUrl = relativeUrl });
        }
    }
}
