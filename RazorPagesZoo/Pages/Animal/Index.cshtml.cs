using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesZoo.Models;

namespace RazorPagesZoo.Pages.AnimalNamespace
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesZoo.Models.ZoodbContext _context;

        public IndexModel(RazorPagesZoo.Models.ZoodbContext context)
        {
            _context = context;
        }

        public IList<Models.Animal> Animal { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Species { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? AnimalSpecie { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateOnly? DateOfBirthFrom { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateOnly? DateOfBirthTo { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; }

        public string NameSort { get; set; } // Свойства для URL-ссылок на сортировку
        public string FeaturesSort { get; set; }
        public string SexSort { get; set; }
        public string SpeciesSort { get; set; }
        public string DateOfBirthSort { get; set; }
        public string CageSort { get; set; } // Новое свойство для сортировки по клетке

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            // Инициализация свойств для сортировки
            NameSort = SortOrder == "name_desc" ? "name_asc" : "name_desc";
            FeaturesSort = SortOrder == "features_desc" ? "features_asc" : "features_desc";
            SexSort = SortOrder == "sex_desc" ? "sex_asc" : "sex_desc";
            SpeciesSort = SortOrder == "species_desc" ? "species_asc" : "species_desc";
            DateOfBirthSort = SortOrder == "dob_desc" ? "dob_asc" : "dob_desc";
            CageSort = SortOrder == "cage_desc" ? "cage_asc" : "cage_desc"; // Новое свойство

            // Загрузка данных с включением связанных сущностей
            var animals = await _context.Animals
                .Include(a => a.IdSpeciesNavigation)
                .Include(a => a.IdCageNavigation) // Включаем данные о клетке
                .ToListAsync();

            // Применение сортировки
            switch (SortOrder)
            {
                case "name_desc":
                    animals = animals.OrderByDescending(s => s.Name).ToList();
                    break;
                case "name_asc":
                    animals = animals.OrderBy(s => s.Name).ToList();
                    break;
                case "features_desc":
                    animals = animals.OrderByDescending(s => s.Features).ToList();
                    break;
                case "features_asc":
                    animals = animals.OrderBy(s => s.Features).ToList();
                    break;
                case "sex_desc":
                    animals = animals.OrderByDescending(s => s.Sex).ToList();
                    break;
                case "sex_asc":
                    animals = animals.OrderBy(s => s.Sex).ToList();
                    break;
                case "species_desc":
                    animals = animals.OrderByDescending(s => s.IdSpeciesNavigation.Name).ToList();
                    break;
                case "species_asc":
                    animals = animals.OrderBy(s => s.IdSpeciesNavigation.Name).ToList();
                    break;
                case "dob_desc":
                    animals = animals.OrderByDescending(s => s.Dob).ToList();
                    break;
                case "dob_asc":
                    animals = animals.OrderBy(s => s.Dob).ToList();
                    break;
                case "cage_desc":
                    animals = animals.OrderByDescending(s => s.IdCageNavigation?.Type).ToList(); // Сортировка по типу клетки (по убыванию)
                    break;
                case "cage_asc":
                    animals = animals.OrderBy(s => s.IdCageNavigation?.Type).ToList(); // Сортировка по типу клетки (по возрастанию)
                    break;
                default:
                    animals = animals.OrderBy(s => s.Name).ToList(); // Сортировка по умолчанию
                    break;
            }

            // Фильтрация по виду животного
            if (!string.IsNullOrEmpty(AnimalSpecie))
            {
                animals = animals.Where(a => a.IdSpeciesNavigation.Name == AnimalSpecie).ToList();
            }

            // Фильтрация по дате рождения
            if (DateOfBirthFrom.HasValue)
            {
                animals = animals.Where(a => a.Dob >= DateOfBirthFrom.Value).ToList();
            }
            if (DateOfBirthTo.HasValue)
            {
                animals = animals.Where(a => a.Dob <= DateOfBirthTo.Value).ToList();
            }

            // Фильтрация по поисковой строке
            if (!string.IsNullOrEmpty(SearchString))
            {
                string searchTermLower = SearchString.ToLower();
                Animal = animals.Where(s =>
                    (s.Name != null && s.Name.ToLower().Contains(searchTermLower)) ||
                    (s.Features != null && s.Features.ToLower().Contains(searchTermLower)) ||
                    (s.Sex != null && s.Sex.ToLower().Contains(searchTermLower)) ||
                    (s.IdSpeciesNavigation != null && s.IdSpeciesNavigation.Name.ToLower().Contains(searchTermLower)) ||
                    (s.IdCageNavigation != null && s.IdCageNavigation.Type.ToLower().Contains(searchTermLower)) // Поиск по типу клетки
                ).ToList();
            }
            else
            {
                Animal = animals;
            }

            // Загрузка списка видов для фильтрации
            Species = new SelectList(await _context.SpeciesNotes.Select(s => s.Name).Distinct().ToListAsync(), "Name");
        }
    }
}