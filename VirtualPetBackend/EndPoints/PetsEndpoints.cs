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

        group.MapPost("/", async (CreatePetDTO createPetDTO, VirtualPetBackendContext db) =>
        {
            var pet = createPetDTO.MapToEntity(db.Pets.Local.Count + 1);
            await db.Pets.AddAsync(pet);
            await db.SaveChangesAsync();

            return Results.Created($"/pets/{pet.Id}", pet.MapToPetDto());
        });

        return group;

    }
}
