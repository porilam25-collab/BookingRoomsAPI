using BookingRoomsAPI.Contracts.Bookings;
using BookingRoomsAPI.Contracts.Rooms;
using BookingRoomsAPI.Contracts.Users;
using BookingRoomsAPI.DataAccess.PostgreSQL.Abstractions.Services;
using BookingRoomsAPI.DataAccess.PostgreSQL.Filters;
using BookingRoomsAPI.DataAccess.PostgreSQL.Pages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingRoomsAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Admin")]
public class AdminPanelController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<AdminPanelController> _logger;

    public AdminPanelController(IUserService userService, ILogger<AdminPanelController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserGetDto>>> GetAllAsync([FromQuery] UserFilter filter, [FromQuery] UserPage page)
    {
        try
        {
            var users = await _userService.GetAllUsersAsync(filter, page);

            List<UserGetDto> usersDto = new();

            foreach (var user in users)
            {
                List<BookingGetForAdmins> bookings = new();
                List<RoomGetForAdmins> rooms = new();

                foreach (var booking in user.Bookings)
                    bookings.Add(new BookingGetForAdmins(
                        booking.Id,
                        booking.UserId,
                        booking.RoomId,
                        booking.StartAt,
                        booking.EndAt,
                        booking.PricePerDay,
                        booking.PricePerMonth,
                        booking.TotalPrice));

                foreach (var room in user.Rooms)
                {
                    rooms.Add(new RoomGetForAdmins(
                        room.Id,
                        room.OwnerId,
                        room.IsActive,
                        room.Title,
                        room.Description));
                }

                usersDto.Add(new UserGetDto(
                    user.Id,
                    user.Email,
                    user.Login,
                    user.Name,
                    rooms,
                    bookings));
            }

            return Ok(usersDto);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteUserAsync([FromBody] Guid id)
    {
        try
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

    }
}
