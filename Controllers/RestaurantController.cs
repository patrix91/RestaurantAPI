using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Services;
using RestaurantAPI.Entities;
using RestaurantAPI.Model;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    [Authorize]
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
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
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
        [AllowAnonymous]
        public ActionResult<Restaurant> Get([FromRoute] int id)
        {
            var restaurant = restaurantService.GetById(id);

            return Ok(restaurant);
        }
    }
}
