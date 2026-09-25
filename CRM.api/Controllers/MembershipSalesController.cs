using CRM.domain.Entities;
using CRM.infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.api.Controllers
{
    [ApiController]
    [Route("tenant/{companyId:int}/membershipsales")]
    public class MembershipSalesController : ControllerBase
    {
        private readonly ITenantDbContextFactory _tenantFactory;

        public MembershipSalesController(ITenantDbContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Create(int companyId, [FromBody] MembershipSale sale)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);

            var customerExists = await tenantDb.Customers.AnyAsync(c => c.CustomerId == sale.CustomerId);
            var planExists = await tenantDb.MembershipPlans.AnyAsync(p => p.MembershipPlanId == sale.MembershipPlanId);
            if (!customerExists || !planExists)
                return BadRequest(new { message = "The specified customer or plan does not exist." });

            tenantDb.MembershipSales.Add(sale);
            await tenantDb.SaveChangesAsync();
            return Created($"/tenant/{companyId}/membershipsales/{sale.MembershipSaleId}", sale);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int companyId, [FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);

            var query = tenantDb.MembershipSales
                .Where(x => x.IsActive && !x.IsCancelled)
                .AsQueryable();

            if (from.HasValue)
                query = query.Where(x => x.SaleDate >= from.Value);

            if (to.HasValue)
                query = query.Where(x => x.SaleDate <= to.Value);

            var sales = await query
                .Include(x => x.Customer)
                .Include(x => x.MembershipPlan)
                .AsNoTracking()
                .OrderBy(x => x.SaleDate)
                .ToListAsync();

            return Ok(sales);
        }

        // Request shape for cancel — defined as a small class so the API has a
        // typed contract, not a raw anonymous object.
        public class CancelRequest
        {
            public string? Reason { get; set; }
        }

        [HttpPost("{id:int}/cancel")]
        public async Task<IActionResult> Cancel(int companyId, int id, [FromBody] CancelRequest request)
        {
            // Who is doing this? Read the "userId" claim out of the JWT that
            // the caller sent in the Authorization header.
            var userIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdStr, out var userId))
                return Unauthorized();

            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);

            var sale = await tenantDb.MembershipSales
                .Include(x => x.Customer)
                .FirstOrDefaultAsync(x => x.MembershipSaleId == id && x.IsActive);

            if (sale is null)
                return NotFound(new { message = "Sale not found." });

            if (sale.IsCancelled)
                return BadRequest(new { message = "This sale is already cancelled." });

            // Mark the sale as cancelled
            sale.IsCancelled = true;
            sale.CancelledAt = DateTime.UtcNow;
            sale.CancellationReason = request.Reason;

            // Log it as a retention action so the retention team sees the churn event
            tenantDb.RetentionActions.Add(new RetentionAction
            {
                CustomerId = sale.CustomerId,
                PerformedByUserId = userId,
                ActionType = RetentionActionType.Cancelled,
                Notes = request.Reason,
                Outcome = RetentionOutcome.Lost,
                Timestamp = DateTime.UtcNow,
            });

            await tenantDb.SaveChangesAsync();
            return Ok(sale);
        }
    }
}