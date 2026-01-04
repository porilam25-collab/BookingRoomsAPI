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

	public AuthController(IUserService userService, IJwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegistrationAsync(UserCreate userCreate)
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

    [HttpPost("login")]
    public async Task<ActionResult> LoginAsync(UserLogin userLogin)
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
}
