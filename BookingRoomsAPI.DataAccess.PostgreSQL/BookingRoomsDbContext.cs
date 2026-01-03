using BookingRoomsAPI.DataAccess.PostgreSQL.Configurations;
using BookingRoomsAPI.DataAccess.PostgreSQL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingRoomsAPI.DataAccess.PostgreSQL;

public class BookingRoomsDbContext(DbContextOptions<BookingRoomsDbContext> options) 
    : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<RoomEntity> Rooms { get; set; }
    public DbSet<BookingEntity> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsersConfiguration());
        modelBuilder.ApplyConfiguration(new RoomsConfiguration());
        modelBuilder.ApplyConfiguration(new BookingsConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
