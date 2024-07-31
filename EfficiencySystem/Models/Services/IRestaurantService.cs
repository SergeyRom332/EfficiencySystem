namespace EfficiencySystem.Models.Services
{
    public interface IRestaurantService
    {
        public Task<List<Restaurant>> GetRestaurantAsync();
        public Task<Restaurant> GetRestaurantAsync(int id);
    }
}
