using _123vendas.Application.Commands.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace _123vendas_server.v1.Controllers;

public class AuthController
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /*
    [HttpPost]
    public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserRequest request, CancellationToken cancellationToken)
    {
        var validator = new AuthenticateUserRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<AuthenticateUserCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<AuthenticateUserResponse>
        {
            Success = true,
            Message = "User authenticated successfully",
            Data = _mapper.Map<AuthenticateUserResponse>(response)
        });
    }
    */
}