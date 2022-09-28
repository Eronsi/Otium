using System.Security.Cryptography;
using System.Text;

namespace Otium.Services.Utils;

public static class PasswordService
{
    public record PasswordHashRecord(byte[]? PasswordSalt, byte[]? PasswordHash);
    
    public static async Task<PasswordHashRecord> CreatePasswordHash(string password)
    {
        using var hmac = new HMACSHA512();
        return new PasswordHashRecord(
            hmac.Key,
            await hmac.ComputeHashAsync(new MemoryStream(Encoding.UTF8.GetBytes(password)))
        );
    }
    
    public static async Task<bool> VerifyPassword(string password, PasswordHashRecord passwordHashRecord)
    {
        if (passwordHashRecord.PasswordHash is null || passwordHashRecord.PasswordSalt is null)
            return false;
        
        using var hmac = new HMACSHA512(passwordHashRecord.PasswordSalt);
        var computedHash = await hmac.ComputeHashAsync(new MemoryStream(Encoding.UTF8.GetBytes(password)));
        return computedHash.SequenceEqual(passwordHashRecord.PasswordHash);
    }
}