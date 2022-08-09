using HotelSector.Core.Entities.Room;
using HotelSector.Core.Entities.RoomBooking;
using HotelSector.Core.Entities.Token;
using HotelSector.Core.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace HotelSector.Core.EntityFrameworkCore.Contexts
{
    public class HotelSectorDbContext : DbContext
    {
        public HotelSectorDbContext(DbContextOptions<HotelSectorDbContext> options)
            : base(options)
        {
        }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoomEntity> Room { get; set; }
        public DbSet<RoomBookingEntity> RoomBooking { get; set; }
        public DbSet<UserRefreshTokensEntity> UserRefreshTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>()
           .HasIndex(u => u.Email)
           .IsUnique();


            modelBuilder.Entity<RoomEntity>()
           .HasIndex(u => u.RoomNo)
           .IsUnique();

            modelBuilder.Entity<UserRefreshTokensEntity>()
           .HasIndex(u => new { u.RefreshToken, u.UserId })
           .IsUnique();

        }
    }
}
