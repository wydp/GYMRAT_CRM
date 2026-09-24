using CRM.domain.Entities;
using CRM.infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CRM.api.Controllers
{
    [ApiController]
    [Route("tenant/{companyId:int}/campaigns")]
    public class CampaignsController : ControllerBase
    {
        private readonly ITenantDbContextFactory _tenantFactory;

        public CampaignsController(ITenantDbContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Create(int companyId, [FromBody] Campaign campaign)
        {
            if (!HasAnyAudience(campaign))
                return BadRequest(new { message = "Select at least one target audience for this campaign." });

            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            tenantDb.Campaigns.Add(campaign);

            try { await tenantDb.SaveChangesAsync(); }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sql && (sql.Number == 2601 || sql.Number == 2627))
            {
                return Conflict(new { message = "A campaign with this code already exists." });
            }

            return Created($"/tenant/{companyId}/campaigns/{campaign.CampaignId}", campaign);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int companyId)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var campaigns = await tenantDb.Campaigns
                .Where(x => x.IsActive)
                .AsNoTracking()
                .OrderBy(x => x.CampaignId)
                .ToListAsync();
            return Ok(campaigns);
        }

        [HttpPut("{campaignId:int}")]
        public async Task<IActionResult> Update(int companyId, int campaignId, [FromBody] Campaign updated)
        {
            if (!HasAnyAudience(updated))
                return BadRequest(new { message = "Select at least one target audience for this campaign." });

            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var existing = await tenantDb.Campaigns.FindAsync(campaignId);
            if (existing is null) return NotFound();

            existing.CampaignCode = updated.CampaignCode;
            existing.CampaignName = updated.CampaignName;
            existing.Description = updated.Description;
            existing.Reason = updated.Reason;
            existing.TargetAllMembers = updated.TargetAllMembers;
            existing.TargetPWD = updated.TargetPWD;
            existing.TargetSenior = updated.TargetSenior;
            existing.TargetStudent = updated.TargetStudent;
            existing.TargetCorporate = updated.TargetCorporate;
            existing.StartDate = updated.StartDate;
            existing.EndDate = updated.EndDate;
            existing.Status = updated.Status;
            existing.IsActive = updated.IsActive;

            try { await tenantDb.SaveChangesAsync(); }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sql && (sql.Number == 2601 || sql.Number == 2627))
            {
                return Conflict(new { message = "A campaign with this code already exists." });
            }

            return Ok(existing);
        }

        [HttpPut("{campaignId:int}/status")]
        public async Task<IActionResult> UpdateStatus(int companyId, int campaignId, [FromBody] UpdateCampaignStatusRequest request)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var existing = await tenantDb.Campaigns.FindAsync(campaignId);
            if (existing is null) return NotFound();

            existing.Status = request.Status;
            await tenantDb.SaveChangesAsync();
            return Ok(existing);
        }

        [HttpDelete("{campaignId:int}")]
        public async Task<IActionResult> Delete(int companyId, int campaignId)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var existing = await tenantDb.Campaigns.FindAsync(campaignId);
            if (existing is null) return NotFound();

            existing.IsActive = false;
            await tenantDb.SaveChangesAsync();
            return Ok(existing);
        }

        private static bool HasAnyAudience(Campaign c)
        {
            return c.TargetAllMembers
                || c.TargetPWD
                || c.TargetSenior
                || c.TargetStudent
                || c.TargetCorporate;
        }
    }
}