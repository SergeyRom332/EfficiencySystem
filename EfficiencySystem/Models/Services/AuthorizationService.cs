using Microsoft.AspNetCore.Authentication.Cookies;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;

namespace EfficiencySystem.Models.Services
{
    public class AuthorizationService
    {
        private MainDbContext _dbContext;
        private HttpContext _httpContext;

        public AuthorizationService(MainDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task LogInAsync(string login, string password)
        {
            //Get user information
            var currentUser = await GetUserAsync(login, password);
            if (currentUser == null) throw new Exception("User doesn't exsist");

            //create list of claims
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Login", currentUser.Login));
            claims.Add(new Claim("Name", currentUser.Name));
            claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, currentUser.Role));

            //SignIn
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            await _httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }

        public async Task LogOutAsync()
        {
            await _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<User> GetUserAsync(string login, string password)
        {
            return await _dbContext.Users.Where(u => u.Login == login && u.Password == password).FirstOrDefaultAsync();
        }
    }

    public static class AuthorizationServiceProviderExtensions
    {
        public static void AddAuthorizationService(this IServiceCollection services)
        {
            services.AddTransient<AuthorizationService>();
        }
    }
}
