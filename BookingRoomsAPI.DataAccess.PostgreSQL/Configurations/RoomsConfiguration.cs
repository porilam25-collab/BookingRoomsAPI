using BookingRoomsAPI.DataAccess.PostgreSQL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingRoomsAPI.DataAccess.PostgreSQL.Configurations;

public class RoomsConfiguration : IEntityTypeConfiguration<RoomEntity>
{
    public void Configure(EntityTypeBuilder<RoomEntity> builder)
    {
        builder
            .ToTable("Rooms");
            
        builder
            .HasKey(r => r.Id);

        builder
            .HasOne(r => r.OwnerEntity)
            .WithMany(u => u.Rooms)
            .HasForeignKey(r => r.OwnerId);

        builder
            .Property(r => r.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder
            .Property(r => r.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder
            .Property(r => r.IsActive)
            .IsRequired();
    }
}
