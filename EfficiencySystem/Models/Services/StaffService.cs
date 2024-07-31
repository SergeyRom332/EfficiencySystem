using Microsoft.EntityFrameworkCore;

namespace EfficiencySystem.Models.Services
{
    public class StaffService : IStaffService
    {
        private MainDbContext _dbContext;

        public StaffService(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Staff>> GetAllStaffAsync(int departmentId)
        {
            var query = _dbContext.Staff.Where(i=>!i.IsDeleted);
            if (departmentId != 0) query = query.Where(i => i.DepartmentId == departmentId);

            return await query
                .Include(i => i.Department)
                .Include(i => i.Position)
                .ToListAsync();
        }

        public async Task<Staff> GetStaffAsync(int id)
        {
            return await _dbContext.Staff
                .Where(i=>i.Id == id)
                .Include(i => i.Department)
                .Include(i => i.Position)
                .FirstOrDefaultAsync(i => i.Id == id) ?? new Staff();
        }

        public async Task AddStaffAsync(Staff staff)
        {
            _dbContext.Staff.Add(staff);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateStaffAsync(Staff staff)
        {
            _dbContext.Entry(staff).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteStaffAsync(int id)
        {
            await _dbContext.Staff.Where(i => i.Id == id).ExecuteDeleteAsync();
        }
    }

    public static class StaffServiceProviderExtensions
    {
        public static void AddStaffService(this IServiceCollection services)
        {
            services.AddTransient<IStaffService, StaffService>();
        }
    }
}

