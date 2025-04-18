using System;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using VirtualPetBackend.Entities;

namespace VirtualPetBackend.Data;

public interface IPasswordHelper
{
    string Hash(string password);
    bool Verify(string password, string v1, string v2);
}

public class PasswordHelper : IPasswordHelper
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100000;
    private readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;

    public string Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _hashAlgorithm, HashSize);

        return $"{Convert.ToHexString(hash)}:{Convert.ToHexString(salt)}";
    }

    public bool Verify(string password, string hash, string salt)
    {
        byte[] hashBytes = Convert.FromHexString(hash);
        byte[] saltBytes = Convert.FromHexString(salt);

        byte[] hashToVerify = Rfc2898DeriveBytes.Pbkdf2(password, saltBytes, Iterations, _hashAlgorithm, HashSize);

        return CryptographicOperations.FixedTimeEquals(hashBytes, hashToVerify);
    }
   
}

public sealed class LoginUser(VirtualPetBackendContext db, IPasswordHelper passwordHelper)
{
    public record LoginRequest(string Username, string Password);
    public async Task<UserEntity> Handle(LoginRequest request)
    {
        UserEntity? user = await db.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Username == request.Username) ?? throw new UnauthorizedAccessException("Invalid username or password.");

        string[] parts = user.PasswordHash.Split(':');
        
        if (parts.Length != 2)
        {
            throw new InvalidOperationException("Invalid password hash format.");
        }

        bool verified = passwordHelper.Verify(request.Password, parts[0], parts[1]);

        if (!verified)
        {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        return user;
        
    }
}
