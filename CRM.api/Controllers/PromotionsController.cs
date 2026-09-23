using CRM.domain.Entities;
using CRM.infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CRM.api.Controllers
{
    [ApiController]
    [Route("tenant/{companyId:int}/promotions")]
    public class PromotionsController : ControllerBase
    {
        private readonly ITenantDbContextFactory _tenantFactory;

        public PromotionsController(ITenantDbContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Create(int companyId, [FromBody] Promotion promotion)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            tenantDb.Promotions.Add(promotion);

            try { await tenantDb.SaveChangesAsync(); }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sql && (sql.Number == 2601 || sql.Number == 2627))
            {
                return Conflict(new { message = "A promotion with this code already exists." });
            }

            return Created($"/tenant/{companyId}/promotions/{promotion.PromotionId}", promotion);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int companyId)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var promotions = await tenantDb.Promotions
                .Where(x => x.IsActive)
                .AsNoTracking()
                .OrderBy(x => x.PromotionId)
                .ToListAsync();
            return Ok(promotions);
        }

        [HttpPut("{promotionId:int}")]
        public async Task<IActionResult> Update(int companyId, int promotionId, [FromBody] Promotion updated)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var existing = await tenantDb.Promotions.FindAsync(promotionId);
            if (existing is null) return NotFound();

            existing.PromotionCode = updated.PromotionCode;
            existing.PromotionName = updated.PromotionName;
            existing.Description = updated.Description;
            existing.DiscountType = updated.DiscountType;
            existing.DiscountValue = updated.DiscountValue;
            existing.StartDate = updated.StartDate;
            existing.EndDate = updated.EndDate;
            existing.IsActive = updated.IsActive;

            try { await tenantDb.SaveChangesAsync(); }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sql && (sql.Number == 2601 || sql.Number == 2627))
            {
                return Conflict(new { message = "A promotion with this code already exists." });
            }

            return Ok(existing);
        }

        [HttpDelete("{promotionId:int}")]
        public async Task<IActionResult> Delete(int companyId, int promotionId)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var existing = await tenantDb.Promotions.FindAsync(promotionId);
            if (existing is null) return NotFound();

            existing.IsActive = false;
            await tenantDb.SaveChangesAsync();
            return Ok(existing);
        }
    }
}