using RazorPagesZoo.Models;
using RazorPagesZoo;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Globalization;


var builder = WebApplication.CreateBuilder(args);
var cultureInfo = new CultureInfo("ru-RU");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// Add services to the container.

builder.Services.AddDbContext<RazorPagesZoo.Models.ZoodbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Server=localhost;Database=zoodb;Username=postgres;Password=student; Persist Security Info=True")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ZoodbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<EmployeeService>();
builder.Services.AddTransient<TaskRepositoryService>();
builder.Services.AddScoped<VaccinationService>();


builder.Services.AddRazorPages();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}


using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "admin", "veterinarian", "keeper" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role)) await roleManager.CreateAsync(new IdentityRole(role));
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    var adminUser = userManager.FindByNameAsync("admin@mail.com").Result;

    if (adminUser == null)
    {
        adminUser = new IdentityUser { UserName = "admin@mail.com", Email = "admin@mail.com" };
        var result = userManager.CreateAsync(adminUser, "Password123!_").Result;

        if (result.Succeeded)
        {
            userManager.AddToRoleAsync(adminUser, "admin").Wait();
            Console.WriteLine("Admin user created successfully.");
        }
        else
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine(error.Description);
            }
        }
    }
    else
    {
        Console.WriteLine("Admin user already exists.");
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
