using _123vendas.Application.Commands.Users;
using _123vendas.Application.Mappers.Users;
using _123vendas.Application.DTOs.Users;
using _123vendas.Application.Results.Users;
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
    public async Task<ActionResult<UserPostResponseDTO>> PostAsync([FromBody] UserPostRequestDTO dto)
    {
        var command = dto.ToCommand();

        var result = await _mediator.Send(command);

        var response = result?.ToPostResponse();

        return Created(string.Empty, response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserGetResponseDTO>> GetByIdAsync(int id)
    {
        var command = new GetUserCommand(id);

        var result = await _mediator.Send(command);

        var response = result?.ToGetResponse();

        return Created(string.Empty, response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteByIdAsync(int id)
    {
        var command = new DeleteUserCommand(id);

        await _mediator.Send(command);

        return NoContent();
    }
}