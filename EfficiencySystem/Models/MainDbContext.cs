using Microsoft.EntityFrameworkCore;

namespace EfficiencySystem.Models
{
    public class MainDbContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<WorkShift> WorkShifts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Revenue> Revenues { get; set; }
        public DbSet<WorkShiftDuration> WorkShiftDurations { get; set; }
        public DbSet<WorkShiftPayment> WorkShiftPayments { get; set; }
        public DbSet<SalaryBonus> SalaryBonuses { get; set; }
        public DbSet<SalaryAdjustment> SalaryAdjustments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Good> Goods { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderType> OrderTypes { get; set; }
        public DbSet<Position> Positions { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }
    }
}
