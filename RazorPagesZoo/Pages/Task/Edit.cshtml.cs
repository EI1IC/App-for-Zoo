using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesZoo.Models;

namespace RazorPagesZoo.Pages.Task
{
    public class EditModel : PageModel
    {
        private readonly RazorPagesZoo.Models.ZoodbContext _context;

        public EditModel(RazorPagesZoo.Models.ZoodbContext context)
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

            var task =  await _context.Tasks.FirstOrDefaultAsync(m => m.IdTask == id);
            if (task == null)
            {
                return NotFound();
            }
            Task = task;
           ViewData["IdAnimal"] = new SelectList(_context.Animals, "IdAnimal", "Name");
           ViewData["IdCage"] = new SelectList(_context.Cages, "IdCage", "IdCage");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(Task.IdTask))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.IdTask == id);
        }
    }
}
