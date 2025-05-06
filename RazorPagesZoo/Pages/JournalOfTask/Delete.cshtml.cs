using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesZoo.Models;

namespace RazorPagesZoo.Pages.JournalOfTask
{
    public class DeleteModel : PageModel
    {
        private readonly RazorPagesZoo.Models.ZoodbContext _context;

        public DeleteModel(RazorPagesZoo.Models.ZoodbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.JournalOfTask JournalOfTask { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journaloftask = await _context.JournalOfTasks.FirstOrDefaultAsync(m => m.IdEmployee == id);

            if (journaloftask == null)
            {
                return NotFound();
            }
            else
            {
                JournalOfTask = journaloftask;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journaloftask = await _context.JournalOfTasks.FindAsync(id);
            if (journaloftask != null)
            {
                JournalOfTask = journaloftask;
                _context.JournalOfTasks.Remove(JournalOfTask);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
