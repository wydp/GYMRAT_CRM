using CRM.domain.Entities;
using CRM.infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.api.Controllers
{
    [ApiController]
    [Route("tenant/{companyId:int}/feedbacks")]
    public class FeedbacksController : ControllerBase
    {
        private readonly ITenantDbContextFactory _tenantFactory;

        public FeedbacksController(ITenantDbContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Create(int companyId, [FromBody] Feedback feedback)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);

            var customerExists = await tenantDb.Customers.AnyAsync(c => c.CustomerId == feedback.CustomerId);
            if (!customerExists)
                return BadRequest(new { message = "The specified customer does not exist." });

            tenantDb.Feedbacks.Add(feedback);
            await tenantDb.SaveChangesAsync();
            return Created($"/tenant/{companyId}/feedbacks/{feedback.FeedbackId}", feedback);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int companyId)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var feedbacks = await tenantDb.Feedbacks
                .Where(x => x.IsActive)
                .Include(x => x.Customer)
                .AsNoTracking()
                .OrderBy(x => x.FeedbackId)
                .ToListAsync();
            return Ok(feedbacks);
        }

        [HttpPut("{feedbackId:int}/status")]
        public async Task<IActionResult> UpdateStatus(int companyId, int feedbackId, [FromBody] UpdateStatusRequest request)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var existing = await tenantDb.Feedbacks.FindAsync(feedbackId);
            if (existing is null) return NotFound();

            existing.Status = request.Status;
            await tenantDb.SaveChangesAsync();
            return Ok(existing);
        }

        [HttpDelete("{feedbackId:int}")]
        public async Task<IActionResult> Delete(int companyId, int feedbackId)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var existing = await tenantDb.Feedbacks.FindAsync(feedbackId);
            if (existing is null) return NotFound();

            existing.IsActive = false;
            await tenantDb.SaveChangesAsync();
            return Ok(existing);
        }
    }
}