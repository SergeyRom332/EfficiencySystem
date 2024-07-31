using Microsoft.EntityFrameworkCore;

namespace EfficiencySystem.Models.Services
{
    public class DepartmentService : IDepartmentService
    {
        private MainDbContext _dbContext;

        public DepartmentService(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            return await _dbContext.Departments.ToListAsync();
        }
    }

    public static class DepartmentServiceProviderExtensions
    {
        public static void AddDepartmentService(this IServiceCollection services)
        {
            services.AddTransient<IDepartmentService, DepartmentService>();
        }
    }
}
