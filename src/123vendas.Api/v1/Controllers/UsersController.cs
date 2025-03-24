using _123vendas.Application.Users.CreateUser;
using _123vendas.Application.Users.GetUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace _123vendas_server.v1.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<CreateUserResult>> PostAsync([FromBody] CreateUserCommand command)
    {
        var result = await _mediator.Send(command);

        return Created(string.Empty, result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetUserResult>> GetByIdAsync(int id)
    {
        var command = new GetUserCommand(id);

        var result = await _mediator.Send(command);

        return Created(string.Empty, result);
    }
}