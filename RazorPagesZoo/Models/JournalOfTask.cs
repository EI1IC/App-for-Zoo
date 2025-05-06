using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using RazorPagesZoo.Migrations;

namespace RazorPagesZoo.Models;

public partial class JournalOfTask
{
    [Required]
    public long IdEmployee { get; set; }

    [Required]
    public int IdTask { get; set; }

    public string IdentityUserID { get; set; } = null!;

    [DataType(DataType.Date)]
    public DateOnly? StartDate { get; set; }
    
    [DataType(DataType.Date)]
    
    public DateOnly? EndDate { get; set; }


    [RegularExpression(@"^(Выдано|В процессе|Выполнено)$")]
    [Required]
    public string Status { get; set; } = null!;

    public virtual Employee IdEmployeeNavigation { get; set; } = null!;

    public virtual Task IdTaskNavigation { get; set; } = null!;

}
