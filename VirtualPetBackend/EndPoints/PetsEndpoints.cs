using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using VirtualPetBackend.Data;
using VirtualPetBackend.DTOs;
using VirtualPetBackend.Entities;
using VirtualPetBackend.Mapping;

namespace VirtualPetBackend.EndPoints;

public static class PetsEndpoints
{
    public static RouteGroupBuilder MapPetsEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("/pets").WithTags("Pets");

        group.MapGet("/", async (VirtualPetBackendContext db) =>
        {
            return await db.Pets.Select(p => p.MapToPetDto()).ToListAsync();
        })
        .WithName("GetAllPets");

        group.MapGet("/{userId}", async (int userId, VirtualPetBackendContext db) =>
        {
            var pet = await db.Pets
                .Include(p => p.Sprite)
                .FirstOrDefaultAsync(p => p.UserId == userId);

            return pet != null ? 
                Results.Ok(pet.MapToPetDto()) : 
                Results.NotFound("No pet found for this user");
        })
        .WithName("GetPetByUserId");

        group.MapDelete("/{id}", async (int id, VirtualPetBackendContext db) =>
        {
            await db.Pets.Where(p => p.Id == id).ExecuteDeleteAsync();

            return Results.NoContent();
        })
        .WithName("DeletePetById");

        group.MapPost("/", [Authorize] async (CreatePetDTO createPetDTO, VirtualPetBackendContext db, HttpContext context) =>
        {
            var userId = context.User.FindFirst("id")?.Value;

            var user = await db.Users
                .Include(u => u.Pet)
                .FirstOrDefaultAsync(u => u.Id == int.Parse(userId!));
            if (user == null)
                return Results.NotFound("User not found");

            if (user.Pet != null)
                return Results.Conflict("User already has a pet");

            var pet = new Pet
            {
                Name = createPetDTO.Name,
                UserId = user.Id,
                SpriteId = createPetDTO.SpriteId,
                CreatedAt = DateTime.UtcNow
            };

            user.Pet = pet;
            await db.SaveChangesAsync();

            return Results.Created($"/pets/{pet.Id}", pet.MapToPetDto());
        });

        return group;
    }
}
