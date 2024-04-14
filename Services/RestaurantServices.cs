using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantAPI.Authorization;
using System.Collections.Generic;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Entities;
using System.Security.Claims;
using RestaurantAPI.Model;
using System.Linq;
using AutoMapper;

namespace RestaurantAPI.Services
{
    public interface IRestaurantService
    {
        RestaurantDTO GetById(int id);
        IEnumerable<RestaurantDTO> GetAll();
        int Create(CreateRestaurantDto dto, int userId);

        void Delete(int id, ClaimsPrincipal user);

        void Update(int id, UpdateRestaurantDTO dto, ClaimsPrincipal user);
    }
    public class RestaurantServices : IRestaurantService
    {
        private readonly RestaurantDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<RestaurantServices> logger;
        private readonly IAuthorizationService authorizationService;

        public RestaurantServices(RestaurantDbContext restaurantDbContext, IMapper mapper, ILogger<RestaurantServices> logger,
             IAuthorizationService authorizationService)
        {
            dbContext = restaurantDbContext;
            this.mapper = mapper;
            this.logger = logger;
            this.authorizationService = authorizationService;
        }

        public void Update(int id, UpdateRestaurantDTO dto, ClaimsPrincipal user)
        {
            
            var restaurant = dbContext.Restaurants.FirstOrDefault(r => r.Id == id);

            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");

            var authorizationResult = authorizationService.AuthorizeAsync(user, restaurant, 
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;

            dbContext.SaveChanges();
        }

        public void Delete(int id, ClaimsPrincipal user)
        {
            //logger.LogWarning($"Restaurant with id: {id} DELETE action invoked.");
            logger.LogError($"Restaurant with id: {id} DELETE action invoked.");
            var restaurant = dbContext.Restaurants.FirstOrDefault(r => r.Id == id);

            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");

            var authorizationResult = authorizationService.AuthorizeAsync(user, restaurant,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            dbContext.Restaurants.Remove(restaurant);
            dbContext.SaveChanges();
        }
        public RestaurantDTO GetById(int id)
        {
            var restaurant = dbContext.Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes).FirstOrDefault(r => r.Id == id);

            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");

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

        public int Create(CreateRestaurantDto dto, int userId)
        {
            var restaurant = mapper.Map<Restaurant>(dto);
            restaurant.CreatedById = userId;
            dbContext.Restaurants.Add(restaurant);
            dbContext.SaveChanges();

            return restaurant.Id;
        }
    }
}
