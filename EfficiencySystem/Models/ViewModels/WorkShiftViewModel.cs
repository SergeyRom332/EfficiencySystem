namespace EfficiencySystem.Models.ViewModels
{
    public class WorkShiftViewModel
    {
        public DateTime DateFirst { get; set; } = DateTime.Today;
        public DateTime DateSecond { get; set; } = DateTime.Today;
        public int RestaurantId { get; set; }
        public List<Restaurant> Restaurants { get; set; }
        public List<WorkShift>? WorkShifts { get; set; }
    }
}
