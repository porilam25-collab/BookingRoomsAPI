using BookingRoomsAPI.DataAccess.PostgreSQL.Entities;
using BookingRoomsAPI.DataAccess.PostgreSQL.Filters;
using BookingRoomsAPI.DataAccess.PostgreSQL.Pages;

namespace BookingRoomsAPI.DataAccess.PostgreSQL.Extensions;

public static class RoomExtensions
{
    public static IQueryable<RoomEntity> UseFilter(this IQueryable<RoomEntity> query, RoomFilter filter)
    {
        if (!string.IsNullOrEmpty(filter.Title))
            query = query.Where(r => r.Title.Contains(filter.Title));

        if (!string.IsNullOrEmpty(filter.Description))
            query = query.Where(r => r.Description.Contains(filter.Description));

        if (filter.IsActive.HasValue)
            query = query.Where(r => r.IsActive == filter.IsActive);

        if (filter.OwnerId.HasValue)
            query = query.Where(r => r.OwnerId == filter.OwnerId);

        return query;
    }

    public static IQueryable<RoomEntity> UsePaging(this IQueryable<RoomEntity> query, RoomPage pageParams)
    {
        var page = pageParams.Page ?? 1;
        var pageSize = pageParams.PageSize ?? 10;

        var skip = (page - 1) * pageSize;

        return query.Skip(skip).Take(pageSize);
    }
}
