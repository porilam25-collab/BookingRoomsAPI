namespace BookingRoomsAPI.Contracts.Rooms;

public record RoomGet(
    Guid Id,
    bool IsActive,
    string Title,
    string Description);
