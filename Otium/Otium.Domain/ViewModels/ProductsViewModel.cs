using Otium.Domain.Models;

namespace Otium.Domain.ViewModels;

public class ProductsViewModel
{
    public Categories? Category { get; set; }
    public List<Products>? ProductsList { get; set; }
}