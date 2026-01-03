namespace BookingRoomsAPI.DataAccess.PostgreSQL.Entities;

public class RoomEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public UserEntity OwnerEntity { get; set; } = null!;
    public Guid OwnerId { get; set; }
}
