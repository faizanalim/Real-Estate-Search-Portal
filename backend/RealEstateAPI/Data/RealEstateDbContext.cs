using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Models;

namespace RealEstateAPI.Data
{
    public class RealEstateDbContext : DbContext
    {
        public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.PasswordHash).IsRequired();
            });

            // Configure Property entity
            modelBuilder.Entity<Property>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Address).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.Property(e => e.ListingType).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(2000);
                entity.Property(e => e.ImageUrls).HasMaxLength(4000);
            });

            // Configure Favorite entity
            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                // Configure many-to-many relationship
                entity.HasOne(e => e.User)
                    .WithMany(u => u.Favorites)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Property)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(e => e.PropertyId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Ensure unique combination of UserId and PropertyId
                entity.HasIndex(e => new { e.UserId, e.PropertyId }).IsUnique();
            });
        }
    }
}
