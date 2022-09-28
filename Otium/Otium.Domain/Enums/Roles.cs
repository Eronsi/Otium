using System.ComponentModel.DataAnnotations;

namespace Otium.Domain.Enums;

public enum Roles
{
    [Display(Name = "Пользователь")]
    User = 0,
    [Display(Name = "Администратор")]
    Admin
}