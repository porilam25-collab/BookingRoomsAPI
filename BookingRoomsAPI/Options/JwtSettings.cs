namespace BookingRoomsAPI.Options;

public class JwtSettings
{
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int ExpiresInMinutes { get; set; }
    public string SecretKey { get; set; } = null!;
}
