using System.ComponentModel.DataAnnotations.Schema;

namespace EfficiencySystem.Models
{
    [Table("salary_adjustment")]
    public class SalaryAdjustment
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("staff_id")]
        public int StaffId { get; set; }
        public Staff Staff { get; set; }
        [Column("adjustment_sum")]
        public decimal AdjustmentSum { get; set; }
        [Column("date")]
        public DateTime Date { get; set; }
    }
}
