using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Otium.Domain.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class IsFileImageAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is not IFormFile {Length: > 0} file)
            return false;

        try
        {
            var imageContentType = file.ContentType;
            return imageContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase);
        }
        catch
        {
            return false;
        }
    }
}