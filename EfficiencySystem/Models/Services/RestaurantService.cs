﻿using Microsoft.EntityFrameworkCore;

namespace EfficiencySystem.Models.Services
{
    public class RestaurantService : IRestaurantService
    {
        private MainDbContext _dbContext;

        public RestaurantService(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Restaurant>> GetRestaurantsAsync()
        {
            return await _dbContext.Restaurants.ToListAsync();
        }

        public async Task<Restaurant> GetRestaurantAsync(int id)
        {
            return await _dbContext.Restaurants.FirstOrDefaultAsync(i => i.Id == id) ?? new Restaurant();
        }

        public List<Restaurant> GetRestaurants()
        {
            return _dbContext.Restaurants.ToList();
        }
    }

    public static class RestaurantServiceProviderExtensions
    {
        public static void AddRestaurantService(this IServiceCollection services)
        {
            services.AddTransient<IRestaurantService, RestaurantService>();
        }
    }
}
