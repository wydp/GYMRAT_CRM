using CRM.domain.Entities;
using CRM.infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CRM.api.Controllers
{
    [ApiController]
    [Route("tenant/{companyId:int}/membershipplans")]
    public class MembershipPlansController : ControllerBase
    {
        private readonly ITenantDbContextFactory _tenantFactory;

        public MembershipPlansController(ITenantDbContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        // POST /tenant/{companyId}/membershipplans
        [HttpPost]
        public async Task<IActionResult> Create(int companyId, [FromBody] MembershipPlan plan)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            tenantDb.MembershipPlans.Add(plan);

            try
            {
                await tenantDb.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sql && (sql.Number == 2601 || sql.Number == 2627))
            {
                return Conflict(new { message = "A membership plan with this code already exists." });
            }

            return Created($"/tenant/{companyId}/membershipplans/{plan.MembershipPlanId}", plan);
        }

        // GET /tenant/{companyId}/membershipplans
        [HttpGet]
        public async Task<IActionResult> GetAll(int companyId)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var plans = await tenantDb.MembershipPlans
                .Where(x => x.IsActive)
                .AsNoTracking()
                .OrderBy(x => x.MembershipPlanId)
                .ToListAsync();
            return Ok(plans);
        }

        // PUT /tenant/{companyId}/membershipplans/{planId}
        [HttpPut("{planId:int}")]
        public async Task<IActionResult> Update(int companyId, int planId, [FromBody] MembershipPlan updated)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var existing = await tenantDb.MembershipPlans.FindAsync(planId);
            if (existing is null) return NotFound();

            existing.PlanCode = updated.PlanCode;
            existing.PlanName = updated.PlanName;
            existing.Description = updated.Description;
            existing.Price = updated.Price;
            existing.DurationInDays = updated.DurationInDays;
            existing.IsActive = updated.IsActive;

            try
            {
                await tenantDb.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sql && (sql.Number == 2601 || sql.Number == 2627))
            {
                return Conflict(new { message = "A membership plan with this code already exists." });
            }

            return Ok(existing);
        }

        // DELETE /tenant/{companyId}/membershipplans/{planId}
        [HttpDelete("{planId:int}")]
        public async Task<IActionResult> Delete(int companyId, int planId)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var existing = await tenantDb.MembershipPlans.FindAsync(planId);
            if (existing is null) return NotFound();

            existing.IsActive = false;
            await tenantDb.SaveChangesAsync();
            return Ok(existing);
        }
    }
}