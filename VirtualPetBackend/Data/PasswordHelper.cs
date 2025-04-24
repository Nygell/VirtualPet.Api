using System;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using VirtualPetBackend.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VirtualPetBackend.Settings;
using System.Text;

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



public sealed class LoginUser
{
    private readonly VirtualPetBackendContext _db;
    private readonly IPasswordHelper _passwordHelper;
    private readonly JwtSettings _jwtSettings;

    public LoginUser(
        VirtualPetBackendContext db, 
        IPasswordHelper passwordHelper,
        IOptions<JwtSettings> jwtSettings)
    {
        _db = db;
        _passwordHelper = passwordHelper;
        _jwtSettings = jwtSettings.Value;
    }

    public record LoginRequest(string Username, string Password);
    public record LoginResponse(string Token);

    public async Task<LoginResponse> Handle(LoginRequest request)
    {
        var user = await _db.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Username == request.Username) 
            ?? throw new UnauthorizedAccessException("Invalid username or password.");

        string[] parts = user.PasswordHash?.Split(':') ?? throw new InvalidOperationException("Invalid password hash format.");
        
        if (parts.Length != 2)
        {
            throw new InvalidOperationException("Invalid password hash format.");
        }

        bool verified = _passwordHelper.Verify(request.Password, parts[0], parts[1]);

        if (!verified)
        {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        var token = GenerateJwtToken(user);
        return new LoginResponse(token);
    }

    private string GenerateJwtToken(UserEntity user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
            new Claim("id", user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // Changed from HmacSha512

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
