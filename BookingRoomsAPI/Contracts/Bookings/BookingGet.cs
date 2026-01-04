namespace BookingRoomsAPI.Contracts.Bookings;

public record BookingGet(
    Guid UserId,
    Guid RoomId,
    DateTime StartAt,
    DateTime EndAt,
    decimal TotalPrice);
