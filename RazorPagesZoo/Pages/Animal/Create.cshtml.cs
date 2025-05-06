using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesZoo.Models;

namespace RazorPagesZoo.Pages.Animal
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
            // Загружаем список видов животных
            ViewData["IdSpecies"] = new SelectList(_context.SpeciesNotes, "IdSpecies", "Name");

            // Загружаем список клеток
            ViewData["IdCage"] = new SelectList(_context.Cages, "IdCage", "Type");

            return Page();
        }

        [BindProperty]
        public Models.Animal Animal { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Если модель невалидна, загружаем списки снова
                ViewData["IdSpecies"] = new SelectList(_context.SpeciesNotes, "IdSpecies", "Name");
                ViewData["IdCage"] = new SelectList(_context.Cages, "IdCage", "Type");
                return Page();
            }

            // Добавляем животное в базу данных
            _context.Animals.Add(Animal);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}