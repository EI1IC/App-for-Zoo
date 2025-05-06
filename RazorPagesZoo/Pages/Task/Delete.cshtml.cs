using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesZoo.Models;

namespace RazorPagesZoo.Pages.Task
{
    public class DeleteModel : PageModel
    {
        private readonly RazorPagesZoo.Models.ZoodbContext _context;

        public DeleteModel(RazorPagesZoo.Models.ZoodbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Task Task { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FirstOrDefaultAsync(m => m.IdTask == id);

            if (task == null)
            {
                return NotFound();
            }
            else
            {
                Task = task;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                Task = task;
                _context.Tasks.Remove(Task);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
