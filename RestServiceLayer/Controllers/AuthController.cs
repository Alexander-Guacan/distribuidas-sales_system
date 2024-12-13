using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer;
using Entities;

namespace RestServiceLayer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("Login")]
    public IActionResult Login(User user)
    {
        try
        {

            var authLogic = new AuthLogic();
            string? ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            if (string.IsNullOrEmpty(ipAddress))
            {
                throw new Exception("Ip adress could not be resolve");
            }

            User result = authLogic.Login(user, ipAddress);
            return Ok(result);
        }
        catch (Exception exception)
        {
            return BadRequest(new { message = $"Login failed: {exception.Message}" });
        }
    }
}