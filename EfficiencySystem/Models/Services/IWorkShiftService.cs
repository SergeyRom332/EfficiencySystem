namespace EfficiencySystem.Models.Services
{
    public interface IWorkShiftService
    {
        public Task<List<WorkShift>> GetWorkShiftsAsync(DateTime dateFirst, DateTime dateSecond, int restaurantId);
        public Task<WorkShift> GetWorkShiftAsync(int id);
        public Task<List<WorkShift>> GetEmployeeWorkShifts(int employeeId, DateTime dateFirst, DateTime dateSecond);
        public Task AddWorkShiftAsync(WorkShift workShift);
        public Task UpdateWorkShiftAsync(WorkShift workShift);
        public Task DeleteWorkShiftAsync(int id);
        public Task<WorkShiftDuration> GetWorkShiftDuration(int positionId);
        public Task<WorkShiftPayment> GetWorkShiftPayments(int positionId);
    }
}
