using System.ComponentModel.DataAnnotations;

namespace BookingRoomsAPI.Contracts.Users;

public record UserLogin(
    string? Login,
    [EmailAddress(ErrorMessage = "Wrong email format")]
    string? Email,
    [Required(ErrorMessage = "Password is required")]
    string Password);
