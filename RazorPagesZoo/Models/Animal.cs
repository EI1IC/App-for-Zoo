using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesZoo.Models;

public partial class Animal
{
    [RegularExpression(@"^\d+$")]
    [Required]
    public int IdAnimal { get; set; }

    [RegularExpression(@"^\d+$")]
    [Required]
    public int IdSpecies { get; set; }

    [RegularExpression(@"^[А-Я]+[а-я\s]*$")] // Только буквы(кириллица)
    [StringLength(50, MinimumLength = 2)]
    [Required]
    public string Name { get; set; } = null!;

    [DataType(DataType.Date)]
    [Required]
    public DateOnly Dob { get; set; }

    [StringLength(1,MinimumLength=1)]
    [RegularExpression(@"^[MF]$")]
    [Required]
    public string Sex { get; set; } = null!;

    [Required]
    public string Features { get; set; } = null!;

    public int? IdCage { get; set; } // Nullable, если животное может не находиться в клетке

    // Навигационное свойство для клетки
    public virtual Cage IdCageNavigation { get; set; } = null!;

    public virtual SpeciesNote IdSpeciesNavigation { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();

    public virtual ICollection<Cage> IdCages { get; set; } = new List<Cage>();
}
