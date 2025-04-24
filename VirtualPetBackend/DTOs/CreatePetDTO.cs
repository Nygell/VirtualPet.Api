using VirtualPetBackend.Entities;

namespace VirtualPetBackend.DTOs;

public record class CreatePetDTO()
{
    public required string Name {get; set;}
    public required int SpriteId {get; set;} = 1; // Default sprite id
}
