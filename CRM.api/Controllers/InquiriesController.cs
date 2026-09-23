using CRM.domain.Entities;
using CRM.infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.api.Controllers
{
    [ApiController]
    [Route("tenant/{companyId:int}/inquiries")]
    public class InquiriesController : ControllerBase
    {
        private readonly ITenantDbContextFactory _tenantFactory;

        public InquiriesController(ITenantDbContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Create(int companyId, [FromBody] Inquiry inquiry)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);

            var customerExists = await tenantDb.Customers.AnyAsync(c => c.CustomerId == inquiry.CustomerId);
            if (!customerExists)
                return BadRequest(new { message = "The specified customer does not exist." });

            tenantDb.Inquiries.Add(inquiry);
            await tenantDb.SaveChangesAsync();
            return Created($"/tenant/{companyId}/inquiries/{inquiry.InquiryId}", inquiry);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int companyId)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var inquiries = await tenantDb.Inquiries
                .Where(x => x.IsActive)
                .Include(x => x.Customer)
                .AsNoTracking()
                .OrderBy(x => x.InquiryId)
                .ToListAsync();
            return Ok(inquiries);
        }

        [HttpPut("{inquiryId:int}/status")]
        public async Task<IActionResult> UpdateStatus(int companyId, int inquiryId, [FromBody] UpdateStatusRequest request)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var existing = await tenantDb.Inquiries.FindAsync(inquiryId);
            if (existing is null) return NotFound();

            existing.Status = request.Status;
            await tenantDb.SaveChangesAsync();
            return Ok(existing);
        }

        [HttpDelete("{inquiryId:int}")]
        public async Task<IActionResult> Delete(int companyId, int inquiryId)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var existing = await tenantDb.Inquiries.FindAsync(inquiryId);
            if (existing is null) return NotFound();

            existing.IsActive = false;
            await tenantDb.SaveChangesAsync();
            return Ok(existing);
        }
    }
}