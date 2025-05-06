using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesZoo.Models;

namespace RazorPagesZoo.Pages.Task
{
    public class DetailsModel : PageModel
    {
        private readonly RazorPagesZoo.Models.ZoodbContext _context;

        public DetailsModel(RazorPagesZoo.Models.ZoodbContext context)
        {
            _context = context;
        }

        public Models.Task Task { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FirstOrDefaultAsync(m => m.IdTask == id);
            if (task == null)
            {
                return NotFound();
            }
            else
            {
                Task = task;
            }
            return Page();
        }
    }
}
