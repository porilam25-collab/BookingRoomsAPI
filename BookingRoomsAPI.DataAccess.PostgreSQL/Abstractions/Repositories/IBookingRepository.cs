using BookingRoomsAPI.DataAccess.PostgreSQL.Filters;
using BookingRoomsAPI.DataAccess.PostgreSQL.Pages;
using BookingRoomsAPI.Domain.Entities;

namespace BookingRoomsAPI.DataAccess.PostgreSQL.Abstractions.Repositories
{
    public interface IBookingRepository
    {
        Task AddAsync(Booking booking);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Booking>> GetAllAsync(BookingFilter filter, BookingPage page);
        Task<Booking?> GetByIdAsync(Guid id);
        Task UpdateAsync(Booking booking);
    }
}