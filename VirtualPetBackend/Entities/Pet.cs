
namespace VirtualPetBackend.Entities;

public class Pet
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Age { get; set; }
    public int SpriteId { get; set; }
    public int Hunger { get; set; }
    public int Happiness { get; set; }
    public PetSprite? Sprite { get; set; }
    public DateTime CreatedAt { get; set; }
}
