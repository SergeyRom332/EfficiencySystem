using EfficiencySystem.Models;
using EfficiencySystem.Models.Services;
using EfficiencySystem.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EfficiencySystem.Controllers
{
    public class WorkShiftController : Controller
    {
        private IWorkShiftService _workShiftService;
        private IRestaurantService _restaurantService;

        public WorkShiftController(IWorkShiftService workShiftService, IRestaurantService restaurantService)
        {
            _workShiftService = workShiftService;
            _restaurantService = restaurantService;
        }

        public async Task<IActionResult> Index(WorkShiftViewModel viewModel)
        {
            viewModel.WorkShifts = await _workShiftService.GetWorkShiftsAsync(viewModel.DateFirst, viewModel.DateSecond, viewModel.RestaurantId);
            viewModel.Restaurants = await _restaurantService.GetRestaurantsAsync();

            return View(viewModel);
        }

        public async Task<IActionResult> WorkShift(int id)
        {
            if (id == 0) BadRequest();
            var workShift = await _workShiftService.GetWorkShiftAsync(id);

            return View(workShift);
        }

        [Authorize(Roles = "manager, admin")]
        public async Task<IActionResult> Add(WorkShift workShift)
        {
            if (workShift == null) return BadRequest();
            await _workShiftService.AddWorkShiftAsync(workShift);

            return Ok();
        }

        [Authorize(Roles = "manager, admin")]
        public async Task<IActionResult> Update(WorkShift workShift)
        {
            if (workShift == null) return BadRequest();
            await _workShiftService.UpdateWorkShiftAsync(workShift);

            return Ok();
        }

        [Authorize(Roles = "manager, admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return BadRequest();
            await _workShiftService.DeleteWorkShiftAsync(id);

            return Ok();
        }
    }
}
