using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;
using RestaurantAPI.Model;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
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
            var id = restaurantService.Create(dto);

            return Created($"/api/restaurant/{id}", null);
        }

        [HttpGet]
        [Authorize]
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
