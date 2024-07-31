namespace EfficiencySystem.Models.Services
{
    public interface IRevenueService
    {
        public Task<List<Revenue>> GetRevenue(int restaurantId, DateTime dateFirst, DateTime dateSecond);
    }
}
