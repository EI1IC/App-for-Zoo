﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesZoo.Models;

namespace RazorPagesZoo.Pages.Animal
{
    public class DeleteModel : PageModel
    {
        private readonly RazorPagesZoo.Models.ZoodbContext _context;

        public DeleteModel(RazorPagesZoo.Models.ZoodbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals.FindAsync(id);
            if (animal != null)
            {
                Animal = animal;
                _context.Animals.Remove(Animal);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
