using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesZoo.Models;

namespace RazorPagesZoo.Pages.Vaccination
{
    public class EditModel : PageModel
    {
        private readonly RazorPagesZoo.Models.ZoodbContext _context;

        public EditModel(RazorPagesZoo.Models.ZoodbContext context)
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

            var vaccination =  await _context.Vaccinations.FirstOrDefaultAsync(m => m.IdVaccination == id);
            if (vaccination == null)
            {
                return NotFound();
            }
            Vaccination = vaccination;
           ViewData["IdAnimal"] = new SelectList(_context.Animals, "IdAnimal", "Name");
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

            _context.Attach(Vaccination).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VaccinationExists(Vaccination.IdVaccination))
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

        private bool VaccinationExists(int id)
        {
            return _context.Vaccinations.Any(e => e.IdVaccination == id);
        }
    }
}
