namespace BookingRoomsAPI.DataAccess.PostgreSQL.Filters;

public class RoomFilter
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? IsActive { get; set; }
    public Guid? OwnerId { get; set; }
}
