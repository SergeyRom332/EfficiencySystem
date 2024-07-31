using Microsoft.EntityFrameworkCore;

namespace EfficiencySystem.Models.Services
{
    public class OrdersService : IOrdersService
    {
        private MainDbContext _dbContext;

        public OrdersService(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Order>> GetOrdersAsync(int restaurantId, DateTime dateFirst, DateTime dateSecond)
        {
            var query = _dbContext.Orders.Where(i => i.Date >= dateFirst && i.Date <= dateSecond);
            if (restaurantId != 0) query = query.Where(i => i.RestaurantId == restaurantId);

            return await query
                .Include(i => i.Restaurant)
                .Include(i => i.Type)
                .Include(i => i.Items)
                .ToListAsync();
        }

        public async Task<Order> GetOrderAsync(int id)
        {
            return await _dbContext.Orders
                .Where(i => i.Id == id)
                .Include(i => i.Restaurant)
                .Include(i => i.Type)
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == id) ?? new Order();
        }

        public async Task AddOrderAsync(Order order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _dbContext.Entry(order).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            await _dbContext.Orders.Where(i => i.Id == id).ExecuteDeleteAsync();
        }
    }

    public static class OrdersServiceProviderExtensions
    {
        public static void AddOrdersService(this IServiceCollection services)
        {
            services.AddTransient<IOrdersService, OrdersService>();
        }
    }
}
