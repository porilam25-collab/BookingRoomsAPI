using BookingRoomsAPI.Domain.Common;

namespace BookingRoomsAPI.Domain.Entities;

public class Room
{
    public const int MAX_TITLE_LENGTH = 200;
    public const int MAX_DESCRIPTION_LENGTH = 1000;

    public Guid Id { get; }
    public Guid OwnerId { get; }
    public bool IsActive { get; }
    public string Title { get; }
    public string Description { get; }

    private Room(Guid id, Guid ownerId, bool isActive, string title, string description)
    {
        Id = id;
        OwnerId = ownerId;
        IsActive = isActive;
        Title = title;
        Description = description;
    }

    public static Result<Room> Create(Guid id, Guid ownerId, bool isActive, string title, string description)
    {
        var res = new Result<Room>();

        if (string.IsNullOrEmpty(title) || title.Length > MAX_TITLE_LENGTH)
            res.Failured($"Title can`t be null or longer then {MAX_TITLE_LENGTH} symbols");

        if (string.IsNullOrEmpty(description) || description.Length > MAX_DESCRIPTION_LENGTH)
            res.Failured($"Description can`t be null or longer then {MAX_DESCRIPTION_LENGTH} symbols");

        if (!string.IsNullOrEmpty(res.Error))
            return res;

        res.Success(new Room(id, ownerId, isActive, title, description));

        return res;
    }
}
