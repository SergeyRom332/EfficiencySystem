namespace EfficiencySystem.Models.ViewModels
{
    public class RevenueViewModel
    {
        public DateTime DateFirst { get; set; } = DateTime.Today;
        public DateTime DateSecond { get; set; } = DateTime.Today;
        public int RestaurantId { get; set; }
        public List<Revenue>? Revenues { get; set; }
    }
}
