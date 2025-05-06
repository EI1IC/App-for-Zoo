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
    public class DetailsModel : PageModel
    {
        private readonly RazorPagesZoo.Models.ZoodbContext _context;

        public DetailsModel(RazorPagesZoo.Models.ZoodbContext context)
        {
            _context = context;
        }

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
    }
}
