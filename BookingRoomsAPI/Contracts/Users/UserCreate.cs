using System.ComponentModel.DataAnnotations;

namespace BookingRoomsAPI.Contracts.Users;

public record UserCreate(
    [StringLength(50, ErrorMessage = "Name can`t be longer than 50 symbols")]
    string Name,
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Wrong email format")]
    string Email,
    [Required(ErrorMessage = "Login is required")]
    [StringLength(100, ErrorMessage = "Login can`t be longer than 100 symbols")]
    string Login,
    [Required(ErrorMessage = "Password is required")]
    [StringLength(50, MinimumLength = 8, ErrorMessage = "Password long need be between 8-50")]
    string Password);
