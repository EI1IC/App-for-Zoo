using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesZoo.Models;

public partial class Post
{
    [Required]
    public int ID_Post { get; set; }

    [StringLength(50, MinimumLength = 5)]
    [Required]
    public string Charge { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
