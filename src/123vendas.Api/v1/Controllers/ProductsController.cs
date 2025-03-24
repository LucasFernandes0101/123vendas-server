using _123vendas.Application.DTOs.Products;
using _123vendas.Application.Mappers.Products;
using _123vendas.Domain.Base;
using _123vendas.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace _123vendas_server.v1.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponseDTO<ProductGetResponseDTO>>> GetAsync([FromQuery] ProductGetRequestDTO request)
        {
            var pagedResult = await _productService.GetAllAsync(request.Id,
                                                            request.IsActive,
                                                            request.Title,
                                                            request.Category,
                                                            request.MinPrice,
                                                            request.MaxPrice,
                                                            request.StartDate,
                                                            request.EndDate,
                                                            request.Page,
                                                            request.Size,
                                                            request.OrderByClause);

            if (pagedResult?.Items is not null && pagedResult.Items.Any())
                return Ok(new PagedResponseDTO<ProductGetResponseDTO>(pagedResult.Items.ToDTO(), pagedResult.Total, request.Page, request.Size));

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductGetDetailResponseDTO>> GetAsync([FromRoute] int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product is null)
                return NoContent();

            var response = product.ToDetailDTO();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ProductPostResponseDTO>> PostAsync([FromBody] ProductPostRequestDTO request)
        {
            var createdProduct = await _productService.CreateAsync(request.ToEntity());
            var response = createdProduct.ToPostResponseDTO();
            return Created(string.Empty, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] ProductPutRequestDTO request)
        {
            var product = await _productService.UpdateAsync(id, request.ToEntity());
            return Ok(product.ToPutResponseDTO());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _productService.DeleteAsync(id);
            return NoContent();
        }
    }
}
