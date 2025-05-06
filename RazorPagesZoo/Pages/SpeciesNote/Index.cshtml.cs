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
    public class IndexModel : PageModel
    {
        private readonly RazorPagesZoo.Models.ZoodbContext _context;

        public IndexModel(RazorPagesZoo.Models.ZoodbContext context)
        {
            _context = context;
        }

        public IList<Models.SpeciesNote> SpeciesNote { get;set; } = default!;

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            SpeciesNote = await _context.SpeciesNotes.ToListAsync();
        }
    }
}
