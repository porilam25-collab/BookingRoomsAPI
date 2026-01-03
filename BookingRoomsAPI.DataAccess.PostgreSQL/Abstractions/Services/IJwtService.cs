using BookingRoomsAPI.Domain.Entities;

namespace BookingRoomsAPI.DataAccess.PostgreSQL.Abstractions.Services;

public interface IJwtService
{
    string Generate(User user);
}