using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;
using System.Linq;
using RestaurantAPI.Model;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantDbContext dbContext;
        public RestaurantController(RestaurantDbContext restaurantDbContext)
        {
            dbContext = restaurantDbContext;
        }
        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDTO>> GetAll()
        {
            var restaurants = dbContext
                .Restaurants
                .ToList();

            var restaurantsDtos = restaurants.Select(r => new RestaurantDTO()
            {
                Name = r.Name,
                Category = r.Category,
                City = r.Address.City
            });

            return Ok(restaurantsDtos);
        }
        [HttpGet("{id}")]
        public ActionResult<Restaurant> Get([FromRoute] int id)
        {
            var restaurant = dbContext.Restaurants.FirstOrDefault(r => r.Id == id);

            if (restaurant is null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }
    }
}
