using Otium.Domain.Models;

namespace Otium.Domain.ViewModels;

public class UpdateNewsViewModel
{
    public bool IsNew { get; set; }
    public News? News { get; set; }
}