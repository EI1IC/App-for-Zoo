using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesZoo.Models;
using RazorPagesZoo;

namespace RazorPagesZoo.Pages.Vaccination
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesZoo.Models.ZoodbContext _context;
        private readonly VaccinationService _vaccinationService;

        public IndexModel(RazorPagesZoo.Models.ZoodbContext context, VaccinationService vaccinationservice)
        {
            _context = context;
            _vaccinationService = vaccinationservice;
        }

        public IList<Models.Vaccination> Vaccination { get;set; } = default!;
        public List<UpcomingVaccination> UpcomingVaccinations { get; set; }

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            Vaccination = await _context.Vaccinations
                .Include(v => v.IdAnimalNavigation).ToListAsync();
            UpcomingVaccinations = await _vaccinationService.GetUpcomingVaccinationsAsync();
        }

    }
}
