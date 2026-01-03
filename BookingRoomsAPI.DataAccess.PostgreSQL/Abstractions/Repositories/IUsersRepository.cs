using BookingRoomsAPI.DataAccess.PostgreSQL.Filters;
using BookingRoomsAPI.DataAccess.PostgreSQL.Pages;
using BookingRoomsAPI.Domain.Entities;

namespace BookingRoomsAPI.DataAccess.PostgreSQL.Abstractions.Repositories;

public interface IUsersRepository
{
    Task AddUserAsync(User user);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<User>> GetAllAsync(UserFilter filter, UserPage page);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByLoginAsync(string login);
    Task UpdateUserAsync(User user);
}