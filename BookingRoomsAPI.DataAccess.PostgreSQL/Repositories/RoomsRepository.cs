using BookingRoomsAPI.DataAccess.PostgreSQL.Abstractions.Repositories;
using BookingRoomsAPI.DataAccess.PostgreSQL.Extensions;
using BookingRoomsAPI.DataAccess.PostgreSQL.Filters;
using BookingRoomsAPI.DataAccess.PostgreSQL.Mappers;
using BookingRoomsAPI.DataAccess.PostgreSQL.Pages;
using BookingRoomsAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingRoomsAPI.DataAccess.PostgreSQL.Repositories;

public class RoomsRepository : IRoomsRepository
{
    private readonly BookingRoomsDbContext _context;

    public RoomsRepository(BookingRoomsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Room>> GetAllAsync(RoomFilter filter, RoomPage page)
    {
        var rooms = await _context
            .Rooms
            .AsNoTracking()
            .Include(r => r.OwnerEntity)
            .UseFilter(filter)
            .UsePaging(page)
            .ToListAsync();

        return rooms
            .Select(r => r.ToDomain());
    }

    public async Task<Room?> GetByIdAsync(Guid id)
    {
        var room = await _context.Rooms
            .AsNoTracking()
            .Include(r => r.OwnerEntity)
            .FirstOrDefaultAsync(r => r.Id == id);

        return room?.ToDomain();
    }

    public async Task AddAsync(Room room)
    {
        await _context.Rooms
            .AddAsync(room.ToEntity());

        await _context
            .SaveChangesAsync();
    }

    public async Task UpdateAsync(Room room)
    {
        await _context.Rooms
            .Where(r => r.Id == room.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(r => r.Title, room.Title)
                .SetProperty(r => r.OwnerId, room.OwnerId)
                .SetProperty(r => r.IsActive, room.IsActive)
                .SetProperty(r => r.Description, room.Description));
    }

    public async Task DeleteAsync(Guid id)
    {
        await _context.Rooms
            .Where(r => r.Id == id)
            .ExecuteDeleteAsync();
    }
}
