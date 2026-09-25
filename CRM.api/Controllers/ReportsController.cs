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
        // ============================================================
        // GET /reports/membership
        // Status counts (Active / Expiring Soon / Expired / Frozen),
        // breakdown by plan, and per-customer rows.
        // Date range is intentionally ignored — membership status is
        // a "current state" report, not historical.
        // ============================================================
        [HttpGet("membership")]
        public async Task<IActionResult> GetMembership(int companyId)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);

            var today = DateTime.UtcNow.Date;
            var expiringThreshold = today.AddDays(30);

            var customers = await tenantDb.Customers
                .Where(c => c.IsActive)
                .AsNoTracking()
                .ToListAsync();

            var sales = await tenantDb.MembershipSales
                .Include(s => s.MembershipPlan)
                .Where(s => s.IsActive && !s.IsCancelled)
                .AsNoTracking()
                .ToListAsync();

            var latestPerCustomer = sales
                .Where(s => s.MembershipPlan != null)
                .GroupBy(s => s.CustomerId)
                .ToDictionary(g => g.Key, g => g.OrderByDescending(s => s.SaleDate).First());

            int activeCount = 0;
            int expiringSoonCount = 0;
            int expiredCount = 0;
            int frozenCount = 0;

            var perPlan = new Dictionary<string, int>();
            var rows = new List<object>();

            foreach (var c in customers)
            {
                if (c.IsFrozen) frozenCount++;

                if (!latestPerCustomer.TryGetValue(c.CustomerId, out var sale))
                    continue;

                var expiry = sale.SaleDate.Date.AddDays(sale.MembershipPlan!.DurationInDays);
                var daysLeft = (expiry - today).Days;

                string status;
                if (daysLeft < 0) { status = "Expired"; expiredCount++; }
                else if (daysLeft <= 30) { status = "Expiring Soon"; expiringSoonCount++; }
                else { status = "Active"; activeCount++; }

                var planName = sale.MembershipPlan.PlanName;
                if (!perPlan.ContainsKey(planName)) perPlan[planName] = 0;
                perPlan[planName]++;

                rows.Add(new
                {
                    c.CustomerId,
                    c.CustomerCode,
                    c.CustomerName,
                    PlanName = planName,
                    StartDate = sale.SaleDate.Date,
                    ExpiryDate = expiry,
                    DaysLeft = daysLeft,
                    Status = status,
                    IsFrozen = c.IsFrozen
                });
            }

            var byPlan = perPlan
                .Select(kv => new { PlanName = kv.Key, Count = kv.Value })
                .OrderByDescending(x => x.Count)
                .ToList();

            var statusDistribution = new List<object>
            {
                new { Status = "Active", Count = activeCount },
                new { Status = "Expiring Soon", Count = expiringSoonCount },
                new { Status = "Expired", Count = expiredCount },
                new { Status = "Frozen", Count = frozenCount }
            };

            return Ok(new
            {
                activeCount,
                expiringSoonCount,
                expiredCount,
                frozenCount,
                totalCustomers = customers.Count,
                statusDistribution,
                byPlan,
                members = rows.OrderBy(x => ((dynamic)x).ExpiryDate).ToList()
            });
        }

        // ============================================================
        // GET /reports/attendance?from=&to=
        // Check-in totals for KPI cards, plus daily and hourly
        // distributions for charts, plus per-row data for the grid.
        // ============================================================
        [HttpGet("attendance")]
        public async Task<IActionResult> GetAttendance(
            int companyId,
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to)
        {
            var fromDate = (from ?? DateTime.UtcNow.AddMonths(-6)).Date;
            var toDate = (to ?? DateTime.UtcNow).Date.AddDays(1).AddSeconds(-1);

            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);

            var allInRange = await tenantDb.Attendances
                .Include(a => a.Customer)
                .Where(a => a.IsActive
                         && a.CheckInTime >= fromDate
                         && a.CheckInTime <= toDate)
                .AsNoTracking()
                .ToListAsync();

            var today = DateTime.UtcNow.Date;
            var weekStart = today.AddDays(-(((int)today.DayOfWeek + 6) % 7)); // Monday
            var monthStart = new DateTime(today.Year, today.Month, 1);

            var todayCount = allInRange.Count(a => a.CheckInTime.Date == today);
            var weekCount = allInRange.Count(a => a.CheckInTime.Date >= weekStart);
            var monthCount = allInRange.Count(a => a.CheckInTime.Date >= monthStart);
            var uniqueMembers = allInRange.Select(a => a.CustomerId).Distinct().Count();

            var byDay = allInRange
                .GroupBy(a => a.CheckInTime.Date)
                .OrderBy(g => g.Key)
                .Select(g => new
                {
                    Date = g.Key.ToString("yyyy-MM-dd"),
                    Count = g.Count()
                })
                .ToList();

            var byHour = allInRange
                .GroupBy(a => a.CheckInTime.Hour)
                .OrderBy(g => g.Key)
                .Select(g => new
                {
                    Hour = g.Key,
                    Count = g.Count()
                })
                .ToList();

            return Ok(new
            {
                todayCount,
                weekCount,
                monthCount,
                uniqueMembers,
                byDay,
                byHour,
                attendance = allInRange
                    .OrderByDescending(a => a.CheckInTime)
                    .Select(a => new
                    {
                        a.AttendanceId,
                        a.CustomerId,
                        CustomerName = a.Customer != null ? a.Customer.CustomerName : "(unknown)",
                        a.CheckInTime,
                        a.CheckOutTime,
                        a.Notes
                    })
                    .ToList()
            });
        }
    }
}