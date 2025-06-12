using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketsManager.Business.UserManagement;
using TicketsManager.Common.RequestDtos;

namespace TicketsManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private IUserManagement userManagement;

    public UserController(IUserManagement userManagement)
    {
        this.userManagement = userManagement;
    }

    [HttpPost("register")]
    public IActionResult RegisterUser([FromBody] RegisterUserDto request)
    {
        var userResult = userManagement.RegisterUser(request);

        if (!userResult.IsSuccess)
            return BadRequest(userResult.ErrorMessage);

        return Ok(userResult.Value);
    }

    [HttpPost("login")]
    public IActionResult LoginUser([FromBody] LoginUserDto request)
    {
        var loginResult = userManagement.LoginUser(request);

        if(!loginResult.IsSuccess)
            return BadRequest(loginResult.ErrorMessage);

        return Ok(loginResult.Value);
    }
}
