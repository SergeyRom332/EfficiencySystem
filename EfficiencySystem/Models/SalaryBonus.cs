using System.ComponentModel.DataAnnotations.Schema;

namespace EfficiencySystem.Models
{
    [Table("salary_bonus")]
    public class SalaryBonus
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("staff_id")]
        public int StaffId { get; set; }
        public Staff Staff { get; set; }
        [Column("bonus_sum")]
        public decimal BonusSum { get; set; }
        [Column("date")]
        public DateTime Date { get; set; }
    }
}
