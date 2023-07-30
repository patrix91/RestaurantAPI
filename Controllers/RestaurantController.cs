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

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateRestaurantDTO dto, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            restaurantService.Update(id, dto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            restaurantService.Delete(id);
            return NoContent();
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

            return Ok(restaurant);
        }
    }
}
