using BookingRoomsAPI.DataAccess.PostgreSQL.Filters;
using BookingRoomsAPI.DataAccess.PostgreSQL.Pages;
using BookingRoomsAPI.Domain.Entities;

namespace BookingRoomsAPI.DataAccess.PostgreSQL.Abstractions.Services;

public interface IBookingService
{
    Task AddBookingAsync(Booking booking);
    Task DeleteBookingAsync(Guid id);
    Task<IEnumerable<Booking>> GetAllBookingsAsync(BookingFilter filter, BookingPage page);
    Task<IEnumerable<Booking>> GetAllBookingsAsync();
    Task<Booking?> GetBookingByIdAsync(Guid id);
    Task UpdateBookingAsync(Booking booking);
}