namespace Otium.Domain.Models;

public class Products
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string NameRus { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int MinPrice { get; set; }
    public int MaxPrice { get; set; }
}