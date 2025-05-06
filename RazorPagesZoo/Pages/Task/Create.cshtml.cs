using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesZoo.Models;

namespace RazorPagesZoo.Pages.Task
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesZoo.Models.ZoodbContext _context;

        public CreateModel(RazorPagesZoo.Models.ZoodbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Models.Task Task { get; set; } = new Models.Task();
        public IActionResult OnGet()
        {
            var animals = _context.Animals.ToList();
            var cages = _context.Cages.ToList();

            if (!animals.Any())
            {
                Console.WriteLine("Нет животных в базе данных!");
                return RedirectToPage("/Error");
            }

            if (!cages.Any())
            {
                Console.WriteLine("Нет клеток в базе данных!");
                return RedirectToPage("/Error");
            }
            Task = new Models.Task { IdAnimal = 1 };
            ViewData["IdAnimal"] = new SelectList(_context.Animals, "IdAnimal", "Name",Task.IdAnimal);
            ViewData["IdCage"] = new SelectList(_context.Cages, "IdCage", "Type",Task.IdCage);

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["IdAnimal"] = new SelectList(await _context.Animals.ToListAsync(), "IdAnimal", "Name");
                ViewData["IdCage"] = new SelectList(await _context.Cages.ToListAsync(), "IdCage", "Type");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return Page();
            }
            var selectedAnimalId = Request.Form["Task.IdAnimal"];
            var selectedCageId = Request.Form["Task.IdCage"];

            if (int.TryParse(selectedAnimalId, out var animalId))
            {
                Task.IdAnimal = animalId; // Устанавливаем IdAnimal
            }

            if (int.TryParse(selectedCageId, out var cageId))
            {
                Task.IdCage = cageId; // Устанавливаем IdCage
            }


            _context.Tasks.Add(Task);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
