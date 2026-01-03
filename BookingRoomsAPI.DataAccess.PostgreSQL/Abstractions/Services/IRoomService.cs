using BookingRoomsAPI.DataAccess.PostgreSQL.Filters;
using BookingRoomsAPI.DataAccess.PostgreSQL.Pages;
using BookingRoomsAPI.Domain.Entities;

namespace BookingRoomsAPI.DataAccess.PostgreSQL.Abstractions.Services;

public interface IRoomService
{
    Task AddRoomAsync(Room room);
    Task DeleteRoomAsync(Guid id);
    Task<IEnumerable<Room>> GetAllRoomsAsync(RoomFilter filter, RoomPage page);
    Task<Room?> GetByIdAsync(Guid id);
    Task UpdateRoomAsync(Room room);
}