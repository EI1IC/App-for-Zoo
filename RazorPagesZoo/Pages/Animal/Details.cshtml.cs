using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesZoo.Models;

namespace RazorPagesZoo.Pages.Animal
{
    public class DetailsModel : PageModel
    {
        private readonly RazorPagesZoo.Models.ZoodbContext _context;

        public DetailsModel(RazorPagesZoo.Models.ZoodbContext context)
        {
            _context = context;
        }

        public Models.Animal Animal { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals.FirstOrDefaultAsync(m => m.IdAnimal == id);
            if (animal == null)
            {
                return NotFound();
            }
            else
            {
                Animal = animal;
            }
            return Page();
        }
    }
}
