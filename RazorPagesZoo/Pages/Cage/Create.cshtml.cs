using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesZoo.Models;

namespace RazorPagesZoo.Pages.Cage
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
            return Page();
        }

        [BindProperty]
        public Models.Cage Cage { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Cages.Add(Cage);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
