using EfficiencySystem.Models;
using EfficiencySystem.Models.Services;
using EfficiencySystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EfficiencySystem.Controllers
{
    public class SalaryController : Controller
    {
        private const string CASHIERS_COOKIE_KEY_NAME = "CashiersSalaryCache";
        private const string COOKS_COOKIE_KEY_NAME = "CooksSalaryCache";
        private const string COURIERS_COOKIE_KEY_NAME = "CouriersSalaryCache";
        private ISalaryService _salaryService;
        private ICacheService _cacheService;

        public SalaryController(ISalaryService salaryService, ICacheService cacheService)
        {
            _salaryService = salaryService;
            _cacheService = cacheService;
        }

        public async Task<IActionResult> Cashiers(SalaryViewModel viewModel)
        {
            viewModel.Salaries = await _salaryService.CalculateCashiersSalaries(viewModel.DateFirst, viewModel.DateSecond);

            //set cache
            var cacheKey = new Guid().ToString();
            _cacheService.AddCache(cacheKey, viewModel.Salaries);

            //set cookie
            HttpContext.Response.Cookies.Append(CASHIERS_COOKIE_KEY_NAME, cacheKey);

            return View(viewModel);
        }

        public async Task<IActionResult> Cooks(SalaryViewModel viewModel)
        {
            viewModel.Salaries = await _salaryService.CalculateCooksSalaries(viewModel.DateFirst, viewModel.DateSecond);

            //set cache
            var cacheKey = new Guid().ToString();
            _cacheService.AddCache(cacheKey, viewModel.Salaries);

            //set cookie
            HttpContext.Response.Cookies.Append(COOKS_COOKIE_KEY_NAME, cacheKey);

            return View(viewModel);
        }

        public async Task<IActionResult> Couriers(SalaryViewModel viewModel)
        {
            viewModel.Salaries = await _salaryService.CalculateCouriersSalaries(viewModel.DateFirst, viewModel.DateSecond);

            //set cache
            var cacheKey = new Guid().ToString();
            _cacheService.AddCache(cacheKey, viewModel.Salaries);

            //set cookie
            HttpContext.Response.Cookies.Append(COURIERS_COOKIE_KEY_NAME, cacheKey);

            return View(viewModel);
        }

        public async Task<IActionResult> ExportCashiers(DateTime dateFirst, DateTime dateSecond)
        {
            List<Salary> data;

            if (HttpContext.Request.Cookies.ContainsKey(CASHIERS_COOKIE_KEY_NAME))
            {
                data = _cacheService.GetCache<List<Salary>>(HttpContext.Request.Cookies[CASHIERS_COOKIE_KEY_NAME]);
            }
            else
            {
                data = await _salaryService.CalculateCashiersSalaries(dateFirst, dateSecond);
            }

            var fileData = _salaryService.CreateSalaryFile(data);

            return File(fileData, "application/xlsx", "CashiersSalary.xlsx");
        }

        public async Task<IActionResult> ExportCooks(DateTime dateFirst, DateTime dateSecond)
        {
            List<Salary> data;

            if (HttpContext.Request.Cookies.ContainsKey(COOKS_COOKIE_KEY_NAME))
            {
                data = _cacheService.GetCache<List<Salary>>(HttpContext.Request.Cookies[COOKS_COOKIE_KEY_NAME]);
            }
            else
            {
                data = await _salaryService.CalculateCooksSalaries(dateFirst, dateSecond);
            }

            var fileData = _salaryService.CreateSalaryFile(data);

            return File(fileData, "application/xlsx", "CooksSalary.xlsx");
        }

        public async Task<IActionResult> ExportCouriers(DateTime dateFirst, DateTime dateSecond)
        {
            List<Salary> data;

            if (HttpContext.Request.Cookies.ContainsKey(COURIERS_COOKIE_KEY_NAME))
            {
                data = _cacheService.GetCache<List<Salary>>(HttpContext.Request.Cookies[COURIERS_COOKIE_KEY_NAME]);
            }
            else
            {
                data = await _salaryService.CalculateCouriersSalaries(dateFirst, dateSecond);
            }

            var fileData = _salaryService.CreateSalaryFile(data);

            return File(fileData, "application/xlsx", "CouriersSalary.xlsx");
        }
    }
}
