namespace BookingRoomsAPI.DataAccess.PostgreSQL.Filters;

public class BookingFilter
{
    public DateTime? StartAt { get; set; }
    public DateTime? EndAt { get; set; }
    public decimal? PricePerDayMin { get; set; }
    public decimal? PricePerDayMax { get; set; }
    public decimal? PricePerMonthMin { get; set; }
    public decimal? PricePerMonthMax { get; set; }
    public Guid? UserId { get; set; }
    public Guid? RoomId { get; set; }
}
