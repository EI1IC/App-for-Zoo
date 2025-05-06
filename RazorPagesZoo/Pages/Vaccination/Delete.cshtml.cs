using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesZoo.Models;

namespace RazorPagesZoo.Pages.Vaccination
{
    public class DeleteModel : PageModel
    {
        private readonly RazorPagesZoo.Models.ZoodbContext _context;

        public DeleteModel(RazorPagesZoo.Models.ZoodbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Vaccination Vaccination { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccination = await _context.Vaccinations.FirstOrDefaultAsync(m => m.IdVaccination == id);

            if (vaccination == null)
            {
                return NotFound();
            }
            else
            {
                Vaccination = vaccination;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccination = await _context.Vaccinations.FindAsync(id);
            if (vaccination != null)
            {
                Vaccination = vaccination;
                _context.Vaccinations.Remove(Vaccination);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
