
using Microsoft.AspNetCore.Mvc;
using pos_simple.DTO;

[ApiController]
[Route("api/[controller]")]
public class ProductVariantController : ControllerBase
{
    private readonly ProductVariantService _service;

    public ProductVariantController(ProductVariantService service)
    {
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ProductVariantResponse>), 200)]
    public async Task<IActionResult> Create([FromBody] ProductVariantDto request)
    {
        var created = await _service.CreateAsync(request);
        var response = new ApiResponse<ProductVariantResponse>
        {
            Code = 200,
            Message = "Product variant created successfully",
            Data = created
        };
        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ProductVariantResponse>), 200)]
    [ProducesResponseType(typeof(ApiResponse<string>), 404)]
    public async Task<IActionResult> GetById(int id)
    {
        var variant = await _service.GetByIdAsync(id);
        if (variant == null)
            return NotFound(new ApiResponse<string> { Code = 404, Message = "Variant not found" });

        return Ok(new ApiResponse<ProductVariantResponse> { Code = 200, Message = "Success", Data = variant });
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<ProductVariantResponse>>), 200)]
    public async Task<IActionResult> GetAll()
    {
        var variants = await _service.GetAllAsync();
        return Ok(new ApiResponse<List<ProductVariantResponse>> { Code = 200, Message = "Success", Data = variants });
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<string>), 200)]
    [ProducesResponseType(typeof(ApiResponse<string>), 404)]
    public async Task<IActionResult> Update(int id, [FromBody] ProductVariantDto request)
    {
        var success = await _service.UpdateAsync(id, request);
        if (!success)
            return NotFound(new ApiResponse<string> { Code = 404, Message = "Variant not found" });

        return Ok(new ApiResponse<string> { Code = 200, Message = "Variant updated successfully" });
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<string>), 200)]
    [ProducesResponseType(typeof(ApiResponse<string>), 404)]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        if (!success)
            return NotFound(new ApiResponse<string> { Code = 404, Message = "Variant not found" });

        return Ok(new ApiResponse<string> { Code = 200, Message = "Variant deleted successfully" });
    }

    [HttpGet("product/{productId}")]
    [ProducesResponseType(typeof(ApiResponse<List<ProductVariantResponse>>), 200)]
    [ProducesResponseType(typeof(ApiResponse<string>), 404)]
    public async Task<IActionResult> GetByProductId(int productId)
    {
        var variants = await _service.GetByProductIdAsync(productId);
        if (variants == null || !variants.Any())
            return NotFound(new ApiResponse<string> { Code = 404, Message = "No variants found for this product" });

        // Mapping entity ke DTO
        var dtoList = variants.Select(v => new ProductVariantResponse
        {
            Id = v.Id,
            ProductId = v.ProductId,
            Name = v.Name,
            Price = v.Price
        }).ToList();

        return Ok(new ApiResponse<List<ProductVariantResponse>>
        {
            Code = 200,
            Message = "Success",
            Data = dtoList
        });
    }


    [HttpPut("product/{productId}")]
    [ProducesResponseType(typeof(ApiResponse<string>), 200)]
    [ProducesResponseType(typeof(ApiResponse<string>), 404)]
    public async Task<IActionResult> UpdateByProductId(int productId, [FromBody] List<ProductVariantDto> requests)
    {
        var success = await _service.UpdateByProductIdAsync(productId, requests);
        if (!success)
            return NotFound(new ApiResponse<string> { Code = 404, Message = "No variants found for this product" });

        return Ok(new ApiResponse<string> { Code = 200, Message = "Variants updated successfully" });
    }

    [HttpDelete("product/{productId}")]
    [ProducesResponseType(typeof(ApiResponse<string>), 200)]
    [ProducesResponseType(typeof(ApiResponse<string>), 404)]
    public async Task<IActionResult> DeleteByProductId(int productId)
    {
        var success = await _service.DeleteByProductIdAsync(productId);
        if (!success)
            return NotFound(new ApiResponse<string> { Code = 404, Message = "No variants found for this product" });

        return Ok(new ApiResponse<string> { Code = 200, Message = "Variants deleted successfully" });
    }
}



//using Microsoft.AspNetCore.Mvc;
//using pos_simple.DTO;

//[ApiController]
//[Route("api/[controller]")]
//public class ProductVariantController : ControllerBase
//{
//    private readonly ProductVariantService _service;

//    public ProductVariantController(ProductVariantService service)
//    {
//        _service = service;
//    }

//    [HttpPost]
//    public async Task<IActionResult> Create([FromBody] ProductVariantRequest request)
//    {
//        var created = await _service.CreateAsync(request);
//        return Ok(created);
//    }

//    [HttpGet("{id}")]
//    public async Task<IActionResult> GetById(int id)
//    {
//        var variant = await _service.GetByIdAsync(id);
//        if (variant == null) return NotFound();
//        return Ok(variant);
//    }

//    [HttpGet]
//    public async Task<IActionResult> GetAll()
//    {
//        var variants = await _service.GetAllAsync();
//        return Ok(variants);
//    }

//    [HttpPut("{id}")]
//    public async Task<IActionResult> Update(int id, [FromBody] ProductVariantRequest request)
//    {
//        var success = await _service.UpdateAsync(id, request);
//        if (!success) return NotFound();
//        return Ok();
//    }

//    [HttpDelete("{id}")]
//    public async Task<IActionResult> Delete(int id)
//    {
//        var success = await _service.DeleteAsync(id);
//        if (!success) return NotFound();
//        return Ok();
//    }


//    [HttpGet("product/{productId}")]
//    public async Task<IActionResult> GetByProductId(int productId)
//    {
//        var variants = await _service.GetByProductIdAsync(productId);
//        if (variants == null || !variants.Any())
//            return NotFound();

//        return Ok(variants);
//    }

//    [HttpPut("product/{productId}")]
//    public async Task<IActionResult> UpdateByProductId(int productId, [FromBody] List<ProductVariantRequest> requests)
//    {
//        var success = await _service.UpdateByProductIdAsync(productId, requests);
//        if (!success) return NotFound();
//        return Ok();
//    }

//    [HttpDelete("product/{productId}")]
//    public async Task<IActionResult> DeleteByProductId(int productId)
//    {
//        var success = await _service.DeleteByProductIdAsync(productId);
//        if (!success) return NotFound();
//        return Ok();
//    }

//}
