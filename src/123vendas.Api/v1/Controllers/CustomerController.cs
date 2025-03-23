using _123vendas.Application.DTOs.Customers;
using _123vendas.Application.Mappers.Customers;
using _123vendas.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace _123vendas_server.v1.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerGetResponseDTO>>> GetAsync([FromQuery] CustomerGetRequestDTO request)
    {
        var customers = await _customerService.GetAllAsync(request.Id,
                                                        request.Name,
                                                        request.Document,
                                                        request.Phone,
                                                        request.Email,
                                                        request.IsActive,
                                                        request.StartDate,
                                                        request.EndDate,
                                                        request.Page,
                                                        request.Size,
                                                        request.OrderByClause);

        var response = customers.ToDTO();

        if (response is not null && response.Any())
            return Ok(response);

        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerGetDetailResponseDTO>> GetAsync([FromRoute] int id)
    {
        var customer = await _customerService.GetByIdAsync(id);

        if (customer is null)
            return NoContent();

        var response = customer.ToDetailDTO();
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerPostResponseDTO>> PostAsync([FromBody] CustomerPostRequestDTO request)
    {
        var createdCustomer = await _customerService.CreateAsync(request.ToEntity());
        var response = createdCustomer.ToPostResponseDTO();
        return Created(string.Empty, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] CustomerPutRequestDTO request)
    {
        var customer = await _customerService.UpdateAsync(id, request.ToEntity());
        return Ok(customer.ToPutResponseDTO());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _customerService.DeleteAsync(id);
        return NoContent();
    }
}