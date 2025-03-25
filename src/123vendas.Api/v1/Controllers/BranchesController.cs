using _123vendas.Application.DTOs.Branches;
using _123vendas.Application.Mappers.Branches;
using _123vendas.Domain.Base;
using _123vendas.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _123vendas_server.v1.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class BranchesController : ControllerBase
{
    private readonly IBranchService _branchService;

    public BranchesController(IBranchService branchService)
    {
        _branchService = branchService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponseDTO<BranchGetResponseDTO>>> GetAsync([FromQuery] BranchGetRequestDTO request)
    {
        var pagedResult = await _branchService.GetAllAsync(request.Id,
                                                           request.IsActive,
                                                           request.Name,
                                                           request.StartDate,
                                                           request.EndDate,
                                                           request.Page,
                                                           request.Size,
                                                           request.OrderByClause);

        if (pagedResult?.Items is not null && pagedResult.Items.Any())
            return Ok(new PagedResponseDTO<BranchGetResponseDTO>(pagedResult.Items.ToDTO(), pagedResult.Total, request.Page, request.Size));
        
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BranchGetDetailResponseDTO>> GetAsync([FromRoute] int id)
    {
        var branch = await _branchService.GetByIdAsync(id);

        if (branch is null)
            return NoContent();

        var response = branch.ToDetailDTO();

        return Ok(response);
    }

    [Authorize(Policy = "ManagerOnly")]
    [HttpPost]
    public async Task<ActionResult<BranchPostResponseDTO>> PostAsync([FromBody] BranchPostRequestDTO request)
    {
        var createdBranch = await _branchService.CreateAsync(request.ToEntity());

        var response = createdBranch.ToPostResponseDTO(); 

        return Created(string.Empty, response);
    }

    [Authorize(Policy = "ManagerOnly")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] BranchPutRequestDTO request)
    {
        var branch = await _branchService.UpdateAsync(id, request.ToEntity());

        return Ok(branch.ToPutResponseDTO());
    }

    [Authorize(Policy = "ManagerOnly")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _branchService.DeleteAsync(id);

        return NoContent();
    }
}
