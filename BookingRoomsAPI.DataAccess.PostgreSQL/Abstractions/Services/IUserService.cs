using BookingRoomsAPI.DataAccess.PostgreSQL.Filters;
using BookingRoomsAPI.DataAccess.PostgreSQL.Pages;
using BookingRoomsAPI.Domain.Entities;

namespace BookingRoomsAPI.DataAccess.PostgreSQL.Abstractions.Services;

public interface IUserService
{
    Task DeleteUserAsync(Guid id);
    Task<IEnumerable<User>> GetAllUsersAsync(UserFilter filter, UserPage pageParams);
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User> RegistrationAsync(User user, string password);
    Task<User?> LoginAsync(string password, string? email = null, string? login = null);
    Task UpdateUserAsync(User user);
}