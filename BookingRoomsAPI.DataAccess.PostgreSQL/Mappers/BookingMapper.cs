using BookingRoomsAPI.DataAccess.PostgreSQL.Entities;
using BookingRoomsAPI.Domain.Entities;

namespace BookingRoomsAPI.DataAccess.PostgreSQL.Mappers;

public static class BookingMapper
{
    public static BookingEntity ToEntity(this Booking booking)
    {
        var bookingEntity = new BookingEntity
        {
            Id = booking.Id,
            StartAt = booking.StartAt,
            EndAt = booking.EndAt,
            PricePerDay = booking.PricePerDay,
            PricePerMonth = booking.PricePerMonth,
            UserId = booking.UserId,
            RoomId = booking.RoomId
        };

        return bookingEntity;
    }

    public static Booking ToDomain(this BookingEntity bookingEntity)
    {
        var booking = Booking.Create(
            bookingEntity.Id,
            bookingEntity.UserId,
            bookingEntity.RoomId,
            bookingEntity.StartAt,
            bookingEntity.EndAt,
            bookingEntity.PricePerDay,
            bookingEntity.PricePerMonth)
            .Value!;

        return booking;
    }
}
