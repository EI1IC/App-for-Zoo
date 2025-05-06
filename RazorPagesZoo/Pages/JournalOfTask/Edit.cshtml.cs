using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesZoo.Models;

namespace RazorPagesZoo.Pages.JournalOfTask
{
    public class EditModel : PageModel
    {
        private readonly RazorPagesZoo.Models.ZoodbContext _context;

        public EditModel(RazorPagesZoo.Models.ZoodbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.JournalOfTask JournalOfTask { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journaloftask =  await _context.JournalOfTasks.FirstOrDefaultAsync(m => m.IdEmployee == id);
            if (journaloftask == null)
            {
                return NotFound();
            }
            JournalOfTask = journaloftask;
           ViewData["IdEmployee"] = new SelectList(_context.Employees, "IdEmployee", "IdEmployee");
           ViewData["IdTask"] = new SelectList(_context.Tasks, "IdTask", "IdTask");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(JournalOfTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JournalOfTaskExists(JournalOfTask.IdEmployee))
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

        private bool JournalOfTaskExists(long id)
        {
            return _context.JournalOfTasks.Any(e => e.IdEmployee == id);
        }
    }
}
