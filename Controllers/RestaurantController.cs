using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;
using System.Linq;
using RestaurantAPI.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantDbContext dbContext;
        private readonly IMapper mapper;
        public RestaurantController(RestaurantDbContext restaurantDbContext, IMapper mapper)
        {
            dbContext = restaurantDbContext;
            this.mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDTO>> GetAll()
        {
            var restaurants = dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .ToList();

            var restaurantsDtos = mapper.Map<List<RestaurantDTO>>(restaurants);

            return Ok(restaurantsDtos);
        }
        [HttpGet("{id}")]
        public ActionResult<Restaurant> Get([FromRoute] int id)
        {
            var restaurant = dbContext.Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes).FirstOrDefault(r => r.Id == id);

            if (restaurant is null)
            {
                return NotFound();
            }

            var restaurantDto = mapper.Map<RestaurantDTO>(restaurant);

            return Ok(restaurantDto);
        }
    }
}
