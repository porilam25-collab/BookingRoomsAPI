namespace BookingRoomsAPI.Contracts.Rooms;

public record RoomGetForAdmins(
    Guid Id,
    Guid OwnerId,
    bool IsActive,
    string Title,
    string Description);
