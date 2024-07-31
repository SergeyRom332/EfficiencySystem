using EfficiencySystem.Models.Services;
using EfficiencySystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EfficiencySystem.Controllers
{
    public class RevenueController : Controller
    {
        private IRevenueService _revenueService;

        public RevenueController(IRevenueService revenueService)
        {
            _revenueService = revenueService;
        }

        public async Task<IActionResult> Index(RevenueViewModel viewModel)
        {
            viewModel.Revenues = await _revenueService.GetRevenue(viewModel.RestaurantId, viewModel.DateFirst, viewModel.DateSecond);

            return View(viewModel);
        }
    }
}
