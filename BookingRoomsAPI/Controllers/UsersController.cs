using BookingRoomsAPI.Contracts.Users;
using BookingRoomsAPI.DataAccess.PostgreSQL.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingRoomsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UsersController> _logger;

	public UsersController(IUserService userService, ILogger<UsersController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUserAsync([FromBody] UserUpdate userUpdate)
    {
        try
        {
            if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid id))
                return Unauthorized();

            var userResult = Domain.Entities.User.Create(
                id,
                userUpdate.Name,
                userUpdate.Login,
                userUpdate.Email,
                "secret_temporary_password");

            if (!userResult.IsSuccess)
                return BadRequest(userResult.Error);

            await _userService.UpdateUserAsync(userResult.Value!);
            
            return Ok();
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
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
