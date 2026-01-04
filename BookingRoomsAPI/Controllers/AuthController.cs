using BookingRoomsAPI.Contracts.Users;
using BookingRoomsAPI.DataAccess.PostgreSQL.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingRoomsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;
    private readonly ILogger<AuthController> _logger;

	public AuthController(IUserService userService, IJwtService jwtService, ILogger<AuthController> logger)
    {
        _userService = userService;
        _jwtService = jwtService;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegistrationAsync(UserCreate userCreate)
    {
        try
        {
            var userResult = Domain.Entities.User.Create(
                Guid.NewGuid(),
                userCreate.Name,
                userCreate.Login,
                userCreate.Email,
                "try");

            if (!userResult.IsSuccess)
                return BadRequest(userResult.Error);

            await _userService.RegistrationAsync(userResult.Value!, userCreate.Password);

            return Created();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult> LoginAsync(UserLogin userLogin)
    {
        try
        {
            var user = await _userService.LoginAsync(userLogin.Password, userLogin.Email, userLogin.Login);

            if (user == null)
                return Unauthorized();

            var token = _jwtService.Generate(user);

            Response.Cookies.Append("tasty-cookies", token, new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddHours(12),
                SameSite = SameSiteMode.Strict
            });

            return Ok();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
