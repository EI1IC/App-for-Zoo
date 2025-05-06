using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesZoo.Models;

public partial class Vaccination
{
    [RegularExpression(@"^\d+$")]
    [Required]
    public int IdVaccination { get; set; }

    [StringLength(50,MinimumLength =5)]
    [Required]
    public string NameVaccination { get; set; } = null!;

    [RegularExpression(@"^\d+$")]
    [Required]
    public int IdBatchLastVaccination { get; set; }

    [DataType(DataType.Date)]
    [Required]
    public DateOnly DateLastVaccination { get; set; }

    [DataType(DataType.Date)]
    [Required]
    public DateOnly DateNextVaccination { get; set; }

    [RegularExpression(@"^\d+$")]
    [Required]
    public int IdAnimal { get; set; }

    public virtual Animal IdAnimalNavigation { get; set; } = null!;
}
