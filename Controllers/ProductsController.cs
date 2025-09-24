
using Microsoft.AspNetCore.Mvc;
using pos_simple.DTO;
using pos_simple.Service;

namespace pos_simple.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _service;
        public ProductsController(ProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAllProductsAsync();
            return Ok(new ApiResponse<List<ProductDto>>
            {
                Code = 200,
                Message = "Products retrieved successfully",
                Data = products
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null)
                return NotFound(new ApiResponse<object>
                {
                    Code = 404,
                    Message = "Product not found",
                    Data = null
                });

            return Ok(new ApiResponse<ProductDto>
            {
                Code = 200,
                Message = "Product retrieved successfully",
                Data = product
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductRequest request)
        {
            var created = await _service.CreateAsync(request);

            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                new ApiResponse<ProductDto>
                {
                    Code = 201,
                    Message = "Product created successfully",
                    Data = created
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductRequest request)
        {
            var updated = await _service.UpdateAsync(id, request);
            if (updated == null)
                return NotFound(new ApiResponse<object>
                {
                    Code = 404,
                    Message = "Product not found",
                    Data = null
                });

            return Ok(new ApiResponse<ProductDto>
            {
                Code = 200,
                Message = "Product updated successfully",
                Data = updated
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound(new ApiResponseWithoutData
                {
                    Code = 404,
                    Message = "Product not found"
                });

            return Ok(new ApiResponseWithoutData
            {
                Code = 200,
                Message = "Product deleted successfully"
            });
        }
    }
}
