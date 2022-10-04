using Otium.Domain.Attributes;

namespace Otium.Domain.Models;

public class Categories
{
    public int Id { get; set; }
    
    [ValidationAttributes.IsStringInEnglishAttribute(ErrorMessage = "Название категории должно быть на английском языке")]
    public string Name { get; set; } = string.Empty;
    
    [ValidationAttributes.IsStringInRussianAttribute(ErrorMessage = "Описание категории должно быть на русском языке")]
    public string NameRus { get; set; } = string.Empty;
}