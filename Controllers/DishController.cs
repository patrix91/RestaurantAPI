using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Model;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant/{restaurantId}/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }
        [HttpPost]
        public ActionResult Post([FromRoute]int restaurantId, CreateDishDto dto)
        {
            var newDishId = _dishService.Create(restaurantId, dto);

            return Created($"api/{restaurantId}/dish/{newDishId}", null);
        }
        [HttpGet("{dishId}")]
        public ActionResult Get([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            DishDTO dish = _dishService.GetById(restaurantId, dishId);

            return Ok(dish);
        }

        [HttpGet]
        public ActionResult<List<DishDTO>> Get([FromRoute] int restaurantId)
        {
            var result = _dishService.GetAll(restaurantId);

            return Ok(result);
        }

        [HttpDelete]
        public ActionResult Delete([FromRoute] int restaurantId)
        {
            _dishService.RemoveAll(restaurantId);
            return NoContent();
        }
    }

}
