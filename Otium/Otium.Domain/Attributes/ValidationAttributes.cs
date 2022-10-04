using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace Otium.Domain.Attributes;

public abstract class ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class IsFileImageAttribute : ValidationAttribute
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
    
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class IsStringInRussianAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value) =>
            value is string str && Regex.IsMatch(str, @"^[а-яА-ЯёЁ]+$");
    }
    
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class IsStringInEnglishAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value) =>
            value is string str && Regex.IsMatch(str, @"^[a-zA-Z]+$");
    }
}