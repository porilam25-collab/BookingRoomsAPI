using BookingRoomsAPI.DataAccess.PostgreSQL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingRoomsAPI.DataAccess.PostgreSQL.Configurations;

public class UsersConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder
            .ToTable("Users");

        builder
            .HasKey(u => u.Id);

        builder
            .HasMany(u => u.Rooms)
            .WithOne(r => r.OwnerEntity)
            .HasForeignKey(r => r.OwnerId);

        builder
            .HasMany(u => u.Bookings)
            .WithOne(b => b.UserEntity)
            .HasForeignKey(b => b.UserId);

        builder
            .HasIndex(u => u.Email)
            .IsUnique();

        builder
            .HasIndex(u => u.Login)
            .IsUnique();

        builder
            .Property(u => u.PasswordHash)
            .IsRequired();

        builder
            .Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(u => u.Login)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(u => u.Email)
            .IsRequired();
    }
}
