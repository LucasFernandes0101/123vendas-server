﻿using _123vendas.Application.DTOs.Carts;
using _123vendas.Application.DTOs.Common;
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

    /// <summary>
    /// Retrieves a paginated list of carts based on the provided filters.
    /// </summary>
    /// <param name="request">The filter parameters for the cart list.</param>
    /// <returns>A paginated response with the list of carts.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResponseDTO<CartGetResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
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

    /// <summary>
    /// Retrieves detailed information of a specific cart by its ID.
    /// </summary>
    /// <param name="id">The ID of the cart.</param>
    /// <returns>The detailed cart information.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CartGetDetailResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CartGetDetailResponseDTO>> GetAsync([FromRoute] int id)
    {
        var cart = await _cartService.GetByIdAsync(id);

        if (cart is null)
            return NoContent();

        var response = cart.ToDetailDTO();

        return Ok(response);
    }

    /// <summary>
    /// Creates a new shopping cart.
    /// </summary>
    /// <param name="request">The data for the new cart.</param>
    /// <returns>The created cart with response data.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CartPostResponseDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CartPostResponseDTO>> PostAsync([FromBody] CartPostRequestDTO request)
    {
        var createdCart = await _cartService.CreateAsync(request.ToEntity());

        var response = createdCart.ToPostResponseDTO();

        return Created(string.Empty, response);
    }

    /// <summary>
    /// Updates the details of an existing cart by its ID.
    /// </summary>
    /// <param name="id">The ID of the cart to update.</param>
    /// <param name="request">The updated cart data.</param>
    /// <returns>The updated cart with response data.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CartPutResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CartPutResponseDTO>> PutAsync([FromRoute] int id, [FromBody] CartPutRequestDTO request)
    {
        var cart = await _cartService.UpdateAsync(id, request.ToEntity());

        return Ok(cart.ToPutResponseDTO());
    }

    /// <summary>
    /// Deletes a cart by its ID.
    /// </summary>
    /// <param name="id">The ID of the cart to delete.</param>
    /// <returns>No content if deletion is successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _cartService.DeleteAsync(id);

        return NoContent();
    }
}