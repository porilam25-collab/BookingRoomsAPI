namespace BookingRoomsAPI.Contracts.Bookings;

public record BookingCreate(
    Guid RoomId,
    DateTime StartAt,
    DateTime EndAt,
    decimal PricePerDay,
    decimal PricePerMonth);
