using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesZoo.Models;

namespace RazorPagesZoo.Pages.Task
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesZoo.Models.ZoodbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly TaskRepositoryService _taskRepository;
        public IndexModel(RazorPagesZoo.Models.ZoodbContext context, UserManager<IdentityUser> userManager, TaskRepositoryService taskRepository)
        {
            _context = context;
            _userManager = userManager;
            _taskRepository = taskRepository;
        }

        [BindProperty]
        public int SelectedTaskId { get; set; }
        public SelectList AvailableTasks { get; set; }
        public IList<Models.Task> Task { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Task = await _context.Tasks
                .Include(t => t.IdAnimalNavigation)
                .Include(t => t.IdCageNavigation).ToListAsync();
            
            var userId = _userManager.GetUserId(User);

            if (userId == null) return NotFound("Пользователь не найден");

            if (userId != null)
            {
                var availableTasks = await GetAvailableTasksAsync();
                AvailableTasks = new SelectList(availableTasks, "IdTask", "Description");
            }
            else
            {
                AvailableTasks = new SelectList(new List<Models.Task>(), "IdTask", "Description");
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = _userManager.GetUserId(User);
            if (userId == null) return NotFound("Пользователь не найден");

            // Находим сотрудника по ID пользователя (предполагается наличие связи Employee-User)
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.IdentityUserId == userId);
            if (employee == null) return NotFound("Сотрудник не найден");

            var task = await _context.Tasks.FindAsync(SelectedTaskId);
            if (task == null) return NotFound("Задача не найдена");

            // Проверка, не взята ли задача уже этим сотрудником
            if (task.JournalOfTasks.Any(jot => jot.IdTask == task.IdTask && (jot.Status == "Выполнено" || jot.Status == "В процессе")))
            {
                ModelState.AddModelError("SelectedTaskId", "Эта задача уже взята в работу.");
                return Page();
            }
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                // Начинаем транзакцию
                var success = await _taskRepository.AssignTaskAsync(SelectedTaskId, employee.IdEmployee, userId);
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                if (success)
                {
                    await _context.SaveChangesAsync();
                    return RedirectToPage("/JournalOfTask/Index"); // Перенаправление на ту же страницу для обновления
                }
                else
                {
                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }
                    ModelState.AddModelError("SelectedTaskId", "Не удалось назначить задание. Возможно, оно уже занято.");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при назначении задания: {ex.Message}");

                // Выводим сообщение об ошибке на текущей странице
                ModelState.AddModelError(string.Empty, "Не удалось назначить задание. Попробуйте снова.");
                return Page();
            }
            }
        private async Task<List<Models.Task>> GetAvailableTasksAsync()
        {
            // Получаем все задания из таблицы Tasks
            var allTasks = await _context.Tasks
                .Include(t => t.IdAnimalNavigation)
                .Include(t => t.IdCageNavigation)
                .ToListAsync();

            // Получаем все задания, которые уже есть в JournalOfTasks
            var takenTasks = await _context.JournalOfTasks
                .Select(j => j.IdTask)
                .Distinct()
                .ToListAsync();

            // Фильтруем задания: оставляем только те, которых нет в JournalOfTasks
            var availableTasks = allTasks
                .Where(t => !takenTasks.Contains(t.IdTask))
                .ToList();

            return availableTasks;
        }
    }
}