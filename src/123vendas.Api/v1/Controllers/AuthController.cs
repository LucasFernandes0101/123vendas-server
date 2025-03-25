using _123vendas.Application.DTOs.Auth;
using _123vendas.Application.Mappers.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace _123vendas_server.v1.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<AuthenticateUserResponseDTO>> AuthenticateUser([FromBody] AuthenticateUserRequestDTO request)
    {
        var command = request.ToCommand();

        var result = await _mediator.Send(command);

        var response = result.ToResponseDTO();

        return Ok(response);
    }
}