using EfficiencySystem.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EfficiencySystem.Controllers
{
    public class AuthorizationController : Controller
    {
        AuthorizationService _authorizationService;

        public AuthorizationController(AuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public IActionResult LogInPage()
        {
            return View();
        }

        public IActionResult LogOutPage()
        {
            return View();
        }

        public async Task<IActionResult> LogIn(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password)) return BadRequest("Incorrect data");
            await _authorizationService.LogInAsync(login, password);

            return Ok();
        }

        public async Task<IActionResult> LogOut()
        {
            await _authorizationService.LogOutAsync();
            return Ok();
        }

        public IActionResult LogInRequired()
        {
            return View();
        }

        public IActionResult NoRights()
        {
            return View();
        }
    }
}
