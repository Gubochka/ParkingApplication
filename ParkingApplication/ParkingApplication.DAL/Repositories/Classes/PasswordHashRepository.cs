using System.Security.Cryptography;
using ParkingApplication.DAL.Repositories.Interfaces;

namespace ParkingApplication.DAL.Repositories.Classes;

public class PasswordHashRepository : IPasswordHashRepository
{
    private const int SaltSize = 16;
    private const int HashSize = 20;
    private const int Iterations = 10000;


    [Obsolete("Method HashPassword() is obsolete. Use MethodX() instead.")]
    public string HashPassword(string password)
    {
        byte[] salt;
        new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
        byte[] hash = pbkdf2.GetBytes(HashSize);

        byte[] hashBytes = new byte[SaltSize + HashSize];
        Array.Copy(salt, 0, hashBytes, 0, SaltSize);
        Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

        string savedPasswordHash = Convert.ToBase64String(hashBytes);
        return savedPasswordHash;
    }

    public bool VerifyPassword(string enteredPassword, string savedPasswordHash)
    {
        byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
        byte[] salt = new byte[SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);
        byte[] hash = new byte[HashSize];
        Array.Copy(hashBytes, SaltSize, hash, 0, HashSize);

        var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, Iterations);
        byte[] hashToVerify = pbkdf2.GetBytes(HashSize);

        for (int i = 0; i < HashSize; i++)
        {
            if (hashBytes[i + SaltSize] != hashToVerify[i])
                return false;
        }

        return true;
    }
}
