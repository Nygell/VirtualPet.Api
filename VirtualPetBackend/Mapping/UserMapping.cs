using System;
using VirtualPetBackend.Data;
using VirtualPetBackend.DTOs;
using VirtualPetBackend.Entities;

namespace VirtualPetBackend.Mapping;

public static class UserMapping
{
    public static UserEntity MapToUserEntity(this CreateUserDTO createUserDTO)
    {
        return new UserEntity
        {
            
            Username = createUserDTO.Username,
            PasswordHash = createUserDTO.Password, // This should be hashed in the service layer
            CreatedAt = DateTime.UtcNow,
            LastLogin = DateTime.UtcNow,
            Role = createUserDTO.Role
        };
    }

    public static UserDetailsDTO MapToUserDetailsDTO(this UserEntity userEntity)
    {
        return new UserDetailsDTO
        {
            Id = userEntity.Id,
            Username = userEntity.Username,
            Role = userEntity.Role ?? "User",
            CreatedAt = userEntity.CreatedAt,
            LastLogin = userEntity.LastLogin,
            PetName = userEntity.Pet?.Name
        };
    }
}
