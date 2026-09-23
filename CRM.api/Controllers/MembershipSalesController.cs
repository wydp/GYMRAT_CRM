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
                .Where(x => x.IsActive)
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
    }
}