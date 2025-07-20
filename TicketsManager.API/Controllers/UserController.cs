using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicketsManager.API.Request;
using TicketsManager.Business.Actions.Users;
using TicketsManager.Common.Dto;

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
        return Ok(loginResponse.User);
    }
}
