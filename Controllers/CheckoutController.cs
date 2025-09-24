using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pos_simple.DTO;
using pos_simple.Service;

[ApiController]
[Route("api/[controller]")]
public class CheckoutController : ControllerBase
{
    private readonly CheckoutService _service;

    public CheckoutController(CheckoutService service)
    {
        _service = service;
    }

    [HttpPost]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CheckoutDto request)
    {
        try
        {
            var created = await _service.CreateAsync(request);
            return Ok(new ApiResponse<CheckoutResponse>
            {
                Code = 201,
                Message = "Checkout created successfully",
                Data = created
            });
        }
        catch (DbUpdateException dbEx) 
        {
            var msg = dbEx.InnerException?.Message ?? dbEx.Message; 
            return BadRequest(new ApiResponse<object>
            {
                Code = 400,
                Message = msg,
                Data = null
            });
        }
        catch (Exception ex) 
        {
            return BadRequest(new ApiResponse<object>
            {
                Code = 400,
                Message = ex.Message,
                Data = null
            });
        }
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound(new ApiResponse<object>
            {
                Code = 404,
                Message = "Checkout not found",
                Data = null
            });
        }

        return Ok(new ApiResponse<CheckoutResponse>
        {
            Code = 200,
            Message = "Checkout retrieved successfully",
            Data = result
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _service.GetAllAsync();
        return Ok(new ApiResponse<List<CheckoutResponse>>
        {
            Code = 200,
            Message = "Checkouts retrieved successfully",
            Data = list
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CheckoutDto request)
    {
        try
        {
            var updatedCheckout = await _service.UpdateAsync(id, request);
            if (updatedCheckout == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Code = 404,
                    Message = "Checkout not found",
                    Data = null
                });
            }

            return Ok(new ApiResponse<CheckoutResponse>
            {
                Code = 200,
                Message = "Checkout updated successfully",
                Data = updatedCheckout
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Code = 400,
                Message = ex.Message,
                Data = null
            });
        }
    }

}
