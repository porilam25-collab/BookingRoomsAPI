using BookingRoomsAPI.DataAccess.PostgreSQL.Filters;
using BookingRoomsAPI.DataAccess.PostgreSQL.Pages;
using BookingRoomsAPI.Domain.Entities;

namespace BookingRoomsAPI.DataAccess.PostgreSQL.Abstractions.Repositories
{
    public interface IRoomsRepository
    {
        Task AddAsync(Room room);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Room>> GetAllAsync(RoomFilter filter, RoomPage page);
        Task<Room?> GetByIdAsync(Guid id);
        Task UpdateAsync(Room room);
    }
}