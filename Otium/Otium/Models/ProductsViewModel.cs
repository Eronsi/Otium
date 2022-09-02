using Otium.Domain.Models;

namespace Otium.Models;

public class ProductsViewModel
{
    public Categories? Category { get; set; }
    public List<Products>? ProductsList { get; set; }
}