using System.ComponentModel.DataAnnotations;

namespace Otium.Domain.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Введите имя")]
    [MaxLength(20, ErrorMessage = "Имя должно иметь длину меньше 20 символов")]
    [MinLength(3, ErrorMessage = "Имя должно иметь длину больше 3 символов")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите пароль")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; } = string.Empty;
    public string? ReturnUrl { get; set; }
}