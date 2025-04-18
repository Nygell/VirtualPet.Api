using System;
using VirtualPetBackend.Data;
using VirtualPetBackend.DTOs;
using VirtualPetBackend.Entities;

namespace VirtualPetBackend.Mapping;

public static class UserMapping
{
    public static UserEntity MapToUserEntity(this CreateUserDTO createUserDTO, int petId)
    {
        return new UserEntity
        {
            
            Username = createUserDTO.Username,
            PasswordHash = createUserDTO.Password, // This should be hashed in the service layer
            PetSpriteId = petId,
            CreatedAt = DateTime.UtcNow,
            LastLogin = DateTime.UtcNow
        };
    }
}
