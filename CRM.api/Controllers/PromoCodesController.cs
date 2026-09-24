using CRM.domain.Entities;
using CRM.infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CRM.api.Controllers
{
    [ApiController]
    [Route("tenant/{companyId:int}/promocodes")]
    public class PromoCodesController : ControllerBase
    {
        private readonly ITenantDbContextFactory _tenantFactory;

        public PromoCodesController(ITenantDbContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Create(int companyId, [FromBody] PromoCode promoCode)
        {
            if (string.IsNullOrWhiteSpace(promoCode.Code))
                return BadRequest(new { message = "Code is required." });

            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);

            var promotionExists = await tenantDb.Promotions
                .AnyAsync(p => p.PromotionId == promoCode.PromotionId);
            if (!promotionExists)
                return BadRequest(new { message = "The specified promotion does not exist." });

            // Force uppercase for consistency
            promoCode.Code = promoCode.Code.Trim().ToUpperInvariant();

            tenantDb.PromoCodes.Add(promoCode);

            try { await tenantDb.SaveChangesAsync(); }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sql && (sql.Number == 2601 || sql.Number == 2627))
            {
                return Conflict(new { message = "A promo code with this value already exists." });
            }

            // Reload with promotion for the response
            await tenantDb.Entry(promoCode).Reference(x => x.Promotion).LoadAsync();

            return Created($"/tenant/{companyId}/promocodes/{promoCode.PromoCodeId}", promoCode);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int companyId)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var codes = await tenantDb.PromoCodes
                .Where(x => x.IsActive)
                .Include(x => x.Promotion)
                .AsNoTracking()
                .OrderBy(x => x.PromoCodeId)
                .ToListAsync();
            return Ok(codes);
        }

        [HttpDelete("{promoCodeId:int}")]
        public async Task<IActionResult> Delete(int companyId, int promoCodeId)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var existing = await tenantDb.PromoCodes.FindAsync(promoCodeId);
            if (existing is null) return NotFound();

            existing.IsActive = false;
            await tenantDb.SaveChangesAsync();
            return Ok(existing);
        }
    }
}