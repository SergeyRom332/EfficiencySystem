using System.ComponentModel.DataAnnotations.Schema;

namespace EfficiencySystem.Models
{
    [Table("order_items")]
    public class OrderItem
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("order_id")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        [Column("good_id")]
        public int GoodId {  get; set; }
        public Good Good { get; set; }
    }
}
