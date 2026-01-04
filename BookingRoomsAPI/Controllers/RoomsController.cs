using BookingRoomsAPI.Contracts.Rooms;
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
[Authorize(Roles = "User")]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;
	private readonly ILogger<RoomsController> _logger;

	public RoomsController(IRoomService roomService, ILogger<RoomsController> logger)
    {
        _roomService = roomService;
        _logger = logger;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<RoomGet>>> GetAllAsync([FromQuery] RoomFilter filter, RoomPage page)
    {
        try
        {
            var rooms = await _roomService.GetAllRoomsAsync(filter, page);

            var roomsDto = rooms
                .Select(r => new RoomGet(
                    r.Id,
                    r.IsActive,
                    r.Title,
                    r.Description));

            return Ok(roomsDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<RoomGet>> GetByIdAsync(Guid id)
    {
        try
        {
            var room = await _roomService.GetByIdAsync(id);

            if (room == null)
                return NotFound("Room not found");

            return Ok(new RoomGet(
                room.Id,
                room.IsActive,
                room.Title,
                room.Description));
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateRoomAsync([FromBody] RoomCreate roomCreate)
    {
        try
        {
            if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
                return Unauthorized();

            var roomResult = Room.Create(
                Guid.NewGuid(),
                userId,
                true,
                roomCreate.Title,
                roomCreate.Description
                );

            if(!roomResult.IsSuccess)
                return BadRequest(roomResult.Error);

            await _roomService.AddRoomAsync(roomResult.Value!);
            return Created();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAsync([FromBody] RoomUpdate roomUpdate)
    {
        try
        {
            if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
                return Unauthorized();

            var room = await _roomService.GetByIdAsync(roomUpdate.Id);

            if (room == null)
                return NotFound("Room not found");

            if (room.OwnerId != userId)
                return Forbid();

            var roomNewResult = Room.Create(
                room.Id,
                room.OwnerId,
                room.IsActive,
                roomUpdate.Title,
                roomUpdate.Description);

            if (!roomNewResult.IsSuccess)
                return BadRequest(roomNewResult.Error);

            await _roomService.UpdateRoomAsync(roomNewResult.Value!);
        
            return Ok();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        try
        {
            if(!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
                return Unauthorized();

            var room = await _roomService.GetByIdAsync(id);

            if (room == null)
                return NotFound("Room not found");

            if (room.OwnerId != userId)
                return Forbid();

            await _roomService.DeleteRoomAsync(id);

            return NoContent();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
