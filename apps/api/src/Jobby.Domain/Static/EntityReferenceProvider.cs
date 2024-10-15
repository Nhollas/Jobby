using System.Security.Cryptography;
using Jobby.Domain.Entities;
using Jobby.Domain.Primitives;

namespace Jobby.Domain.Static;

public static class EntityReferenceProvider<T> where T : Entity
{
    public static string CreateReference()
    {
        string entityTypeName = typeof(T).Name;
        
        string abbreviation = Abbreviations[entityTypeName];
        string randomChars = GenerateRandomChars(12);
        return $"{abbreviation}_{randomChars}";
    }

    private static readonly Dictionary<string, string> Abbreviations = new()
    {
        { nameof(Board), "bo" },
        { nameof(Job), "jo" },
        { nameof(Contact), "co"},
        { nameof(JobList), "li"},
        { nameof(Activity), "ac"}
    };

    private static string GenerateRandomChars(int length)
    {
        const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        using RandomNumberGenerator rng = RandomNumberGenerator.Create();
        
        byte[] data = new byte[length];
        char[] randomChars = new char[length];
        rng.GetBytes(data);

        for (int i = 0; i < length; i++)
        {
            randomChars[i] = characters[data[i] % characters.Length];
        }
        
        return new string(randomChars);
    }
}
