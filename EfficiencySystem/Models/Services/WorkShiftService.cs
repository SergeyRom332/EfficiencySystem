using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Numeric;
using System.Net.Http;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EfficiencySystem.Models.Services
{
    public class WorkShiftService : IWorkShiftService
    {
        private MainDbContext _dbContext;
        private HttpClient _httpClient;
        private WorkShiftHttpClient _workShiftHttpClient;

        public WorkShiftService(MainDbContext dbContext, HttpClient httpClient, WorkShiftHttpClient workShiftHttpClient)
        {
            _dbContext = dbContext;
            _httpClient = httpClient;
            _workShiftHttpClient = workShiftHttpClient;
        }

        public async Task<List<WorkShift>> GetWorkShiftsAsync(DateTime dateFirst, DateTime dateSecond, int restaurantId)
        {
            return await _workShiftHttpClient.GetWorkShifts(dateFirst, dateSecond, restaurantId);
        }

        public async Task<WorkShift> GetWorkShiftAsync(int id)
        {
            return await _workShiftHttpClient.GetWorkShift(id);
        }

        public async Task<List<WorkShift>> GetEmployeeWorkShifts(int employeeId, DateTime dateFirst, DateTime dateSecond)
        {
            return await _dbContext.WorkShifts
                .Where(i => i.StaffId == employeeId && i.Date >= dateFirst && i.Date <= dateSecond)
                .Include(i => i.Restaurant)
                .Include(i => i.Staff)
                .ToListAsync();
        }

        public async Task AddWorkShiftAsync(WorkShift workShift)
        {
            await _workShiftHttpClient.AddWorkShiftAsync(workShift);
        }

        public async Task UpdateWorkShiftAsync(WorkShift workShift)
        {
            _dbContext.Entry(workShift).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteWorkShiftAsync(int id)
        {
            await _dbContext.WorkShifts.Where(i => i.Id == id).ExecuteDeleteAsync();
        }

        public async Task<WorkShiftDuration> GetWorkShiftDuration(int positionId)
        {
            return await _dbContext.WorkShiftDurations
                .Where(i => i.PositionId == positionId)
                .FirstOrDefaultAsync() ?? new WorkShiftDuration();
        }

        public async Task<WorkShiftPayment> GetWorkShiftPayments(int positionId)
        {
            return await _dbContext.WorkShiftPayments
                .Where(i => i.PositionId == positionId)
                .FirstOrDefaultAsync() ?? new WorkShiftPayment();
        }
    }

    public static class WorkShiftServiceProviderExtensions
    {
        public static void AddWorkShiftService(this IServiceCollection services)
        {
            services.AddTransient<IWorkShiftService, WorkShiftService>();
        }
    }
}
