using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pos_simple.DTO;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly ReportService _reportService;

    public ReportController(ReportService reportService)
    {
        _reportService = reportService;
    }

   
    [HttpGet("sales")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<SalesReportDto>), 200)]
    public async Task<IActionResult> GetSalesReport()
    {
        try
        {
            var report = await _reportService.GetSalesReportAsync();
            return Ok(new ApiResponse<SalesReportDto>
            {
                Code = 200,
                Message = "Sales report retrieved successfully",
                Data = report
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<string>
            {
                Code = 500,
                Message = "Failed to retrieve sales report: " + ex.Message,
                Data = null
            });
        }
    }
}
