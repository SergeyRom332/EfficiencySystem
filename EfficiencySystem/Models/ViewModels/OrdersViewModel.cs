using System.ComponentModel.DataAnnotations;

namespace EfficiencySystem.Models.ViewModels
{
    public class OrdersViewModel
    {
        public int RestaurantId { get; set; }
        public List<Restaurant> Restaurants { get; set; }
        public DateTime DateFirst { get; set; } = DateTime.Today;
        public DateTime DateSecond { get; set; } = DateTime.Today;
        public List<Order>? Orders { get; set; }
    }
}
