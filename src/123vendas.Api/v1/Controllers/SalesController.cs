using _123vendas.Application.DTOs.Sales;
using _123vendas.Application.Mappers.Sales;
using _123vendas.Domain.Base;
using _123vendas.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace _123vendas_server.v1.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class SalesController : ControllerBase
{
    private readonly ISaleService _saleService;

    public SalesController(ISaleService saleService)
    {
        _saleService = saleService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponseDTO<SaleGetResponseDTO>>> GetAsync([FromQuery] SaleGetRequestDTO request)
    {
        var pagedResult = await _saleService.GetAllAsync(request.Id,
                                                   request.BranchId,
                                                   request.CustomerId,
                                                   request.Status,
                                                   request.StartDate,
                                                   request.EndDate,
                                                   request.Page,
                                                   request.Size,
                                                   request.OrderByClause);

        if (pagedResult?.Items is not null && pagedResult.Items.Any())
            return Ok(new PagedResponseDTO<SaleGetResponseDTO>(pagedResult.Items.ToDTO(), pagedResult.Total, request.Page, request.Size));

        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SaleGetDetailResponseDTO>> GetAsync([FromRoute] int id)
    {
        var sale = await _saleService.GetByIdAsync(id);

        if (sale is null)
            return NoContent();

        var response = sale.ToDetailDTO();
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<SalePostResponseDTO>> PostAsync([FromBody] SalePostRequestDTO request)
    {
        var createdSale = await _saleService.CreateAsync(request.ToEntity());
        var response = createdSale.ToPostResponseDTO();
        return Created(string.Empty, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] SalePutRequestDTO request)
    {
        var sale = await _saleService.UpdateAsync(id, request.ToEntity());
        return Ok(sale.ToPutResponseDTO());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _saleService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> CancelAsync([FromRoute] int id)
    {
        await _saleService.CancelAsync(id);

        return NoContent();
    }

    [HttpPut("{id}/Items/{sequence}/cancel")]
    public async Task<IActionResult> CancelItemAsync([FromRoute] int id, [FromRoute] int sequence)
    {
        var sale = await _saleService.CancelItemAsync(id, sequence);

        return Ok(sale.ToDetailDTO());
    }

    [HttpGet("{id}/Items/{sequence}")]
    public async Task<IActionResult> GetItemAsync([FromRoute] int id, [FromRoute] int sequence)
    {
        var saleItem = await _saleService.GetItemAsync(id, sequence);

        return Ok(saleItem.ToDetailDTO());
    }
}