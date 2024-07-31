namespace EfficiencySystem.Models.Services
{
    public interface IDepartmentService
    {
        public Task<List<Department>> GetDepartmentsAsync();
    }
}
