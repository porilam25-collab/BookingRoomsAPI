using BookingRoomsAPI.DataAccess.PostgreSQL.Entities;
using BookingRoomsAPI.DataAccess.PostgreSQL.Filters;
using BookingRoomsAPI.DataAccess.PostgreSQL.Pages;

namespace BookingRoomsAPI.DataAccess.PostgreSQL.Extensions;

public static class UserExtenstions
{
    public static IQueryable<UserEntity> UseFilter(this IQueryable<UserEntity> query, UserFilter filter)
    {
        if (!string.IsNullOrEmpty(filter.Email))
            query = query.Where(u => u.Email == filter.Email);

        if (!string.IsNullOrEmpty(filter.Name))
            query = query.Where(u => u.Name.Contains(filter.Name));

        if (!string.IsNullOrEmpty(filter.Login))
            query = query.Where(u => u.Login == filter.Login);

        return query;
    }

    public static IQueryable<UserEntity> UsePaging(this IQueryable<UserEntity> query, UserPage pageParams)
    {
        var page = pageParams.Page ?? 1;
        var pageSize = pageParams.PageSize ?? 10;

        var skip = (page - 1) * pageSize;

        return query.Skip(skip).Take(pageSize);
    }
}
