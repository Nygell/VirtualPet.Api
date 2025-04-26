namespace VirtualPetBackend.DTOs;

public record class UserDetailsDTO
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime LastLogin { get; set; }
    public string? PetName { get; set; }
}
