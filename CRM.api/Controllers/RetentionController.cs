using CRM.domain.Entities;
using CRM.infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CRM.api.Controllers
{
    [ApiController]
    [Route("tenant/{companyId:int}/retention")]
    public class RetentionController : ControllerBase
    {
        private readonly ITenantDbContextFactory _tenantFactory;

        public RetentionController(ITenantDbContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        // ---- request shapes ----

        public class LogActionRequest
        {
            public int CustomerId { get; set; }
            public RetentionActionType ActionType { get; set; }
            public string? Notes { get; set; }
            public DateTime? FollowUpDate { get; set; }
            public RetentionOutcome Outcome { get; set; } = RetentionOutcome.Pending;
        }

        public class UpdateOutcomeRequest
        {
            public RetentionOutcome Outcome { get; set; }
            public string? Notes { get; set; }
        }

        public class FreezeRequest
        {
            public DateTime? FrozenUntil { get; set; }
            public string? Notes { get; set; }
        }

        // ---- helpers ----

        private int? GetCurrentUserId()
        {
            var idStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(idStr, out var id) ? id : null;
        }

        // ============================================================
        // GET /retention/at-risk?days=30
        // Members whose latest sale expires within N days (or already expired).
        // ============================================================
        [HttpGet("at-risk")]
        public async Task<IActionResult> GetAtRisk(int companyId, [FromQuery] int days = 30)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);

            var today = DateTime.UtcNow.Date;
            var threshold = today.AddDays(days);

            // Load all active customers with their latest sale + plan
            var customers = await tenantDb.Customers
                .Where(c => c.IsActive)
                .AsNoTracking()
                .ToListAsync();

            var sales = await tenantDb.MembershipSales
                .Include(s => s.MembershipPlan)
                .Where(s => s.IsActive)
                .AsNoTracking()
                .ToListAsync();

            var latestPerCustomer = sales
                .Where(s => s.MembershipPlan != null)
                .GroupBy(s => s.CustomerId)
                .ToDictionary(g => g.Key, g => g.OrderByDescending(s => s.SaleDate).First());

            var result = new List<object>();
            foreach (var c in customers)
            {
                if (!latestPerCustomer.TryGetValue(c.CustomerId, out var sale)) continue;
                var expiry = sale.SaleDate.Date.AddDays(sale.MembershipPlan!.DurationInDays);
                if (expiry > threshold) continue;

                var daysLeft = (expiry - today).Days;

                result.Add(new
                {
                    c.CustomerId,
                    c.CustomerCode,
                    c.CustomerName,
                    c.ContactNumber,
                    c.EmailAddress,
                    PlanName = sale.MembershipPlan.PlanName,
                    ExpiryDate = expiry,
                    DaysLeft = daysLeft,
                    Status = daysLeft < 0 ? "Expired" : "ExpiringSoon",
                    c.IsFrozen,
                });
            }

            return Ok(result.OrderBy(x => ((dynamic)x).DaysLeft).ToList());
        }

        // ============================================================
        // GET /retention/win-back
        // Customers whose membership expired >90 days ago and haven't renewed since.
        // ============================================================
        [HttpGet("win-back")]
        public async Task<IActionResult> GetWinBack(int companyId)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);

            var today = DateTime.UtcNow.Date;
            var cutoff = today.AddDays(-90);

            var customers = await tenantDb.Customers
                .Where(c => c.IsActive)
                .AsNoTracking()
                .ToListAsync();

            var sales = await tenantDb.MembershipSales
                .Include(s => s.MembershipPlan)
                .Where(s => s.IsActive)
                .AsNoTracking()
                .ToListAsync();

            var latestPerCustomer = sales
                .Where(s => s.MembershipPlan != null)
                .GroupBy(s => s.CustomerId)
                .ToDictionary(g => g.Key, g => g.OrderByDescending(s => s.SaleDate).First());

            var result = new List<object>();
            foreach (var c in customers)
            {
                if (!latestPerCustomer.TryGetValue(c.CustomerId, out var sale)) continue;
                var expiry = sale.SaleDate.Date.AddDays(sale.MembershipPlan!.DurationInDays);
                if (expiry > cutoff) continue;

                var daysExpired = (today - expiry).Days;

                result.Add(new
                {
                    c.CustomerId,
                    c.CustomerCode,
                    c.CustomerName,
                    c.ContactNumber,
                    c.EmailAddress,
                    LastPlanName = sale.MembershipPlan.PlanName,
                    LastExpiry = expiry,
                    DaysExpired = daysExpired,
                });
            }

            return Ok(result.OrderByDescending(x => ((dynamic)x).DaysExpired).ToList());
        }

        // ============================================================
        // GET /retention/frozen
        // ============================================================
        [HttpGet("frozen")]
        public async Task<IActionResult> GetFrozen(int companyId)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var frozen = await tenantDb.Customers
                .Where(c => c.IsFrozen)
                .AsNoTracking()
                .OrderBy(c => c.CustomerName)
                .ToListAsync();
            return Ok(frozen);
        }

        // ============================================================
        // GET /retention/actions
        // ============================================================
        [HttpGet("actions")]
        public async Task<IActionResult> GetActions(int companyId)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var actions = await tenantDb.RetentionActions
                .Include(a => a.Customer)
                .AsNoTracking()
                .OrderByDescending(a => a.Timestamp)
                .Take(200)
                .ToListAsync();
            return Ok(actions);
        }

        // ============================================================
        // POST /retention/actions
        // Log a retention action (campaign, follow-up, etc.)
        // ============================================================
        [HttpPost("actions")]
        public async Task<IActionResult> LogAction(int companyId, [FromBody] LogActionRequest request)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId is null) return Unauthorized();

            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);

            var customerExists = await tenantDb.Customers.AnyAsync(c => c.CustomerId == request.CustomerId);
            if (!customerExists)
                return BadRequest(new { message = "The specified customer does not exist." });

            var action = new RetentionAction
            {
                CustomerId = request.CustomerId,
                PerformedByUserId = currentUserId.Value,
                ActionType = request.ActionType,
                Notes = request.Notes,
                FollowUpDate = request.FollowUpDate,
                Outcome = request.Outcome,
                Timestamp = DateTime.UtcNow,
            };

            tenantDb.RetentionActions.Add(action);
            await tenantDb.SaveChangesAsync();

            return Created($"/tenant/{companyId}/retention/actions/{action.RetentionActionId}", action);
        }

        // ============================================================
        // PUT /retention/actions/{id}/outcome
        // Update outcome after result is known.
        // ============================================================
        [HttpPut("actions/{id:int}/outcome")]
        public async Task<IActionResult> UpdateOutcome(int companyId, int id, [FromBody] UpdateOutcomeRequest request)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var existing = await tenantDb.RetentionActions.FindAsync(id);
            if (existing is null) return NotFound();

            existing.Outcome = request.Outcome;
            if (request.Notes != null) existing.Notes = request.Notes;

            await tenantDb.SaveChangesAsync();
            return Ok(existing);
        }

        // ============================================================
        // POST /retention/freeze/{customerId}
        // ============================================================
        [HttpPost("freeze/{customerId:int}")]
        public async Task<IActionResult> Freeze(int companyId, int customerId, [FromBody] FreezeRequest request)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId is null) return Unauthorized();

            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var customer = await tenantDb.Customers.FindAsync(customerId);
            if (customer is null) return NotFound();

            customer.IsFrozen = true;
            customer.FrozenUntil = request.FrozenUntil;

            // Log the freeze as a retention action
            tenantDb.RetentionActions.Add(new RetentionAction
            {
                CustomerId = customerId,
                PerformedByUserId = currentUserId.Value,
                ActionType = RetentionActionType.FreezeApplied,
                Notes = request.Notes,
                Outcome = RetentionOutcome.Pending,
                Timestamp = DateTime.UtcNow,
            });

            await tenantDb.SaveChangesAsync();
            return Ok(customer);
        }

        // ============================================================
        // DELETE /retention/freeze/{customerId}
        // ============================================================
        [HttpDelete("freeze/{customerId:int}")]
        public async Task<IActionResult> Unfreeze(int companyId, int customerId)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId is null) return Unauthorized();

            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var customer = await tenantDb.Customers.FindAsync(customerId);
            if (customer is null) return NotFound();

            customer.IsFrozen = false;
            customer.FrozenUntil = null;

            tenantDb.RetentionActions.Add(new RetentionAction
            {
                CustomerId = customerId,
                PerformedByUserId = currentUserId.Value,
                ActionType = RetentionActionType.FreezeLifted,
                Outcome = RetentionOutcome.Renewed, // treat lift as positive outcome
                Timestamp = DateTime.UtcNow,
            });

            await tenantDb.SaveChangesAsync();
            return Ok(customer);
        }
    }
}