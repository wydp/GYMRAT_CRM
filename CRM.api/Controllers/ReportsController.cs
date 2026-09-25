using CRM.domain.Entities;
using CRM.infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.api.Controllers
{
    [ApiController]
    [Route("tenant/{companyId:int}/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly ITenantDbContextFactory _tenantFactory;

        public ReportsController(ITenantDbContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        // ============================================================
        // GET /reports/revenue?from=&to=
        // Monthly revenue, by-plan revenue, and totals for KPI cards.
        // ============================================================
        [HttpGet("revenue")]
        public async Task<IActionResult> GetRevenue(
            int companyId,
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to)
        {
            var fromDate = (from ?? DateTime.UtcNow.AddMonths(-6)).Date;
            var toDate = (to ?? DateTime.UtcNow).Date.AddDays(1).AddSeconds(-1);

            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);

            var sales = await tenantDb.MembershipSales
                .Include(s => s.Customer)
                .Include(s => s.MembershipPlan)
                .Where(s => s.IsActive
                         && s.SaleDate >= fromDate
                         && s.SaleDate <= toDate)
                .AsNoTracking()
                .ToListAsync();

            var activeSales = sales.Where(s => !s.IsCancelled).ToList();
            var cancelledCount = sales.Count(s => s.IsCancelled);

            var totalRevenue = activeSales.Sum(s => s.AmountPaid);
            var saleCount = activeSales.Count;
            var avgSale = saleCount > 0 ? totalRevenue / saleCount : 0m;

            var byMonth = activeSales
                .GroupBy(s => s.SaleDate.ToString("yyyy-MM"))
                .OrderBy(g => g.Key)
                .Select(g => new
                {
                    Month = g.Key,
                    Revenue = g.Sum(s => s.AmountPaid),
                    Count = g.Count()
                })
                .ToList();

            var byPlan = activeSales
                .Where(s => s.MembershipPlan != null)
                .GroupBy(s => s.MembershipPlan!.PlanName)
                .OrderByDescending(g => g.Sum(s => s.AmountPaid))
                .Select(g => new
                {
                    PlanName = g.Key,
                    Revenue = g.Sum(s => s.AmountPaid),
                    Count = g.Count()
                })
                .ToList();

            return Ok(new
            {
                totalRevenue,
                saleCount,
                avgSale,
                cancelledCount,
                byMonth,
                byPlan,
                sales = activeSales.Select(s => new
                {
                    s.MembershipSaleId,
                    s.SaleDate,
                    s.AmountPaid,
                    CustomerName = s.Customer != null ? s.Customer.CustomerName : "(unknown)",
                    PlanName = s.MembershipPlan != null ? s.MembershipPlan.PlanName : "(unknown)"
                }).OrderBy(s => s.SaleDate).ToList()
            });
        }
    }
}