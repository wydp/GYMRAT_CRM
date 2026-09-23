using CRM.domain.Entities;
using CRM.infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CRM.api.Controllers
{
    [ApiController]
    [Route("tenant/{companyId:int}/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ITenantDbContextFactory _tenantFactory;

        public CustomersController(ITenantDbContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        // POST /tenant/{companyId}/customers
        [HttpPost]
        public async Task<IActionResult> Create(int companyId, [FromBody] Customer customer)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            tenantDb.Customers.Add(customer);

            try
            {
                await tenantDb.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sql && (sql.Number == 2601 || sql.Number == 2627))
            {
                return Conflict(new { message = "A customer with this code already exists." });
            }

            return CreatedAtAction(nameof(GetById), new { companyId, customerId = customer.CustomerId }, customer);
        }

        // GET /tenant/{companyId}/customers
        [HttpGet]
        public async Task<IActionResult> GetAll(int companyId)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var customers = await tenantDb.Customers
                .Where(x => x.IsActive)
                .AsNoTracking()
                .OrderBy(x => x.CustomerId)
                .ToListAsync();
            return Ok(customers);
        }

        // GET /tenant/{companyId}/customers/{customerId}
        [HttpGet("{customerId:int}")]
        public async Task<IActionResult> GetById(int companyId, int customerId)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var customer = await tenantDb.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CustomerId == customerId);
            if (customer is null) return NotFound();
            return Ok(customer);
        }

        // PUT /tenant/{companyId}/customers/{customerId}
        [HttpPut("{customerId:int}")]
        public async Task<IActionResult> Update(int companyId, int customerId, [FromBody] Customer updated)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var existing = await tenantDb.Customers.FindAsync(customerId);
            if (existing is null) return NotFound();

            existing.CustomerCode = updated.CustomerCode;
            existing.CustomerName = updated.CustomerName;
            existing.ContactNumber = updated.ContactNumber;
            existing.EmailAddress = updated.EmailAddress;
            existing.Address = updated.Address;
            existing.IsActive = updated.IsActive;

            try
            {
                await tenantDb.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sql && (sql.Number == 2601 || sql.Number == 2627))
            {
                return Conflict(new { message = "A customer with this code already exists." });
            }

            return Ok(existing);
        }

        // DELETE /tenant/{companyId}/customers/{customerId}
        [HttpDelete("{customerId:int}")]
        public async Task<IActionResult> Delete(int companyId, int customerId)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var existing = await tenantDb.Customers.FindAsync(customerId);
            if (existing is null) return NotFound();

            existing.IsActive = false;
            await tenantDb.SaveChangesAsync();
            return Ok(existing);
        }
    }
}