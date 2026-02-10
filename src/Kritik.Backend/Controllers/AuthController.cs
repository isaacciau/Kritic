using Kritik.Backend.Services;
using Kritik.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kritik.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;

    public AuthController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        var user = await _userService.GetByUsernameAsync(request.Username);

        if (user is null || user.Password != request.Password)
        {
            return Unauthorized("Invalid credentials");
        }

        // TODO: Generate real JWT Token
        var token = $"fake-jwt-token-for-{user.Username}-{DateTime.UtcNow.Ticks}";

        return Ok(new LoginResponse
        {
            Token = token,
            FullName = user.FullName,
            Role = user.Role
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(User newUser)
    {
        var existing = await _userService.GetByUsernameAsync(newUser.Username);
        if (existing is not null)
        {
            return BadRequest("Username already exists");
        }

        await _userService.CreateAsync(newUser);
        return Ok("User registered successfully");
    }
}
