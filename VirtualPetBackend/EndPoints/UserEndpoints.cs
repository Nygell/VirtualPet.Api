using System;
using Microsoft.EntityFrameworkCore;
using VirtualPetBackend.Data;
using VirtualPetBackend.DTOs;
using VirtualPetBackend.Mapping;

namespace VirtualPetBackend.EndPoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication routes)
    {
        var group = routes.MapGroup("/user").WithTags("User");

        group.MapGet("/", async (VirtualPetBackendContext db) =>
        {
            return await db.Users
                .Include(u => u.Pet)
                .Select(u => u.MapToUserDetailsDTO())
                .ToListAsync();
        });

        group.MapPost("/", async (VirtualPetBackendContext db, CreateUserDTO createUserDto, IPasswordHelper passwordHelper) =>
        {
            var user = createUserDto.MapToUserEntity();

            if (await db.Users.AnyAsync(u => u.Username == user.Username))
            {
                return Results.Conflict("Username already exists.");
            }

            user.PasswordHash = passwordHelper.Hash(createUserDto.Password);

            db.Users.Add(user);
            await db.SaveChangesAsync();
            return Results.Created($"/api/user/{user.Username}", user);
        });

        group.MapPost("/login", async (LoginUser.LoginRequest loginRequest, LoginUser useCase) =>
        {
            var response = await useCase.Handle(loginRequest);
            return Results.Ok(response);
        });
    }
}
