using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesZoo.Models;

namespace RazorPagesZoo.Pages.JournalOfTask
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesZoo.Models.ZoodbContext _context;
        

        public IndexModel(RazorPagesZoo.Models.ZoodbContext context)
        {
            _context = context;
        }

        public IList<Models.JournalOfTask> JournalOfTask { get;set; } = new List<Models.JournalOfTask>();

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Status { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? CStatus { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateOnly? StartDateFrom { get; set; } 
        [BindProperty(SupportsGet = true)]
        public DateOnly? StartDateTo { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; }
        public string IdTaskSort { get; set; }
        public string StatusSort { get; set; }
        public string EndDateSort { get; set; }
        public string StartDateSort { get; set; }
        public string IdEmployeeSort { get; set; }

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            IdTaskSort = SortOrder == "idtask_desc" ? "idtask_asc" : "idtask_desc";
            StatusSort = SortOrder == "status_desc" ? "status_asc" : "status_desc";
            EndDateSort = SortOrder == "enddate_desc" ? "enddate_asc" : "enddate_desc";
            StartDateSort = SortOrder == "startdate_desc" ? "startdate_asc" : "startdate_desc";
            IdEmployeeSort = SortOrder == "idemployee_desc" ? "idemployee_asc" : "idemployee_desc";

            var journal_of_tasks = await _context.JournalOfTasks
                .Include(a => a.IdTaskNavigation)
                .Include(a => a.IdEmployeeNavigation)
                .ToListAsync();


            switch (SortOrder)
            {
                case "idtask_desc":
                    journal_of_tasks = journal_of_tasks.OrderByDescending(s => s.IdTaskNavigation.IdTask).ToList();
                    break;
                case "idtask_asc":
                    journal_of_tasks = journal_of_tasks.OrderBy(s => s.IdTaskNavigation.IdTask).ToList();
                    break;
                case "status_desc":
                    journal_of_tasks = journal_of_tasks.OrderByDescending(s => s.Status).ToList();
                    break;
                case "status_asc":
                    journal_of_tasks = journal_of_tasks.OrderBy(s => s.Status).ToList();
                    break;
                case "enddate_desc":
                    journal_of_tasks = journal_of_tasks.OrderByDescending(s => s.EndDate).ToList();
                    break;
                case "enddate_asc":
                    journal_of_tasks = journal_of_tasks.OrderBy(s => s.EndDate).ToList();
                    break;
                case "startdate_desc":
                    journal_of_tasks = journal_of_tasks.OrderByDescending(s => s.StartDate).ToList();
                    break;
                case "startdate_asc":
                    journal_of_tasks = journal_of_tasks.OrderBy(s => s.StartDate).ToList();
                    break;
                case "idemployee_desc":
                    journal_of_tasks = journal_of_tasks.OrderByDescending(s => s.IdEmployee).ToList();
                    break;
                case "idemployee_asc":
                    journal_of_tasks = journal_of_tasks.OrderBy(s => s.IdEmployee).ToList();
                    break;
                default:
                    journal_of_tasks = journal_of_tasks.OrderBy(s => s.StartDate).ToList(); // Сортировка по умолчанию
                    break;
            }

            if (!string.IsNullOrEmpty(CStatus))
            {
                journal_of_tasks = journal_of_tasks.Where(a => a.Status == CStatus).ToList();
            }

            // Фильтрация по дате начала
            if (StartDateFrom.HasValue)
            {
                journal_of_tasks = journal_of_tasks.Where(a => a.StartDate >= StartDateFrom.Value).ToList();
            }
            if (StartDateTo.HasValue)
            {
                journal_of_tasks = journal_of_tasks.Where(a => a.StartDate <= StartDateTo.Value).ToList();
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                string searchTermLower = SearchString.ToLower();
                JournalOfTask = journal_of_tasks.Where(s =>
                    (s.IdTaskNavigation.Description != null && s.IdTaskNavigation.Description.ToLower().Contains(searchTermLower)) ||
                    (s.Status != null && s.Status.ToLower().Contains(searchTermLower))
                ).ToList(); // Фильтрация на стороне клиента
            }
            else
            {
                JournalOfTask = journal_of_tasks;
                Status = new SelectList(await _context.JournalOfTasks.Select(s => s.Status).Distinct().ToListAsync(), "status");
            }

        }
        public async Task<IActionResult> OnPostDoneAsync(long IdEmployee, int idTask)
        {
            var task = await _context.JournalOfTasks
                .FindAsync(IdEmployee, idTask);
            if (task == null)
            {
                return NotFound(); // Задача не найдена
            }

            task.Status = "Выполнено";
            task.EndDate = DateOnly.FromDateTime(DateTime.Now); // Устанавливаем текущую дату

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Обработка ошибки конкурентного обновления
                if (!JournalOfTaskExists(task.IdTask))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index"); // Перенаправление на страницу списка задач
        }
        

        private bool JournalOfTaskExists(int idTask)
        {
            return _context.JournalOfTasks.Any(e => e.IdTask == idTask);
        }

    }
}
