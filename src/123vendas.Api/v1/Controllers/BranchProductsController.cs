using _123vendas.Application.DTOs.BranchProducts;
using _123vendas.Application.Mappers.BranchProducts;
using _123vendas.Domain.Base;
using _123vendas.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace _123vendas_server.v1.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class BranchProductsController : ControllerBase
    {
        private readonly IBranchProductService _branchProductService;

        public BranchProductsController(IBranchProductService branchProductService)
        {
            _branchProductService = branchProductService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponseDTO<BranchProductGetResponseDTO>>> GetAsync([FromQuery] BranchProductGetRequestDTO request)
        {
            var pagedResult = await _branchProductService.GetAllAsync(request.Id,
                                                                      request.BranchId,
                                                                      request.ProductId,
                                                                      request.IsActive,
                                                                      request.StartDate,
                                                                      request.EndDate,
                                                                      request.Page,
                                                                      request.Size,
                                                                      request.OrderByClause);

            if (pagedResult?.Items is not null && pagedResult.Items.Any())
                return Ok(new PagedResponseDTO<BranchProductGetResponseDTO>(pagedResult.Items.ToDTO(), pagedResult.Total, request.Page, request.Size));

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BranchProductGetDetailResponseDTO>> GetAsync([FromRoute] int id)
        {
            var branchProduct = await _branchProductService.GetByIdAsync(id);

            if (branchProduct is null)
                return NoContent();

            var response = branchProduct.ToDetailDTO();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<BranchProductPostResponseDTO>> PostAsync([FromBody] BranchProductPostRequestDTO request)
        {
            var createdBranchProduct = await _branchProductService.CreateAsync(request.ToEntity());
            var response = createdBranchProduct.ToPostResponseDTO();
            return Created(string.Empty, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] BranchProductPutRequestDTO request)
        {
            var branchProduct = await _branchProductService.UpdateAsync(id, request.ToEntity());
            return Ok(branchProduct.ToPutResponseDTO());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _branchProductService.DeleteAsync(id);
            return NoContent();
        }
    }
}