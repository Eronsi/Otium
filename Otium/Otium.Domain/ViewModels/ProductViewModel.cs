using Otium.Domain.Models;

namespace Otium.Domain.ViewModels;

public class ProductViewModel
{
    public Products? Product { get; set; }
    public Dictionary<Params, List<ParamsValues>>? Params { get; set; }
}