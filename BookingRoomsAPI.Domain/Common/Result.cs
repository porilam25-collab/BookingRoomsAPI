namespace BookingRoomsAPI.Domain.Common;

public class Result<T>
{
    public bool IsSuccess { get; private set; }
    public string? Error { get; private set; }
    public T? Value { get; private set; }

    public void Failured(string error)
    {
        Error += error;
        IsSuccess = false;
    }

    public void Success(T value)
    {
        Value = value;
        IsSuccess = true;
    }
}
