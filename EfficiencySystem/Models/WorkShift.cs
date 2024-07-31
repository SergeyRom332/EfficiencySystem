using System.ComponentModel.DataAnnotations.Schema;

namespace EfficiencySystem.Models
{
    [Table("work_shift")]
    public class WorkShift
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("restaurant_id")]
        public int RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }
        [Column("date")]
        public DateTime Date { get; set; }
        [Column("staff_id")]
        public int StaffId {  get; set; }
        public Staff? Staff {  get; set; }
        [Column("time_start")]
        public DateTime TimeStart { get; set; }
        [Column("time_finish")]
        public DateTime TimeFinish { get; set; }
    }
}
