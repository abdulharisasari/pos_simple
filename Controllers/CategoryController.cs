using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pos_simple.DTO;
using pos_simple.Model;
using pos_simple.Service;

namespace pos_simple.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _service;
        private readonly IValidator<Category> _validator;

        public CategoriesController(CategoryService service, IValidator<Category> validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<Category>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _service.GetAllAsync();
            return Ok(new ApiResponse<List<Category>>
            {
                Code = 200,
                Message = "Categories retrieved successfully",
                Data = categories
            });
        }

      

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Category>), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CategoryDto request)
        {
            var category = new Category { Name = request.Name };
            var validationResult = await _validator.ValidateAsync(category);
            if (!validationResult.IsValid) return BadRequest(new ApiResponse<object>
            {
                Code = 400,
                Message = "Validation failed",
                Data = validationResult.Errors
            });

            var created = await _service.CreateAsync(category);
            return Ok(new ApiResponse<Category>
            {
                Code = 201,
                Message = "Category created successfully",
                Data = created
            });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Category>), StatusCodes.Status200OK)]


        public async Task<IActionResult> Update(int id, [FromBody] CategoryDto request)
        {
            var category = new Category { Name = request.Name };
            var validationResult = await _validator.ValidateAsync(category);
            if (!validationResult.IsValid) return BadRequest(new ApiResponse<object>
            {
                Code = 400,
                Message = "Validation failed",
                Data = validationResult.Errors
            });

            var updated = await _service.UpdateAsync(id, category);
            if (!updated) return NotFound(new ApiResponse<object>
            {
                Code = 404,
                Message = "Category not found",
                Data = null
            });

            return Ok(new ApiResponse<Category>
            {
                Code = 200,
                Message = "Category updated successfully",
                Data = category
            });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithoutData), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound(new ApiResponseWithoutData
            {
                Code = 404,
                Message = "Category not found"
            });

            return Ok(new ApiResponseWithoutData
            {
                Code = 200,
                Message = "Category deleted successfully"
            });
        }
    }
}
