using BookingRoomsAPI.Domain.Common;

namespace BookingRoomsAPI.Domain.Entities;

public class Booking
{
    public Guid Id { get; set; }
    public Guid UserId { get; }
    public Guid RoomId { get;  } 
    public DateTime StartAt { get; }
    public DateTime EndAt { get; }

    public decimal PricePerDay { get; }
    public decimal PricePerMonth { get; }
    public decimal TotalPrice { get; }

    private Booking(Guid id, Guid userId, Guid roomId, DateTime startAt, DateTime endAt, decimal pricePerDay, decimal pricePerMonth)
    {
        Id = id;
        UserId = userId;
        RoomId = roomId;
        StartAt = startAt;
        EndAt = endAt;
        PricePerDay = pricePerDay;
        PricePerMonth = pricePerMonth;
        TotalPrice = CalculateTotalPrice(pricePerDay, pricePerMonth, startAt, endAt);
    }

    private decimal CalculateTotalPrice(decimal pricePerDay, decimal pricePerMonth, DateTime startAt, DateTime endAt)
    {
        var totalDays = (int)Math.Ceiling((endAt - startAt).TotalDays);

        if (totalDays < 30)
            return pricePerDay * totalDays;

        var days = totalDays % 30;
        var month = (totalDays - days) / 30;

        return month * pricePerMonth + days * pricePerDay;
    }

    public static Result<Booking> Create(Guid id, Guid userId, Guid roomId, DateTime startAt, DateTime endAt, decimal pricePerDay, decimal pricePerMonth)
    {
        var res = new Result<Booking>();

        if (startAt < DateTime.Now)
            res.Failured("Start booking can`t be earlier than today");

        if (!string.IsNullOrEmpty(res.Error))
            return res;

        if (pricePerDay <= 0)
            res.Failured("Price per day can`t be zero");

        if (pricePerMonth <= 0)
            res.Failured("Price per month can`t be zero");

        if (!string.IsNullOrEmpty(res.Error))
            return res;

        res.Success(new Booking(id, userId, roomId, startAt, endAt, pricePerDay, pricePerMonth));

        return res;
    }
}
