using System.ComponentModel.DataAnnotations;

namespace VirtualPetBackend.DTOs;

public record class PetDetailsDTO
{
    public int Id { get; set; }
    [Required] [MaxLength(50)]
    public required string Name { get; set; }
    public int Age { get; set; }
    public DateTime CreatedAt { get; set; }
}
