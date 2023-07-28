using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using RestaurantAPI.Entities;
using RestaurantAPI.Model;
using System.Linq;
using AutoMapper;

namespace RestaurantAPI.Services
{
    public interface IRestaurantService
    {
        RestaurantDTO GetById(int id);
        IEnumerable<RestaurantDTO> GetAll();
        int Create(CreateRestaurantDto dto);

        bool Delete(int id);

        public bool Update(int id, UpdateRestaurantDTO dto);
    }
    public class RestaurantServices : IRestaurantService
    {
        private readonly RestaurantDbContext dbContext;
        private readonly IMapper mapper;
        public RestaurantServices(RestaurantDbContext restaurantDbContext, IMapper mapper)
        {
            dbContext = restaurantDbContext;
            this.mapper = mapper;
        }

        public bool Update(int id, UpdateRestaurantDTO dto)
        {
            var restaurant = dbContext.Restaurants.FirstOrDefault(r => r.Id == id);

            if (restaurant is null) return false;

            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;

            dbContext.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var restaurant = dbContext.Restaurants.FirstOrDefault(r => r.Id == id);

            if (restaurant is null) return false;

            dbContext.Restaurants.Remove(restaurant);
            dbContext.SaveChanges();

            return true;
        }
        public RestaurantDTO GetById(int id)
        {
            var restaurant = dbContext.Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes).FirstOrDefault(r => r.Id == id);

            if (restaurant is null) return null;
            
            var result = mapper.Map<RestaurantDTO>(restaurant);

            return result;
        }

        public IEnumerable<RestaurantDTO> GetAll()
        {
            var restaurants = dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .ToList();

            var restaurantsDto = mapper.Map<List<RestaurantDTO>>(restaurants);

            return restaurantsDto;
        }

        public int Create(CreateRestaurantDto dto)
        {
            var restaurant = mapper.Map<Restaurant>(dto);
            dbContext.Restaurants.Add(restaurant);
            dbContext.SaveChanges();

            return restaurant.Id;
        }
    }
}
