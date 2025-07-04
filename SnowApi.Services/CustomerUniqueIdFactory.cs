using SnowApi.Services.Interfaces;

namespace SnowApi.Services;

public class CustomerUniqueIdFactory : ICustomerUniqueIdFactory
{
    private static readonly Random Random = new();

    /// <summary>
    /// Generate unique id for customer that will consist of two random letters and four random digits
    /// </summary>
    /// <returns>Unique id</returns> 
    public string GenerateUniqueId()
    {
        // Generate two random string characters (letters)
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var letter1 = chars[Random.Next(chars.Length)];
        var letter2 = chars[Random.Next(chars.Length)];

        // Generate four random integer characters (digits)
        var number = Random.Next(1000, 10000); // Generates a number from 1000 to 9999

        return $"{letter1}{letter2}{number}";
    }
}