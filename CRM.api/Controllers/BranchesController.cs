using CRM.domain.Entities;
using CRM.infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CRM.api.Controllers
{
    [ApiController]
    [Route("tenant/{companyId:int}/branches")]
    [Authorize]
    public class BranchesController : ControllerBase
    {
        private readonly ITenantDbContextFactory _tenantFactory;

        public BranchesController(ITenantDbContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        // GET /tenant/{companyId}/branches
        [HttpGet]
        public async Task<IActionResult> GetAll(int companyId)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var branches = await tenantDb.Branches
                .Where(x => x.IsActive)
                .AsNoTracking()
                .OrderBy(x => x.BranchId)
                .ToListAsync();
            return Ok(branches);
        }

        // GET /tenant/{companyId}/branches/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int companyId, int id)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var branch = await tenantDb.Branches.AsNoTracking().FirstOrDefaultAsync(x => x.BranchId == id);
            if (branch is null) return NotFound();
            return Ok(branch);
        }

        // POST /tenant/{companyId}/branches
        [HttpPost]
        public async Task<IActionResult> Create(int companyId, [FromBody] Branch branch)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            tenantDb.Branches.Add(branch);

            try
            {
                await tenantDb.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sql && (sql.Number == 2601 || sql.Number == 2627))
            {
                return Conflict(new { message = "A branch with this code already exists." });
            }

            return Created($"/tenant/{companyId}/branches/{branch.BranchId}", branch);
        }

        // PUT /tenant/{companyId}/branches/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int companyId, int id, [FromBody] Branch updated)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var existing = await tenantDb.Branches.FindAsync(id);
            if (existing is null) return NotFound();

            existing.BranchCode = updated.BranchCode;
            existing.BranchName = updated.BranchName;
            existing.Address = updated.Address;
            existing.ContactNumber = updated.ContactNumber;
            existing.ManagerUserId = updated.ManagerUserId;
            existing.IsActive = updated.IsActive;

            try
            {
                await tenantDb.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sql && (sql.Number == 2601 || sql.Number == 2627))
            {
                return Conflict(new { message = "A branch with this code already exists." });
            }

            return Ok(existing);
        }

        // DELETE /tenant/{companyId}/branches/{id}  — soft delete
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int companyId, int id)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var existing = await tenantDb.Branches.FindAsync(id);
            if (existing is null) return NotFound();

            existing.IsActive = false;
            await tenantDb.SaveChangesAsync();
            return Ok(existing);
        }
    }
}