using System;
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

        group.MapGet("/{id}", async (int id, VirtualPetBackendContext db) =>
        {
            return await db.Pets.FindAsync(id) is Pet pet ? Results.Ok(pet.MapToPetDto) : Results.NotFound();
        })
        .WithName("GetPetById");

        group.MapDelete("/{id}", async (int id, VirtualPetBackendContext db) =>
        {
            await db.Pets.Where(p => p.Id == id).ExecuteDeleteAsync();

            return Results.NoContent();
        })
        .WithName("DeletePetById");

        group.MapPost("/", async (CreatePetDTO createPetDTO, VirtualPetBackendContext db, HttpContext context) =>
        {
            var userId = context.User.FindFirst("id")?.Value;
            if (userId == null)
                return Results.Unauthorized();

            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == int.Parse(userId));
            if (user == null)
                return Results.NotFound("User not found");

            if (await db.Pets.AnyAsync(p => p.UserId == user.Id))
                return Results.Conflict("User already has a pet");

            var pet = new Pet
            {
                Name = createPetDTO.Name,
                UserId = user.Id,
                SpriteId = createPetDTO.SpriteId,
                CreatedAt = DateTime.UtcNow
            };

            await db.Pets.AddAsync(pet);
            await db.SaveChangesAsync();

            return Results.Created($"/pets/{pet.Id}", pet.MapToPetDto());
        });

        return group;

    }
}
