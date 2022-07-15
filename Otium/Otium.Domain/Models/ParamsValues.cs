namespace Otium.Domain.Models;

public class ParamsValues
{
    public int Id { get; set; }
    public int ParamId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NameRus { get; set; } = string.Empty;
    public int Price { get; set; }
}