using AutoMapper;
using RestaurantAPI.Entities;
using RestaurantAPI.Model;

namespace RestaurantAPI
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDTO>()
                .ForMember(m => m.City, c => c.MapFrom(z => z.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(z => z.Address.Street))
                .ForMember(m => m.PostalCode, c => c.MapFrom(z => z.Address.PostalCode));

            CreateMap<Dish, DishDTO>();
        }
    }
}
