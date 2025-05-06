using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace RazorPagesZoo.Models;

public partial class Task
{
    [RegularExpression(@"^\d+$")]
    [Required]
    public int IdTask { get; set; }

    [RegularExpression(@"^\d+$")]
    public int IdAnimal { get; set; }

    [RegularExpression(@"^\d+$")]
    public int IdCage { get; set; }

    [Required]
    public string Description { get; set; } = null!;

    [DataType(DataType.Date)]
    [Required]
    public DateOnly DateDrop { get; set; }

    [DataType(DataType.Date)]
    [Required]
    public DateOnly Deadline { get; set; }

    public virtual Animal IdAnimalNavigation { get; set; }

    public virtual Cage IdCageNavigation { get; set; }

    public virtual ICollection<JournalOfTask> JournalOfTasks { get; set; } = new List<JournalOfTask>();
}
