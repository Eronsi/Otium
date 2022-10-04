using Otium.Domain.Models;

namespace Otium.Domain.ViewModels;

public class UpdateCategoryViewModel
{
    public bool IsNew { get; set; }
    public Categories? Category { get; set; }
}