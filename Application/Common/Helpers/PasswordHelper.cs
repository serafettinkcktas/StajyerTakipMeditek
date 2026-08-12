using System.Security.Cryptography;

namespace Application.Common.Helpers;

public static class PasswordHelper
{
    private const string Chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz23456789!@#$%&*";

    public static string Generate(int length = 12)
    {
        return string.Create(length, Chars, (chars, charsSet) =>
        {
            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = charsSet[RandomNumberGenerator.GetInt32(charsSet.Length)];
            }
        });
    }

    public static string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public static bool Verify(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}