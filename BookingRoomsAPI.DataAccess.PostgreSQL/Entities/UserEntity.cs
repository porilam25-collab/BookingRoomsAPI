namespace BookingRoomsAPI.DataAccess.PostgreSQL.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public ICollection<RoomEntity> Rooms { get; set; } = [];
    public ICollection<BookingEntity> Bookings { get; set; } = [];
}
