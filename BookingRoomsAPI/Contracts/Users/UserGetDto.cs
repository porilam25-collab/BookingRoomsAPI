using BookingRoomsAPI.Contracts.Bookings;
using BookingRoomsAPI.Contracts.Rooms;

namespace BookingRoomsAPI.Contracts.Users;

public record UserGetDto(
    Guid Id,
    string Email,
    string Login,
    string Name,
    IEnumerable<RoomGetForAdmins> Rooms,
    IEnumerable<BookingGetForAdmins> Bookings);
