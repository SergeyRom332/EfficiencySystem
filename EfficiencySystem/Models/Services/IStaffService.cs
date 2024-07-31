namespace EfficiencySystem.Models.Services
{
    public interface IStaffService
    {
        public Task<List<Staff>> GetAllStaffAsync(int departmentId);
        public Task<Staff> GetStaffAsync(int id);
        public Task AddStaffAsync(Staff staff);
        public Task UpdateStaffAsync(Staff staff);
        public Task DeleteStaffAsync(int id);
    }
}
