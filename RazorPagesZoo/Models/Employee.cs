using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace RazorPagesZoo.Models;

public partial class Employee
{
    [RegularExpression(@"^\d+$")]
    [Required]
    public long IdEmployee { get; set; }

    [StringLength(50, MinimumLength = 2)]
    [Required]
    public string Surname { get; set; } = null!;

    [StringLength(50, MinimumLength = 2)]
    [Required]
    public string Name { get; set; } = null!;

    [StringLength(50, MinimumLength = 5)]
    [Required]
    public string Patronymic { get; set; } = null!;

    [Range(1,100)]
    public float WorkEfficiency { get; set; }
    [Required]
    public string IdentityUserId { get; set; } = null!;

    [Required]
    public string Role { get; set; } = null!;

    public virtual ICollection<JournalOfTask> JournalOfTasks { get; set; } = new List<JournalOfTask>();

}
