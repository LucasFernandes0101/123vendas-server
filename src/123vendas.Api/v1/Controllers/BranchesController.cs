﻿using _123vendas.Application.DTOs.Branches;
using _123vendas.Application.Mappers.Branches;
using _123vendas.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace _123vendas_server.v1.Controllers
{
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
        public async Task<ActionResult<IEnumerable<BranchGetResponseDTO>>> GetAsync([FromQuery] BranchGetRequestDTO request)
        {
            var branches = await _branchService.GetAllAsync(request.Id,
                                                            request.IsActive,
                                                            request.Name,
                                                            request.StartDate,
                                                            request.EndDate,
                                                            request.Page,
                                                            request.MaxResults);

            var response = branches.ToDTO();

            if (response is not null && response.Any())
                return Ok(response);

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

        [HttpPost]
        public async Task<ActionResult<BranchPostResponseDTO>> PostAsync([FromBody] BranchPostRequestDTO request)
        {
            var createdBranch = await _branchService.CreateAsync(request.ToEntity());
            var response = createdBranch.ToPostResponseDTO(); 
            return Created(string.Empty, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] BranchPutRequestDTO request)
        {
            var branch = await _branchService.UpdateAsync(id, request.ToEntity());
            return Ok(branch.ToPutResponseDTO());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _branchService.DeleteAsync(id);
            return NoContent();
        }
    }
}
