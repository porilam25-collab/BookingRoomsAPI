using BookingRoomsAPI.DataAccess.PostgreSQL.Abstractions.Repositories;
using BookingRoomsAPI.DataAccess.PostgreSQL.Extensions;
using BookingRoomsAPI.DataAccess.PostgreSQL.Filters;
using BookingRoomsAPI.DataAccess.PostgreSQL.Mappers;
using BookingRoomsAPI.DataAccess.PostgreSQL.Pages;
using BookingRoomsAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingRoomsAPI.DataAccess.PostgreSQL.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly BookingRoomsDbContext _context;

    public BookingRepository(BookingRoomsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Booking>> GetAllAsync(BookingFilter filter, BookingPage page)
    {
        var bookingEntities = await _context.Bookings
            .AsNoTracking()
            .Include(b => b.UserEntity)
            .Include(b => b.RoomEntity)
            .UseFilter(filter)
            .UsePaging(page)
            .ToListAsync();

        return bookingEntities
            .Select(b => b.ToDomain());
    }

    public async Task<IEnumerable<Booking>> GetAllAsync()
    {
        var bookingEntities = await _context.Bookings
            .AsNoTracking()
            .Include(b => b.UserEntity)
            .Include(b => b.RoomEntity)
            .ToListAsync();

        return bookingEntities
            .Select(b => b.ToDomain());
    }

    public async Task<Booking?> GetByIdAsync(Guid id)
    {
        var bookingEntity = await _context.Bookings
            .AsNoTracking()
            .Include(b => b.UserEntity)
            .Include(b => b.RoomEntity)
            .FirstOrDefaultAsync(b => b.Id == id);

        return bookingEntity?.ToDomain();
    }

    public async Task AddAsync(Booking booking)
    {
        await _context.Bookings
            .AddAsync(booking.ToEntity());

        await _context
            .SaveChangesAsync();
    }

    public async Task UpdateAsync(Booking booking)
    {
        await _context.Bookings
            .Where(b => b.Id == booking.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.PricePerDay, booking.PricePerDay)
                .SetProperty(b => b.PricePerMonth, booking.PricePerMonth)
                .SetProperty(b => b.EndAt, booking.EndAt)
                .SetProperty(b => b.RoomId, booking.RoomId));
    }

    public async Task DeleteAsync(Guid id)
    {
        await _context.Bookings
            .Where(b => b.Id == id)
            .ExecuteDeleteAsync();
    }
}
