using System;
using VirtualPetBackend.DTOs;
using VirtualPetBackend.Entities;

namespace VirtualPetBackend.Mapping;

public static class PetMapping
{
    public static Pet MapToEntity(this PetDetailsDTO petDetailsDTO)
    {
        return new Pet
        {
            Id = petDetailsDTO.Id,
            Name = petDetailsDTO.Name,
            Age = petDetailsDTO.Age,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static PetDetailsDTO MapToPetDto(this Pet pet)
    {
        return new PetDetailsDTO
        {
            Id = pet.Id,
            Name = pet.Name,
            Age = pet.Age,
            CreatedAt = pet.CreatedAt
        };
    }

    public static Pet MapToEntity(this CreatePetDTO createPetDTO, int id)
    {
        return new Pet
        {
            Id = id,
            Name = createPetDTO.Name,
            CreatedAt = DateTime.UtcNow
        };
    } 
}
