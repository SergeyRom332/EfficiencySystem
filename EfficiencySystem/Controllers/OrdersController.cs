using EfficiencySystem.Models.Services;
using EfficiencySystem.Models;
using Microsoft.AspNetCore.Mvc;
using EfficiencySystem.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace EfficiencySystem.Controllers
{
    public class OrdersController : Controller
    {
        private IOrdersService _ordersService;
        private IRestaurantService _restaurantService;

        public OrdersController(IOrdersService ordersService, IRestaurantService restaurantService)
        {
            _ordersService = ordersService;
            _restaurantService = restaurantService;
        }

        public async Task<IActionResult> Index(OrdersViewModel viewModel)
        {
            viewModel.Orders = await _ordersService.GetOrdersAsync(viewModel.RestaurantId, viewModel.DateFirst, viewModel.DateSecond);
            viewModel.Restaurants = await _restaurantService.GetRestaurantsAsync();

            return View(viewModel);
        }

        public async Task<IActionResult> Order(int id)
        {
            if (id == 0) return BadRequest();
            var order = await _ordersService.GetOrderAsync(id);

            return View(order);
        }

        [Authorize(Roles = "manager, admin")]
        public async Task<IActionResult> Add(Order order)
        {
            if (order == null) return BadRequest();
            await _ordersService.AddOrderAsync(order);

            return Ok();
        }

        [Authorize(Roles = "manager, admin")]
        public async Task<IActionResult> Update(Order order)
        {
            if (order == null) return BadRequest();
            await _ordersService.UpdateOrderAsync(order);

            return Ok();
        }

        [Authorize(Roles = "manager, admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return BadRequest();
            await _ordersService.DeleteOrderAsync(id);

            return Ok();
        }
    }
}
