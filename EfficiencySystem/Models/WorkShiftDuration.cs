using System.ComponentModel.DataAnnotations.Schema;

namespace EfficiencySystem.Models
{
    [Table("workshift_durations")]
    public class WorkShiftDuration
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("position_id")]
        public int PositionId {  get; set; }
        public Position? Position { get; set; }
        [Column("duration")]
        public int MinutesDuration { get; set; }
    }
}
