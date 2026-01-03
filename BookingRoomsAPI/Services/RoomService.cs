using BookingRoomsAPI.DataAccess.PostgreSQL.Abstractions.Repositories;
using BookingRoomsAPI.DataAccess.PostgreSQL.Abstractions.Services;
using BookingRoomsAPI.DataAccess.PostgreSQL.Filters;
using BookingRoomsAPI.DataAccess.PostgreSQL.Pages;
using BookingRoomsAPI.Domain.Entities;

namespace BookingRoomsAPI.Services;

public class RoomService : IRoomService
{
    private readonly IRoomsRepository _roomsRepository;

    public RoomService(IRoomsRepository roomsRepository)
    {
        _roomsRepository = roomsRepository;
    }

    public async Task<IEnumerable<Room>> GetAllRoomsAsync(RoomFilter filter, RoomPage page)
    {
        return await _roomsRepository.GetAllAsync(filter, page);
    }

    public async Task<Room?> GetByIdAsync(Guid id)
    {
        return await _roomsRepository.GetByIdAsync(id);
    }

    public async Task AddRoomAsync(Room room)
    {
        await _roomsRepository.AddAsync(room);
    }

    public async Task UpdateRoomAsync(Room room)
    {
        await _roomsRepository.UpdateAsync(room);
    }

    public async Task DeleteRoomAsync(Guid id)
    {
        await _roomsRepository.DeleteAsync(id);
    }
}
