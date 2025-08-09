using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketsManager.API.Request;
using TicketsManager.Business.Actions.Users;

namespace TicketsManager.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator mediator;

    public UserController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
    {
        var createdUser = await mediator.Send(new CreateUserCommand
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Username = request.Username,
            Email = request.Email,
            Password = request.Password
        });

        return Ok(createdUser.User);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserRequest request)
    {
        var loginResponse = await mediator.Send(new LoginUserCommand
        {
            Username = request.Username,
            Password = request.Password
        });
        if (loginResponse == null)
        {
            return Unauthorized();
        }
        return Ok(loginResponse);
    }

    [HttpPost("token-refresh")]
    public async Task<IActionResult> AccessTokenRefresh([FromBody] TokenRefreshRequest request)
    {
        var refreshTokenResponse = await mediator.Send(new AccessTokenRefreshCommand
        {
            userId = request.UserId,
            RefreshToken = request.RefreshToken
        });
        if (refreshTokenResponse == null)
        {
            return BadRequest("Invalid refresh token or user not found.");
        }
        return Ok(refreshTokenResponse);
    }

    [Authorize]
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        var queryResponse = await mediator.Send(new GetUserByIdQuery { UserId = userId });

        if (queryResponse == null)
        {
            return NotFound();
        }
        return Ok(queryResponse.UserDto);
    }

    [Authorize]
    [HttpPost("update-user")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
    {
        var updateUserResponse = await mediator.Send(new UpdateUserCommand
        {
            UserId = request.UserId,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Username = request.Username
        });

        if (updateUserResponse == null)
            return NotFound("User not found.");

        return Ok(updateUserResponse.User);
    }
}
