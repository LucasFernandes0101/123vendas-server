using _123vendas.Application.DTOs.Carts;
using _123vendas.Application.Mappers.Carts;
using _123vendas.Domain.Base;
using _123vendas.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace _123vendas_server.v1.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class CartsController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartsController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponseDTO<CartGetResponseDTO>>> GetAsync([FromQuery] CartGetRequestDTO request)
    {
        var pagedResult = await _cartService.GetAllAsync(request.Id,
                                                         request.UserId,
                                                         request.MinDate,
                                                         request.MaxDate,
                                                         request.Page,
                                                         request.Size,
                                                         request.OrderByClause);

        if (pagedResult?.Items is not null && pagedResult.Items.Any())
            return Ok(new PagedResponseDTO<CartGetResponseDTO>(pagedResult.Items.ToDTO(), pagedResult.Total, request.Page, request.Size));

        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CartGetDetailResponseDTO>> GetAsync([FromRoute] int id)
    {
        var cart = await _cartService.GetByIdAsync(id);

        if (cart is null)
            return NoContent();

        var response = cart.ToDetailDTO();

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<CartPostResponseDTO>> PostAsync([FromBody] CartPostRequestDTO request)
    {
        var createdCart = await _cartService.CreateAsync(request.ToEntity());

        var response = createdCart.ToPostResponseDTO();

        return Created(string.Empty, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CartPutResponseDTO>> PutAsync([FromRoute] int id, [FromBody] CartPutRequestDTO request)
    {
        var cart = await _cartService.UpdateAsync(id, request.ToEntity());

        return Ok(cart.ToPutResponseDTO());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _cartService.DeleteAsync(id);

        return NoContent();
    }
}