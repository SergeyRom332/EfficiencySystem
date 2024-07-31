using EfficiencySystem.Models;
using EfficiencySystem.Models.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddWorkShiftService();
builder.Services.AddStaffService();
builder.Services.AddRestaurantService();
builder.Services.AddOrdersService();
builder.Services.AddRevenueService();
builder.Services.AddSalaryService();
builder.Services.AddMemoryCache();
builder.Services.AddCacheService();
builder.Services.AddExcelService();
builder.Services.AddAuthorizationService();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDepartmentService();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/Authorization/LogInRequired";
    options.AccessDeniedPath = "/Authorization/NoRights";
});

builder.Services.AddDbContext<MainDbContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));
builder.Logging.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "log.txt"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseStatusCodePagesWithReExecute("/Home/HttpError", "?statusCode={0}");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Staff}/{action=Index}/{id?}");

app.Run();

