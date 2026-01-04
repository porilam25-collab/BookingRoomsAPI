namespace BookingRoomsAPI.Contracts.Bookings;

public record BookingGet(
    Guid Id,
    Guid UserId,
    Guid RoomId,
    DateTime StartAt,
    DateTime EndAt,
    decimal TotalPrice);
