using System.ComponentModel.DataAnnotations;

namespace BookingRoomsAPI.Contracts.Rooms;

public record RoomUpdate(
    [Required(ErrorMessage = "Id is required")]
    Guid Id,
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, ErrorMessage = "Title can`t be longer than 200 symbols")]
    string Title,
    [Required(ErrorMessage = "Description is required")]
    [StringLength(1000, ErrorMessage = "Description can`t be longer than 1000 symbols")]
    string Description);
