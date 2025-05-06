using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesZoo.Models;

namespace RazorPagesZoo.Pages.SpeciesNote
{
    public class DeleteModel : PageModel
    {
        private readonly RazorPagesZoo.Models.ZoodbContext _context;

        public DeleteModel(RazorPagesZoo.Models.ZoodbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.SpeciesNote SpeciesNote { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speciesnote = await _context.SpeciesNotes.FirstOrDefaultAsync(m => m.IdSpecies == id);

            if (speciesnote == null)
            {
                return NotFound();
            }
            else
            {
                SpeciesNote = speciesnote;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speciesnote = await _context.SpeciesNotes.FindAsync(id);
            if (speciesnote != null)
            {
                SpeciesNote = speciesnote;
                _context.SpeciesNotes.Remove(SpeciesNote);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
