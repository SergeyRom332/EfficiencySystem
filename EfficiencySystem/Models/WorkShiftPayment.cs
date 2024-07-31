using System.ComponentModel.DataAnnotations.Schema;

namespace EfficiencySystem.Models
{
    [Table("workshift_payment")]
    public class WorkShiftPayment
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("position_id")]
        public int PositionId { get; set; }
        public Position Position { get; set; }
        [Column("payment")]
        public decimal Payment { get; set; }
    }
}
