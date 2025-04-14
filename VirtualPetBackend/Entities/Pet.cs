
namespace VirtualPetBackend.Entities;

public class Pet
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Age { get; set; }
    public DateTime CreatedAt { get; set; }
}
