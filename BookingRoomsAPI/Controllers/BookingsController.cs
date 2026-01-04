using BookingRoomsAPI.Contracts.Bookings;
using BookingRoomsAPI.DataAccess.PostgreSQL.Abstractions.Services;
using BookingRoomsAPI.DataAccess.PostgreSQL.Filters;
using BookingRoomsAPI.DataAccess.PostgreSQL.Pages;
using BookingRoomsAPI.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingRoomsAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;
    private readonly IUserService _userService;
    private readonly ILogger<BookingsController> _logger;

    public BookingsController(IBookingService bookingService, ILogger<BookingsController> logger, IUserService userService)
    {
        _bookingService = bookingService;
        _logger = logger;
        _userService = userService;
    }

    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<BookingGetForAdmins>>> GetAllAsync([FromQuery] BookingFilter filter, [FromQuery] BookingPage page)
    {
        try
        {
            var bookings = await _bookingService.GetAllBookingsAsync(filter, page);

            var bookingsDto = bookings
                .Select(b => new BookingGetForAdmins(
                    b.Id,
                    b.UserId,
                    b.RoomId,
                    b.StartAt,
                    b.EndAt,
                    b.PricePerDay,
                    b.PricePerMonth,
                    b.TotalPrice));

            return Ok(bookingsDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }   
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<BookingGet>> GetUsersBookingAsync()
    {
        try
        {
            if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
                return Unauthorized();

            var user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
                return Unauthorized();

            var bookings = user.Bookings
                .Select(b => new BookingGet(
                    b.UserId,
                    b.RoomId,
                    b.StartAt,
                    b.EndAt,
                    b.TotalPrice));

            return Ok(bookings);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> CreateBookingAsync([FromBody] BookingCreate bookingCreate)
    {
        try
        {
            if(!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
                return Unauthorized();

            if (await _userService.GetUserByIdAsync(userId) == null)
                return Unauthorized();

            var bookingResult = Booking.Create(
                Guid.NewGuid(),
                userId,
                bookingCreate.RoomId,
                bookingCreate.StartAt,
                bookingCreate.EndAt,
                bookingCreate.PricePerDay,
                bookingCreate.PricePerMonth);

            if(!bookingResult.IsSuccess)
                return BadRequest(bookingResult.Error);

            await _bookingService.AddBookingAsync(bookingResult.Value!);
            return Created();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete]
    [Authorize]
    public async Task<ActionResult> DeleteBookingAsync([FromBody] BookingDelete bookingDelete)
    {
        try
        {
            if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
                return Unauthorized();

            var bookings = await _bookingService.GetAllBookingsAsync();
            var booking = bookings.FirstOrDefault(b => b.RoomId == bookingDelete.RoomId);

            if (booking == null)
                return BadRequest();

            if (booking.UserId != userId)
                return Forbid();

            await _bookingService.DeleteBookingAsync(booking.Id);

            return NoContent();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
