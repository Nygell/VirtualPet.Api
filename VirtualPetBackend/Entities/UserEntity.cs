using System;
using Microsoft.AspNetCore.Identity;

namespace VirtualPetBackend.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public string? Role { get; set; }
    public required int PetSpriteId { get; set; }
    public Pet? Pet { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastLogin { get; set; } = DateTime.UtcNow;
}