using BookingRoomsAPI.DataAccess.PostgreSQL.Entities;
using BookingRoomsAPI.DataAccess.PostgreSQL.Filters;
using BookingRoomsAPI.DataAccess.PostgreSQL.Pages;

namespace BookingRoomsAPI.DataAccess.PostgreSQL.Extensions;

public static class BookingExtensions
{
    public static IQueryable<BookingEntity> UseFilter(this IQueryable<BookingEntity> query, BookingFilter filter)
    {
        if (filter.PricePerDayMin >= 0)
            query = query.Where(b => b.PricePerDay > filter.PricePerDayMin);

        if (filter.PricePerDayMax > 0)
            query = query.Where(b => b.PricePerDay < filter.PricePerDayMax);

        if (filter.PricePerMonthMin >= 0)
            query = query.Where(b => b.PricePerMonth > filter.PricePerMonthMin);

        if (filter.PricePerMonthMax > 0)
            query = query.Where(b => b.PricePerMonth < filter.PricePerMonthMax);

        if (filter.StartAt.HasValue)
            query = query.Where(b => b.StartAt >= filter.StartAt);

        if (filter.EndAt.HasValue)
            query = query.Where(b => b.EndAt <= filter.EndAt);

        if (filter.UserId.HasValue)
            query = query.Where(b => b.UserId == filter.UserId);

        if (filter.RoomId.HasValue)
            query = query.Where(b => b.RoomId == filter.RoomId);

        return query;
    }

    public static IQueryable<BookingEntity> UsePaging(this IQueryable<BookingEntity> query, BookingPage pageParams)
    {
        var page = pageParams.Page ?? 1;
        var pageSize = pageParams.PageSize ?? 10;

        var skip = (page - 1) * pageSize;
        return query.Skip(skip).Take(pageSize);
    }
}
