using System;
using Microsoft.AspNetCore.Identity;

namespace VirtualPetBackend.Entities;

public class UserEntity : IdentityUser
{
    public required string Username { get; set; }
    public string? Role { get; set; }
    public required Pet Pet { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastLogin { get; set; } = DateTime.UtcNow;
}