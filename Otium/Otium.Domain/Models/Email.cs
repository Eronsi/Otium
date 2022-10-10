using System.ComponentModel.DataAnnotations;

namespace Otium.Domain.Models;

public class Email
{
    [Key] 
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Subject { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DateTimeOffset DateTime { get; set; } = DateTimeOffset.Now;
    public string Ip { get; set; } = string.Empty;
}