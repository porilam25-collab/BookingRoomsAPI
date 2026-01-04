using BookingRoomsAPI.DataAccess.PostgreSQL.Abstractions.Repositories;
using BookingRoomsAPI.DataAccess.PostgreSQL.Abstractions.Services;
using BookingRoomsAPI.DataAccess.PostgreSQL.Filters;
using BookingRoomsAPI.DataAccess.PostgreSQL.Pages;
using BookingRoomsAPI.Domain.Entities;

namespace BookingRoomsAPI.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingsRepository;

    public BookingService(IBookingRepository bookingsRepository)
    {
        _bookingsRepository = bookingsRepository;
    }

    public async Task<IEnumerable<Booking>> GetAllBookingsAsync(BookingFilter filter, BookingPage page)
    {
        return await _bookingsRepository.GetAllAsync(filter, page);
    }

    public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
    {
        return await _bookingsRepository.GetAllAsync();
    }

    public async Task<Booking?> GetBookingByIdAsync(Guid id)
    {
        return await _bookingsRepository.GetByIdAsync(id);
    }

    public async Task AddBookingAsync(Booking booking)
    {
        await _bookingsRepository.AddAsync(booking);
    }

    public async Task UpdateBookingAsync(Booking booking)
    {
        await _bookingsRepository.UpdateAsync(booking);
    }

    public async Task DeleteBookingAsync(Guid id)
    {
        await _bookingsRepository.DeleteAsync(id);
    }
}
