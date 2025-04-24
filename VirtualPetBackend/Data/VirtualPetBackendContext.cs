using Microsoft.EntityFrameworkCore;
using VirtualPetBackend.Entities;

namespace VirtualPetBackend.Data;

public class VirtualPetBackendContext(DbContextOptions<VirtualPetBackendContext> options) : DbContext(options)
{
    public DbSet<Pet> Pets => Set<Pet>();
    public DbSet<UserEntity> Users => Set<UserEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pet>()
                    .HasOne(p => p.User)
                    .WithOne(u => u.Pet)
                    .HasForeignKey<Pet>(p => p.UserId)
                    .IsRequired();

        modelBuilder.Entity<Pet>()
                    .HasOne(p => p.Sprite)
                    .WithMany()
                    .HasForeignKey(p => p.SpriteId)
                    .IsRequired();

        modelBuilder.Entity<PetSprite>().HasData(
            new PetSprite { Id = 1, Name = "Default Rat", ImagePath = "/images/rat1.png" },
            new PetSprite { Id = 2, Name = "Fancy Rat", ImagePath = "/images/rat2.png" }
        );
    }
}
