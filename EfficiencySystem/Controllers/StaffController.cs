using EfficiencySystem.Models.Services;
using EfficiencySystem.Models.ViewModels;
using EfficiencySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EfficiencySystem.Controllers
{
    public class StaffController : Controller
    {
        private IStaffService _staffService;
        private IDepartmentService _departmentService;

        public StaffController(IStaffService staffService, IDepartmentService departmentService)
        {
            _staffService = staffService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index(StaffViewModel viewModel)
        {
            viewModel.Staff = await _staffService.GetAllStaffAsync(viewModel.DepartmentId);
            viewModel.Departments = await _departmentService.GetDepartmentsAsync();

            return View(viewModel);
        }

        public async Task<IActionResult> Employee(int id)
        {
            if (id == 0) return BadRequest();
            var order = await _staffService.GetStaffAsync(id);

            return View(order);
        }

        [Authorize(Roles = "manager, admin")]
        public async Task<IActionResult> Add(Staff staff)
        {
            if (staff == null) return BadRequest();
            await _staffService.AddStaffAsync(staff);

            return Ok();
        }

        [Authorize(Roles = "manager, admin")]
        public async Task<IActionResult> Update(Staff staff)
        {
            if (staff == null) return BadRequest();
            await _staffService.UpdateStaffAsync(staff);

            return Ok();
        }

        [Authorize(Roles = "manager, admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return BadRequest();
            await _staffService.DeleteStaffAsync(id);

            return Ok();
        }
    }
}
