using System.ComponentModel.DataAnnotations.Schema;

namespace EfficiencySystem.Models
{
    [Table("revenues")]
    public class Revenue
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("date")] 
        public DateTime Date { get; set; }
        [Column("restaurant_id")]
        public int RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }
        [Column("orders_count")]
        public int OrdersCount { get; set; }
        [Column("revenue")]
        public decimal RevenueSum { get; set; }
        [NotMapped]
        public decimal AverageBill => OrdersCount == 0 ? 0 : RevenueSum / OrdersCount;
    }
}
