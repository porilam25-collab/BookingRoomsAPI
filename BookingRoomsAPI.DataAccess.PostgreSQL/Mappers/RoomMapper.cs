using BookingRoomsAPI.DataAccess.PostgreSQL.Entities;
using BookingRoomsAPI.Domain.Entities;

namespace BookingRoomsAPI.DataAccess.PostgreSQL.Mappers;

public static class RoomMapper
{
    public static RoomEntity ToEntity(this Room room)
    {
        var roomEntity = new RoomEntity
        {
            Id = room.Id,
            Title = room.Title,
            Description = room.Description,
            IsActive = room.IsActive,
            OwnerId = room.OwnerId
        };

        return roomEntity;
    }

    public static Room ToDomain(this RoomEntity roomEntity)
    {
        var room = Room.Create(
            roomEntity.Id,
            roomEntity.OwnerId,
            roomEntity.IsActive,
            roomEntity.Title,
            roomEntity.Description)
            .Value!;

        return room;
    }
}
