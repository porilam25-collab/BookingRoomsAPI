using BookingRoomsAPI.DataAccess.PostgreSQL.Abstractions.Repositories;
using BookingRoomsAPI.DataAccess.PostgreSQL.Extensions;
using BookingRoomsAPI.DataAccess.PostgreSQL.Filters;
using BookingRoomsAPI.DataAccess.PostgreSQL.Mappers;
using BookingRoomsAPI.DataAccess.PostgreSQL.Pages;
using BookingRoomsAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingRoomsAPI.DataAccess.PostgreSQL.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly BookingRoomsDbContext _context;

    public UsersRepository(BookingRoomsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync(UserFilter filter, UserPage page)
    {
        var userEntities = await _context
            .Users
            .AsSplitQuery()
            .Include(u => u.Rooms)
            .Include(u => u.Bookings)
            .AsNoTracking()
            .UseFilter(filter)
            .UsePaging(page)
            .ToListAsync();

        return userEntities
            .Select(u => u.ToDomain());
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        var userEntity = await _context
            .Users
            .AsSplitQuery()
            .Include(u => u.Rooms)
            .Include(u => u.Bookings)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        return userEntity?.ToDomain();
    }

    public async Task<User?> GetByLoginAsync(string login)
    {
        var userEntity = await _context
            .Users
            .AsSplitQuery()
            .Include(u => u.Rooms)
            .Include(u => u.Bookings)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Login == login);

        return userEntity?.ToDomain();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var userEntity = await _context
            .Users
            .AsSplitQuery()
            .Include(u => u.Rooms)
            .Include(u => u.Bookings)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);

        return userEntity?.ToDomain();
    }

    public async Task AddUserAsync(User user)
    {
        await _context
            .Users
            .AddAsync(user.ToEntity());

        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        await _context
            .Users
            .Where(u => u.Id == user.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.Email, user.Email)
                .SetProperty(u => u.Login, user.Login)
                .SetProperty(u => u.Name, user.Name));
    }

    public async Task DeleteAsync(Guid id)
    {
        await _context
           .Users
           .Where(u => u.Id == id)
           .ExecuteDeleteAsync();
    }
}

