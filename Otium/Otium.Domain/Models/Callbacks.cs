namespace Otium.Domain.Models;

public class Callbacks
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string Ip { get; set; } = string.Empty;
    public DateTimeOffset DateTime { get; set; }
}