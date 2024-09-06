namespace EfficiencySystem.Models.Services
{
    public interface IRestaurantService
    {
        public Task<List<Restaurant>> GetRestaurantsAsync();
        public List<Restaurant> GetRestaurants();
        public Task<Restaurant> GetRestaurantAsync(int id);
    }
}
