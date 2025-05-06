using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesZoo.Models;

namespace RazorPagesZoo.Pages.JournalOfTask
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesZoo.Models.ZoodbContext _context;

        public CreateModel(RazorPagesZoo.Models.ZoodbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["IdEmployee"] = new SelectList(_context.Employees, "IdEmployee", "IdEmployee");
        ViewData["IdTask"] = new SelectList(_context.Tasks, "IdTask", "IdTask");
            return Page();
        }

        [BindProperty]
        public Models.JournalOfTask JournalOfTask { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.JournalOfTasks.Add(JournalOfTask);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
