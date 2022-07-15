using System.ComponentModel.DataAnnotations;

namespace Otium.Domain.Models;

public class Admins
{
    [Key]
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}