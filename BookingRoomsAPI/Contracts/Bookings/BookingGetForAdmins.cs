namespace BookingRoomsAPI.Contracts.Bookings;

public record BookingGetForAdmins(
    Guid Id,
    Guid UserId,
    Guid RoomId,
    DateTime StartAt,
    DateTime EndAt,
    decimal PricePerDay,
    decimal PricePerMonth,
    decimal TotalPrice);
