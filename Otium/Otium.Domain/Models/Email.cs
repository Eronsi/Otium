using System.ComponentModel.DataAnnotations;

namespace Otium.Domain.Models;

public class Email
{
    [Key]
    public Guid Id { get; set;  } = Guid.NewGuid();
    public string Subject { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
}