namespace EfficiencySystem.Models.Services
{
    public interface IOrdersService
    {
        public Task<List<Order>> GetOrdersAsync(int restaurantId, DateTime dateFirst, DateTime dateSecond);
        public Task<Order> GetOrderAsync(int id);
        public Task AddOrderAsync(Order order);
        public Task UpdateOrderAsync(Order order);
        public Task DeleteOrderAsync(int id);
    }
}
