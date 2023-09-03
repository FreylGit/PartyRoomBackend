using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PartyRoom.Core.Entities;

namespace PartyRoom.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<UserRoom> UserRoom { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            CreateRole(modelBuilder);

            modelBuilder.Entity<Room>()
                .HasOne(r => r.Author)
                .WithMany(u => u.CreatedRooms)
                .HasForeignKey(r => r.AuthorId);
            modelBuilder.Entity<UserRoom>()
                .HasKey(ur => new { ur.UserId, ur.RoomId }); // Composite primary key

            modelBuilder.Entity<UserRoom>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoom)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRoom>()
                .HasOne(ur => ur.Room)
                .WithMany(r => r.UserRoom)
                .HasForeignKey(ur => ur.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

        private void CreateRole(ModelBuilder builder)
        {
            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = Guid.NewGuid(), Name = "Admin", NormalizedName = "ADMIN" },
                new ApplicationRole { Id = Guid.NewGuid(), Name = "User", NormalizedName = "USER" }
            );
        }
    }
}
