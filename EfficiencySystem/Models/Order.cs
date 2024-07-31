using System.ComponentModel.DataAnnotations.Schema;

namespace EfficiencySystem.Models
{
    [Table("orders")]
    public class Order
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("date")]
        public DateTime Date { get; set; }
        [Column("restaurant_id")]
        public int RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }
        [Column("type_id")]
        public int TypeId { get; set; }
        public OrderType? Type { get; set; }
        [Column("phone")]
        public string? Phone { get; set; }
        [Column("email")]
        public string? Email { get; set; }
        [Column("sum")]
        public decimal Sum { get; set; }
        [Column("discount")]
        public decimal? Discount { get; set; }
        [Column("time_pay")]
        public DateTime TimePay { get; set; }
        [Column("time_rejected")]
        public DateTime TimeRejected { get; set; }
        public List<OrderItem>? Items { get; set;}
    }
}
