namespace BookingRoomsAPI.Contracts.Users;

public record UserUpdate(
    string Email,
    string Login,
    string Name);
