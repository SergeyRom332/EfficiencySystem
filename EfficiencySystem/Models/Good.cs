using System.ComponentModel.DataAnnotations.Schema;

namespace EfficiencySystem.Models
{
    [Table("good")]
    public class Good
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("price")]
        public decimal Price { get; set; }
    }
}
