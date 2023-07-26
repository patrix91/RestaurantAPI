using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;
using System.Linq;
using RestaurantAPI.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService restaurantService;
        public RestaurantController(IRestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
            
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            var isDeleted = restaurantService.Delete(id);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var id = restaurantService.Create(dto);

            return Created($"/api/restaurant/{id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDTO>> GetAll()
        {
            var restaurantsDto = restaurantService.GetAll();

            return Ok(restaurantsDto);
        }
        [HttpGet("{id}")]
        public ActionResult<Restaurant> Get([FromRoute] int id)
        {
            var restaurant = restaurantService.GetById(id);

            if (restaurant is null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }
    }
}
