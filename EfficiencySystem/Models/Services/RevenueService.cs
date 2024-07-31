using Microsoft.EntityFrameworkCore;

namespace EfficiencySystem.Models.Services
{
    public class RevenueService : IRevenueService
    {
        private MainDbContext _dbContext;

        public RevenueService(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Revenue>> GetRevenue(int restaurantId, DateTime dateFirst, DateTime dateSecond)
        {
            var query = _dbContext.Revenues.Where(i => i.Date >= dateFirst && i.Date <= dateSecond);
            if (restaurantId != 0) query = query.Where(i => i.RestaurantId == restaurantId);

            return await query
                .Include(i => i.Restaurant)
                .ToListAsync();
        }
    }

    public static class RevenueServiceProviderExtensions
    {
        public static void AddRevenueService(this IServiceCollection services)
        {
            services.AddTransient<IRevenueService, RevenueService>();
        }
    }
}
