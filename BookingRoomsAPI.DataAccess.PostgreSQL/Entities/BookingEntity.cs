namespace BookingRoomsAPI.DataAccess.PostgreSQL.Entities;

public class BookingEntity
{
    public Guid Id { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public decimal PricePerDay { get; set; }
    public decimal PricePerMonth { get; set; }
    public UserEntity UserEntity { get; set; } = null!;
    public Guid UserId { get; set; }
    public RoomEntity RoomEntity { get; set; } = null!;
    public Guid RoomId { get; set; }
}
