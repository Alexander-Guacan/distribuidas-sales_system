using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BusinessLogicLayer;
using Entities;

namespace RestServiceLayer;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthLogic _authLogic;
    private readonly UserLogic _userLogic;
    private readonly FailedLoginAttemptLogic _failedLoginAttemptLogic;

    public AuthController()
    {
        _authLogic = new AuthLogic();
        _userLogic = new UserLogic();
        _failedLoginAttemptLogic = new FailedLoginAttemptLogic();
    }

    [HttpPost("login")]
public async Task<IActionResult> Login([FromBody] User loginRequest)
{
    if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
    {
        return BadRequest("Invalid login request.");
    }

    try
    {
        string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        bool isAuthenticated = await _authLogic.ValidateLoginAsync(loginRequest.Email, loginRequest.Password, ipAddress);

        if (!isAuthenticated)
        {
            return Unauthorized(new { Message = "Invalid credentials." });
        }

        return Ok(new { Message = "Login successful." });
    }
    catch (Exception ex)
    {
        // Registra el error (si tienes un sistema de logging configurado)
        return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
    }
}

    [HttpPost("register")]
    public IActionResult Register([FromBody] User user)
    {
        if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
        {
            return BadRequest("Invalid registration request.");
        }

        try
        {
            var createdUser = _userLogic.Create(user);
            if (createdUser == null)
            {
                return Conflict("User with the same email or username already exists.");
            }

            return Ok(new { Message = "User registered successfully." });
        }
        catch (Exception ex)
        {
            // Log the exception (if logging is configured)
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpPost("unlock-user")]
    public IActionResult UnlockUser([FromBody] int userId)
    {
        try
        {
            bool result = _userLogic.UnlockUser(userId);
            if (!result)
            {
                return NotFound("User not found or unable to unlock.");
            }

            return Ok(new { Message = "User unlocked successfully." });
        }
        catch (Exception ex)
        {
            // Log the exception (if logging is configured)
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}