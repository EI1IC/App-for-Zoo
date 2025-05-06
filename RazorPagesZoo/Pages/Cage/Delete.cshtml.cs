using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesZoo.Models;

namespace RazorPagesZoo.Pages.Cage
{
    public class DeleteModel : PageModel
    {
        private readonly RazorPagesZoo.Models.ZoodbContext _context;

        public DeleteModel(RazorPagesZoo.Models.ZoodbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Cage Cage { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cage = await _context.Cages.FirstOrDefaultAsync(m => m.IdCage == id);

            if (cage == null)
            {
                return NotFound();
            }
            else
            {
                Cage = cage;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cage = await _context.Cages.FindAsync(id);
            if (cage != null)
            {
                Cage = cage;
                _context.Cages.Remove(Cage);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
