using BookingRoomsAPI.DataAccess.PostgreSQL.Entities;
using BookingRoomsAPI.Domain.Entities;

namespace BookingRoomsAPI.DataAccess.PostgreSQL.Mappers;

public static class UserMapper
{
    public static UserEntity ToEntity(this User user)
    {
        List<BookingEntity> bookingEntities = new();
        List<RoomEntity> roomEntities = new();

        foreach (var booking in user.Bookings)
            bookingEntities.Add(new BookingEntity
            {
                Id = booking.Id,
                StartAt = booking.StartAt,
                EndAt = booking.EndAt,
                PricePerDay = booking.PricePerDay,
                PricePerMonth = booking.PricePerMonth,
                RoomId = booking.RoomId,
                UserId = booking.UserId
            });

        foreach (var room in user.Rooms)
            roomEntities.Add(new RoomEntity
            {
                Id = room.Id,
                Description = room.Description,
                IsActive = room.IsActive,
                Title = room.Title,
                OwnerId = room.OwnerId
            });

        var userEntity = new UserEntity
        {
            Id = user.Id,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            Name = user.Name,
            Login = user.Login,
            Rooms = roomEntities,
            Bookings = bookingEntities
        };

        return userEntity;
    }

    public static User ToDomain(this UserEntity userEntity)
    {
        var user = User.Create(
            userEntity.Id,
            userEntity.Name,
            userEntity.Login,
            userEntity.Email,
            userEntity.PasswordHash)
            .Value!;

        foreach (var bookingEntity in userEntity.Bookings)
            user.AddBooking(Booking.Create(
                bookingEntity.Id,
                bookingEntity.UserId,
                bookingEntity.RoomId,
                bookingEntity.StartAt,
                bookingEntity.EndAt,
                bookingEntity.PricePerDay,
                bookingEntity.PricePerMonth)
                .Value!);
        foreach (var roomEntity in userEntity.Rooms)
            user.AddRoom(Room.Create(
                roomEntity.Id,
                roomEntity.OwnerId,
                roomEntity.IsActive,
                roomEntity.Title,
                roomEntity.Description)
                .Value!);

        return user;
    }
}
