using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesZoo.Models;

public partial class SpeciesNote
{
    [RegularExpression(@"^\d+$",ErrorMessage = "Введён некорректный номер")]
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    public int IdSpecies { get; set; }

    [RegularExpression(@"^[А-Я]+\s?[А-Яа-я\s]*$",ErrorMessage ="Введено некорректное название")]
    [StringLength(50, MinimumLength = 2)]
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    public string Name { get; set; } = null!;

    [RegularExpression(@"^(Плотоядный|Травоядный|Всеядный)$",ErrorMessage ="Должно быть: Плотоядный, Травоядный, Всеядный")]
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    public string SpeciesGroup { get; set; } = null!;

    [RegularExpression(@"^[А-Я]+\s?[А-Яа-я\s]*$",ErrorMessage ="Введена некорректная страна")]
    [StringLength(50, MinimumLength = 2)]
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    public string Country { get; set; } = null!;

    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [Url(ErrorMessage = "Введите корректный URL")]
    public string Image { get; set; } = null!;

    public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();
}
