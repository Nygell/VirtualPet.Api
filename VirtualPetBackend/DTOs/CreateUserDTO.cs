using System;

namespace VirtualPetBackend.DTOs;

public class CreateUserDTO
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required int PetSpriteId { get; set; }
}
