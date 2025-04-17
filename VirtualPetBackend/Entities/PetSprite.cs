using System;

namespace VirtualPetBackend.Entities;

public class PetSprite
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string ImagePath { get; set; }
}
