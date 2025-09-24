using Microsoft.EntityFrameworkCore;
using pos_simple.Data;
using pos_simple.DTO;
using pos_simple.Models;

public class ReportService
{
    private readonly AppDbContext _db;

    public ReportService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<SalesReportDto> GetSalesReportAsync()
    {
        var today = DateTime.UtcNow.Date;
        var yesterday = today.AddDays(-1);

        var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
        var startOfLastWeek = startOfWeek.AddDays(-7);

        var startOfMonth = new DateTime(today.Year, today.Month, 1, 0, 0, 0, DateTimeKind.Utc);
        var startOfLastMonth = startOfMonth.AddMonths(-1);

        var report = new SalesReportDto
        {
            Today = await GetTotalByDateRangeAsync(today, today.AddDays(1)),
            Yesterday = await GetTotalByDateRangeAsync(yesterday, today),

            Week = await GetTotalByDateRangeAsync(startOfWeek, today.AddDays(1)),
            LastWeek = await GetTotalByDateRangeAsync(startOfLastWeek, startOfWeek),

            Month = await GetTotalByDateRangeAsync(startOfMonth, today.AddDays(1)),
            LastMonth = await GetTotalByDateRangeAsync(startOfLastMonth, startOfMonth),

            TodayCash = await GetTotalByDateRangeAndPaymentAsync(today, today.AddDays(1), 1),
            TodayDebit = await GetTotalByDateRangeAndPaymentAsync(today, today.AddDays(1), 2),

            TotalItemsSoldToday = await GetTotalItemsSoldAsync(today, today.AddDays(1)),
            TotalItemsSoldThisMonth = await GetTotalItemsSoldAsync(startOfMonth, today.AddDays(1))
        };

        return report;
    }

    private async Task<decimal> GetTotalByDateRangeAsync(DateTime start, DateTime end)
    {
        return await _db.Checkouts
            .Where(c => c.CreatedAt >= start && c.CreatedAt < end)
            .SumAsync(c => c.TotalPrice);
    }

    private async Task<decimal> GetTotalByDateRangeAndPaymentAsync(DateTime start, DateTime end, int paymentMethodId)
    {
        return await _db.Checkouts
            .Where(c => c.CreatedAt >= start && c.CreatedAt < end && c.PaymentMethodId == paymentMethodId)
            .SumAsync(c => c.TotalPrice);
    }

    private async Task<int> GetTotalItemsSoldAsync(DateTime start, DateTime end)
    {
        return await _db.CheckoutItems
            .Include(ci => ci.Checkout)
            .Where(ci => ci.Checkout.CreatedAt >= start && ci.Checkout.CreatedAt < end)
            .SumAsync(ci => ci.Quantity);
    }
}
