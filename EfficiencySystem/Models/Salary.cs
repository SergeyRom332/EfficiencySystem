namespace EfficiencySystem.Models
{
    public class Salary
    {
        public int StaffId { get; set; }
        public Staff? Staff { get; set; }
        public int ShiftsCount { get; set; }
        public decimal SalarySum { get; set; }
        public decimal BonusSum { get; set; }
        public decimal AdjustmentSum { get; set; }
        public decimal FinalSum => SalarySum + BonusSum - AdjustmentSum;
    }
}
