using BookingRoomsAPI.DataAccess.PostgreSQL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingRoomsAPI.DataAccess.PostgreSQL.Configurations;

public class BookingsConfiguration : IEntityTypeConfiguration<BookingEntity>
{
    public void Configure(EntityTypeBuilder<BookingEntity> builder)
    {
        builder
            .ToTable("Bookings");

        builder
            .HasKey(b => b.Id);

        builder
            .HasOne(b => b.UserEntity)
            .WithMany(u => u.Bookings)
            .HasForeignKey(b => b.UserId);

        builder
            .HasOne(b => b.RoomEntity)
            .WithMany()
            .HasForeignKey(b => b.RoomId);

        builder
            .Property(b => b.StartAt)
            .IsRequired()
            .HasColumnType("timestamp without time zone");

        builder
            .Property(b => b.EndAt)
            .IsRequired()
            .HasColumnType("timestamp without time zone");
    }
}
