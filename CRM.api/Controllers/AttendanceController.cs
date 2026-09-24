using CRM.domain.Entities;
using CRM.infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.api.Controllers
{
    [ApiController]
    [Route("tenant/{companyId:int}/attendance")]
    public class AttendanceController : ControllerBase
    {
        private readonly ITenantDbContextFactory _tenantFactory;

        public AttendanceController(ITenantDbContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        // ---- request shapes ----

        public class CheckInRequest
        {
            public int CustomerId { get; set; }
            public int? BranchId { get; set; }
            public string? Notes { get; set; }
        }

        // ============================================================
        // POST /attendance/checkin
        // Records a check-in. Rejects frozen or expired memberships.
        // ============================================================
        [HttpPost("checkin")]
        public async Task<IActionResult> CheckIn(int companyId, [FromBody] CheckInRequest request)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);

            // 1. Customer exists?
            var customer = await tenantDb.Customers.FindAsync(request.CustomerId);
            if (customer is null)
                return BadRequest(new { message = "The specified customer does not exist." });

            // 2. Customer active?
            if (!customer.IsActive)
                return BadRequest(new { message = "This customer is inactive." });

            // 3. Frozen?
            if (customer.IsFrozen)
                return BadRequest(new { message = "This membership is frozen — check-in is not allowed." });

            // 4. Expired?
            var today = DateTime.UtcNow.Date;
            var latestSale = await tenantDb.MembershipSales
                .Include(s => s.MembershipPlan)
                .Where(s => s.CustomerId == request.CustomerId && s.IsActive && s.MembershipPlan != null)
                .OrderByDescending(s => s.SaleDate)
                .FirstOrDefaultAsync();

            if (latestSale?.MembershipPlan is null)
                return BadRequest(new { message = "This customer has no active membership." });

            var expiry = latestSale.SaleDate.Date.AddDays(latestSale.MembershipPlan.DurationInDays);
            if (expiry < today)
                return BadRequest(new { message = $"This membership expired on {expiry:yyyy-MM-dd}." });

            // 5. All checks passed — record the check-in
            var attendance = new Attendance
            {
                CustomerId = request.CustomerId,
                BranchId = request.BranchId,
                CheckInTime = DateTime.UtcNow,
                Notes = request.Notes,
                IsActive = true,
            };

            tenantDb.Attendances.Add(attendance);
            await tenantDb.SaveChangesAsync();

            // Reload with customer for the response
            await tenantDb.Entry(attendance).Reference(a => a.Customer).LoadAsync();

            return Ok(new
            {
                message = $"{customer.CustomerName} checked in.",
                attendance,
            });
        }

        // ============================================================
        // POST /attendance/{id}/checkout
        // ============================================================
        [HttpPost("{id:int}/checkout")]
        public async Task<IActionResult> CheckOut(int companyId, int id)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var existing = await tenantDb.Attendances.FindAsync(id);
            if (existing is null) return NotFound();

            if (existing.CheckOutTime.HasValue)
                return BadRequest(new { message = "This attendance record already has a check-out time." });

            existing.CheckOutTime = DateTime.UtcNow;
            await tenantDb.SaveChangesAsync();
            return Ok(existing);
        }

        // ============================================================
        // GET /attendance/today
        // Today's check-ins (UTC date match).
        // ============================================================
        [HttpGet("today")]
        public async Task<IActionResult> GetToday(int companyId)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);

            var todayStart = DateTime.UtcNow.Date;
            var todayEnd = todayStart.AddDays(1);

            var records = await tenantDb.Attendances
                .Include(a => a.Customer)
                .Where(a => a.IsActive && a.CheckInTime >= todayStart && a.CheckInTime < todayEnd)
                .AsNoTracking()
                .OrderByDescending(a => a.CheckInTime)
                .ToListAsync();

            return Ok(records);
        }

        // ============================================================
        // GET /attendance?customerId=&from=&to=
        // ============================================================
        [HttpGet]
        public async Task<IActionResult> GetHistory(
            int companyId,
            [FromQuery] int? customerId,
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);

            var query = tenantDb.Attendances
                .Include(a => a.Customer)
                .Where(a => a.IsActive)
                .AsQueryable();

            if (customerId.HasValue)
                query = query.Where(a => a.CustomerId == customerId.Value);

            if (from.HasValue)
                query = query.Where(a => a.CheckInTime >= from.Value.Date);

            if (to.HasValue)
                query = query.Where(a => a.CheckInTime < to.Value.Date.AddDays(1));

            var records = await query
                .AsNoTracking()
                .OrderByDescending(a => a.CheckInTime)
                .Take(500)
                .ToListAsync();

            return Ok(records);
        }
    }
}