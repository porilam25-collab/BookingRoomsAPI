using BookingRoomsAPI.DataAccess.PostgreSQL.Abstractions.Repositories;
using BookingRoomsAPI.DataAccess.PostgreSQL.Abstractions.Services;
using BookingRoomsAPI.DataAccess.PostgreSQL.Filters;
using BookingRoomsAPI.DataAccess.PostgreSQL.Pages;
using BookingRoomsAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookingRoomsAPI.Services;

public class UserService : IUserService
{
    private readonly IUsersRepository _usersRepository;
    private readonly PasswordHasher<User> _passwordHasher;

    public UserService(IUsersRepository usersRepository, PasswordHasher<User> passwordHasher)
    {
        _usersRepository = usersRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync(UserFilter filter, UserPage pageParams)
    {
        return await _usersRepository.GetAllAsync(filter, pageParams);
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _usersRepository.GetByIdAsync(id);
    }

    public async Task UpdateUserAsync(User user)
    {
        await _usersRepository.UpdateUserAsync(user);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        await _usersRepository.DeleteAsync(id);
    }

    public async Task<User> RegistrationAsync(User user, string password)
    {
        string passwordHash = _passwordHasher.HashPassword(user, password);
        user.SetPasswordHash(passwordHash);
        await _usersRepository.AddUserAsync(user);

        return user;
    }

    public async Task<bool> TryLoginAsync(string password, string? email = null, string? login = null)
    {
        if (email == null && login == null)
            throw new ArgumentNullException("Login and email not specified");

        var user = email == null
            ? await _usersRepository.GetByLoginAsync(login!)
            : await _usersRepository.GetByEmailAsync(email);

        if (user == null)
            return false;

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

        if (result == PasswordVerificationResult.Success)
            return true;

        return false;
    }
}
