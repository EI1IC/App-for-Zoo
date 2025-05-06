using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesZoo.Models;

namespace RazorPagesZoo.Pages.Vaccination
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
        ViewData["IdAnimal"] = new SelectList(_context.Animals, "IdAnimal", "Name");
            return Page();
        }

        [BindProperty]
        public Models.Vaccination Vaccination { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Vaccinations.Add(Vaccination);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
