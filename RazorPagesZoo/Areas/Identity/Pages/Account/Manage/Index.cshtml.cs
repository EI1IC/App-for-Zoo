using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesZoo.Models;

namespace RazorPagesZoo.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly ZoodbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly EmployeeService _employeeService;

        public IndexModel(ZoodbContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            EmployeeService employeeService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _employeeService = employeeService;
        }

        public Models.Employee Employee { get; set; } = default!;

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
        }

        private async System.Threading.Tasks.Task LoadAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            if (user != null)
            {
                Employee = await GetEmployeeDataAsync(user);
            }

            Username = userName;

            Input = new InputModel
            {
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync();

            // Вычисляем эффективность сотрудника, если он найден
            if (Employee != null)
            {
                Employee.WorkEfficiency = await _employeeService.CalculateWorkEfficiencyAsync(Employee.IdEmployee);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync();
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            return RedirectToPage();
        }

        private async Task<Employee> GetEmployeeDataAsync(IdentityUser user)
        {
            // Получаем данные сотрудника из базы данных по IdentityUserId
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.IdentityUserId == user.Id);

            if (employee == null)
            {
                // Если сотрудник не найден, можно вернуть null или выбросить исключение
                return null;
            }

            return employee;
        }
    }
}