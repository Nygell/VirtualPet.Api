using Microsoft.EntityFrameworkCore;
using VirtualPetBackend.Entities;

namespace VirtualPetBackend.Data;

public class VirtualPetBackendContext(DbContextOptions<VirtualPetBackendContext> options) : DbContext(options)
{
    public DbSet<Pet> Pets => Set<Pet>();
}
