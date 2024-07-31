using System.ComponentModel.DataAnnotations.Schema;

namespace EfficiencySystem.Models
{
    [Table("staff")]
    public class Staff
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("phone")]
        public string Phone { get; set; }
        [Column("department_id")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        [Column("position_id")]
        public int PositionId { get; set; }
        public Position? Position { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        [Column("deleted")]
        public bool IsDeleted { get; set; }
        public List<WorkShift>? WorkShifts { get; set; }
    }
}
