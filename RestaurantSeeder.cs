using System.Collections.Generic;
using RestaurantAPI.Entities;
using System.Linq;

namespace RestaurantAPI
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext dbContext;
        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Seed()
        {
            if (dbContext.Database.CanConnect())
            {
                if (!dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    dbContext.Restaurants.AddRange(restaurants);
                    dbContext.SaveChanges();
                }

            }

        }
        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "KFC (short for Kentucky Fried Checken) is an American fast food restaurant chain headquartered ...",
                    ContactEmail = "contact@kfc.com",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Nashville Hot Chicken",
                            Price = 10.30M,

                        },

                        new Dish()
                        {
                            Name = "Chicken Nuggets",
                            Price = 5.30M,
                        }
                    },
                    Address = new Address()
                    {
                        City = "Kraków",
                        Street = "Długa 5",
                        PostalCode = "30-001"
                    }
                },

                new Restaurant()
                {
                    Name = "McDonald",
                    Description = "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises..",
                    Category = "Fast Food",
                    HasDelivery = true,
                    ContactEmail = "contact@mcdonald.com",
                    Address = new Address()
                    {
                        City = "Kraków",
                        Street = "Szewska 2",
                        PostalCode = "30-001"
                    },

                    Dishes = new List<Dish>()
                    {
                        new Dish
                        {
                            Name = "Cheesburger",
                            Description = "Burger with slice of meet and cheese inside with peakles and onion",
                            Price = 0.99M
                        },
                        new Dish
                        {
                            Name = "French Fries",
                            Price = 3.99M
                        }
                    }

                }
            };

            return restaurants;
        }
    }
}
