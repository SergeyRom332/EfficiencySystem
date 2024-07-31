using Microsoft.EntityFrameworkCore;

namespace EfficiencySystem.Models.Services
{
    public class SalaryService : ISalaryService
    {
        private MainDbContext _dbContext;
        private IWorkShiftService _workShiftService;
        private IStaffService _staffService;
        private ExcelService _excelService;

        public SalaryService(MainDbContext dbContext, IWorkShiftService workShiftService, IStaffService staffService, ExcelService excelService)
        {
            _dbContext = dbContext;
            _workShiftService = workShiftService;
            _staffService = staffService;
            _excelService = excelService;
        }

        public async Task<List<Salary>> CalculateCashiersSalaries(DateTime dateFirst, DateTime dateSecond)
        {
            var result = new List<Salary>();
            var staff = await _staffService.GetAllStaffAsync((int)Departments.Cashier);

            foreach(var stf in staff)
            {
                var salary = new Salary()
                {
                    StaffId = stf.Id,
                    Staff = stf
                };

                stf.WorkShifts = await _workShiftService.GetEmployeeWorkShifts((int)Departments.Cashier, dateFirst, dateSecond);
                salary.ShiftsCount = stf.WorkShifts.Count();

                var ShiftDuration = await _workShiftService.GetWorkShiftDuration(stf.PositionId);
                if (ShiftDuration == null) throw new Exception("Shift duration wasn't set. Set it and try again. It needs to calcalate a salary");

                var ShiftPayment = await _workShiftService.GetWorkShiftPayments(stf.PositionId);
                if (ShiftPayment == null) throw new Exception("Shift payment wasn't set. Set it and try again. It needs to calcalate a salary");

                foreach (var workShift in stf.WorkShifts)
                {
                    //Find salary per minute
                    decimal MinutePay = ShiftPayment.Payment / ShiftDuration.MinutesDuration;
                    //Find the salary for the shift - multiply the salary per minute by the count of working minutes in the workshift (staff can work less or more then standart shift)
                    decimal ShiftSalary = MinutePay * Convert.ToDecimal(workShift.TimeFinish.Subtract(workShift.TimeStart).TotalMinutes);

                    salary.SalarySum += ShiftSalary;
                }

                //Bonuses
                salary.BonusSum += await GetSalaryBonusSum(stf.Id, dateFirst, dateSecond);
                //Adjustments
                salary.AdjustmentSum += await GetSalaryAdjustmentSum(stf.Id, dateFirst, dateSecond);

                result.Add(salary);
            }

            return result;
        }

        public async Task<List<Salary>> CalculateCooksSalaries(DateTime dateFirst, DateTime dateSecond)
        {
            var result = new List<Salary>();
            var staff = await _staffService.GetAllStaffAsync((int)Departments.Cook);

            foreach (var stf in staff)
            {
                var salary = new Salary()
                {
                    StaffId = stf.Id,
                    Staff = stf
                };

                stf.WorkShifts = await _workShiftService.GetEmployeeWorkShifts((int)Departments.Cook, dateFirst, dateSecond);
                salary.ShiftsCount = stf.WorkShifts.Count();

                var ShiftDuration = await _workShiftService.GetWorkShiftDuration(stf.PositionId);
                var ShiftPayment = await _workShiftService.GetWorkShiftPayments(stf.PositionId);

                foreach (var workShift in stf.WorkShifts)
                {
                    //Find salary per minute
                    decimal MinutePay = ShiftPayment.Payment / ShiftDuration.MinutesDuration;
                    //Find the salary for the shift - multiply the salary per minute by the count of working minutes in the workshift (staff can work less or more then standart shift)
                    decimal ShiftSalary = MinutePay * Convert.ToDecimal(workShift.TimeFinish.Subtract(workShift.TimeStart).TotalMinutes);

                    salary.SalarySum += ShiftSalary;
                }

                //Bonuses
                salary.BonusSum += await GetSalaryBonusSum(stf.Id, dateFirst, dateSecond);
                //Adjustments
                salary.AdjustmentSum += await GetSalaryAdjustmentSum(stf.Id, dateFirst, dateSecond);

                result.Add(salary);
            }

            return result;
        }

        public async Task<List<Salary>> CalculateCouriersSalaries(DateTime dateFirst, DateTime dateSecond)
        {
            var result = new List<Salary>();
            var staff = await _staffService.GetAllStaffAsync((int)Departments.Courier);

            foreach (var stf in staff)
            {
                var salary = new Salary()
                {
                    StaffId = stf.Id,
                    Staff = stf
                };

                stf.WorkShifts = await _workShiftService.GetEmployeeWorkShifts((int)Departments.Courier, dateFirst, dateSecond);
                salary.ShiftsCount = stf.WorkShifts.Count();

                var ShiftDuration = await _workShiftService.GetWorkShiftDuration(stf.PositionId);
                var ShiftPayment = await _workShiftService.GetWorkShiftPayments(stf.PositionId);

                foreach (var workShift in stf.WorkShifts)
                {
                    //Find salary per minute
                    decimal MinutePay = ShiftPayment.Payment / ShiftDuration.MinutesDuration;
                    //Find the salary for the shift - multiply the salary per minute by the count of working minutes in the workshift (staff can work less or more then standart shift)
                    decimal ShiftSalary = MinutePay * Convert.ToDecimal(workShift.TimeFinish.Subtract(workShift.TimeStart).TotalMinutes);

                    salary.SalarySum += ShiftSalary;
                }

                //Bonuses
                salary.BonusSum += await GetSalaryBonusSum(stf.Id, dateFirst, dateSecond);
                //Adjustments
                salary.AdjustmentSum += await GetSalaryAdjustmentSum(stf.Id, dateFirst, dateSecond);

                result.Add(salary);
            }

            return result;
        }

        public async Task<decimal> GetSalaryBonusSum(int staffId, DateTime dateFirst, DateTime dateSecond)
        {
            return await _dbContext.SalaryBonuses
                .Where(i=>i.StaffId == staffId && i.Date >= dateFirst && i.Date <= dateSecond)
                .SumAsync(i => i.BonusSum);
        }

        public async Task<decimal> GetSalaryAdjustmentSum(int staffId, DateTime dateFirst, DateTime dateSecond)
        {
            return await _dbContext.SalaryAdjustments
                .Where(i => i.StaffId == staffId && i.Date >= dateFirst && i.Date <= dateSecond)
                .SumAsync(i => i.AdjustmentSum);
        }

        public byte[] CreateSalaryFile(List<Salary> data)
        {
            var tableHeaders = new List<string[]>() { new string[] { "Name", "Shifts count", "Salary sum", "Bonus sum", "Adjustment sum", "Final sum" }};

            var convertedData = data
                .Select(i => new object[] { i.Staff?.Name, i.ShiftsCount, i.SalarySum, i.BonusSum, i.AdjustmentSum, i.FinalSum })
                .ToList();

            return _excelService.CreateExcelFile(tableHeaders, convertedData);
        }
    }

    public static class SalaryServiceProviderExtensions
    {
        public static void AddSalaryService(this IServiceCollection services)
        {
            services.AddTransient<ISalaryService, SalaryService>();
        }
    }
}
