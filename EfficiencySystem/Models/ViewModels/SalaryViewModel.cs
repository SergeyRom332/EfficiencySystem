namespace EfficiencySystem.Models.ViewModels
{
    public class SalaryViewModel
    {
        public DateTime DateFirst { get; set; } = DateTime.Today;
        public DateTime DateSecond { get; set; } = DateTime.Today;
        public List<Salary>? Salaries { get; set; }
    }
}
