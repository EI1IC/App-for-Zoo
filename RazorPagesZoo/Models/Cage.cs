using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesZoo.Models;

public partial class Cage
{
    [RegularExpression(@"^\d+$")]
    [Required]
    public int IdCage { get; set; }

    [RegularExpression(@"^\d+$")]
    [Required]
    public int Quantity {  get; set; }

    [RegularExpression(@"^\d+(?:[.,]\d+)+$")]
    [Required]
    public decimal CageSize { get; set; }

    [RegularExpression(@"^-?\d+(?:[.,]\d+)$")]
    [Required]
    public decimal Temp { get; set; }

    [StringLength(50,MinimumLength = 5)]
    [Required]
    public string Type { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<Animal> IdAnimals { get; set; } = new List<Animal>();
}
