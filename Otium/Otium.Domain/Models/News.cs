using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Otium.Domain.Attributes;

namespace Otium.Domain.Models;

public class News
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Введите заголовок")]
    [MaxLength(127, ErrorMessage = "Заголовок должен иметь длину меньше 128 символов")]
    [MinLength(5, ErrorMessage = "Заголовок должен иметь длину больше 5 символов")]
    public string Title { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Введите краткое описание")]
    [MinLength(5, ErrorMessage = "Краткое описание должно иметь длину больше 5 символов")]
    public string ShortDescription { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Введите описание")]
    [MinLength(5, ErrorMessage = "Описание должно иметь длину больше 5 символов")]
    public string Description { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; } = DateTime.Now;
    
    [NotMapped]
    [Required(ErrorMessage = "Добавьте изображение")]
    [ValidationAttributes.IsFileImageAttribute(ErrorMessage = "Файл не является изображением")]
    public IFormFile? Image { get; set; }
}