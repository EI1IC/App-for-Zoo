﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesZoo.Models;

namespace RazorPagesZoo.Pages.Vaccination
{
    public class DetailsModel : PageModel
    {
        private readonly RazorPagesZoo.Models.ZoodbContext _context;

        public DetailsModel(RazorPagesZoo.Models.ZoodbContext context)
        {
            _context = context;
        }

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
    }
}
