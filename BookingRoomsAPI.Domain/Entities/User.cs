using BookingRoomsAPI.Domain.Common;

namespace BookingRoomsAPI.Domain.Entities;

public class User
{
    private string _passwordHash;

    public const int MAX_NAME_LENGTH = 50;
    public const int MAX_LOGIN_LENGTH = 100;

    private List<Booking> _bookings = new();
    private List<Room> _rooms = new();

    public Guid Id { get; }
    public string Name { get; }
    public string Login { get; }
    public string Email { get; }
    public string PasswordHash { get => _passwordHash; }
    public IReadOnlyCollection<Booking> Bookings { get => _bookings; }
    public IReadOnlyCollection<Room> Rooms { get => _rooms; }

    private User(Guid id, string name, string login, string email, string passwordHash)
    {
        Id = id;
        Name = name;
        Login = login;
        Email = email;
        _passwordHash = passwordHash;
    }

    public void AddBooking(Booking booking)
    {
        _bookings.Add(booking);
    }

    public void AddRoom(Room room)
    {
        _rooms.Add(room);
    }

    public void SetPasswordHash(string passwordHash)
    {
        _passwordHash = passwordHash;
    }

    public static Result<User> Create(Guid id, string name, string login, string email, string passwordHash)
    {
        var res = new Result<User>();

        if (string.IsNullOrEmpty(name) || name.Length > MAX_NAME_LENGTH)
            res.Failured($"Name can`t be null or longer then {MAX_NAME_LENGTH} symbols");

        if (string.IsNullOrEmpty(login) || login.Length > MAX_LOGIN_LENGTH)
            res.Failured($"Login can`t be null or longer then {MAX_LOGIN_LENGTH} symbols");

        if (string.IsNullOrEmpty(email))
            res.Failured("Email can`t be null");
        if (!email.Contains('@') || !email.Contains('.'))
            res.Failured("Wrond format email");

        if (!string.IsNullOrEmpty(res.Error))
            return res;

        res.Success(new User(id, name, login, email, passwordHash));

        return res;
    }
}
