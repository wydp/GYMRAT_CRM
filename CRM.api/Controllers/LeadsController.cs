using CRM.domain.Entities;
using CRM.infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CRM.api.Controllers
{
    [ApiController]
    [Route("tenant/{companyId:int}/leads")]
    public class LeadsController : ControllerBase
    {
        private readonly ITenantDbContextFactory _tenantFactory;

        public LeadsController(ITenantDbContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        // ---- request shapes ----

        public class ConvertLeadRequest
        {
            public string CustomerCode { get; set; } = string.Empty;   // required for the new customer
            public string? Address { get; set; }                        // optional override
        }

        public class UpdateLeadStatusRequest
        {
            public LeadStatus Status { get; set; }
        }

        // ============================================================
        // POST /leads
        // ============================================================
        [HttpPost]
        public async Task<IActionResult> Create(int companyId, [FromBody] Lead lead)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);

            if (string.IsNullOrWhiteSpace(lead.LeadCode) || string.IsNullOrWhiteSpace(lead.FullName))
                return BadRequest(new { message = "LeadCode and FullName are required." });

            // Default status
            if (lead.Status == 0) lead.Status = LeadStatus.New;

            tenantDb.Leads.Add(lead);

            try { await tenantDb.SaveChangesAsync(); }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sql && (sql.Number == 2601 || sql.Number == 2627))
            {
                return Conflict(new { message = "A lead with this code already exists." });
            }

            return Created($"/tenant/{companyId}/leads/{lead.LeadId}", lead);
        }

        // ============================================================
        // GET /leads
        // ============================================================
        [HttpGet]
        public async Task<IActionResult> GetAll(int companyId)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var leads = await tenantDb.Leads
                .Where(x => x.IsActive)
                .Include(x => x.ConvertedCustomer)
                .AsNoTracking()
                .OrderByDescending(x => x.LeadId)
                .ToListAsync();
            return Ok(leads);
        }

        // ============================================================
        // GET /leads/{id}
        // ============================================================
        [HttpGet("{leadId:int}")]
        public async Task<IActionResult> GetById(int companyId, int leadId)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var lead = await tenantDb.Leads
                .Include(x => x.ConvertedCustomer)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.LeadId == leadId);
            if (lead is null) return NotFound();
            return Ok(lead);
        }

        // ============================================================
        // PUT /leads/{id}
        // ============================================================
        [HttpPut("{leadId:int}")]
        public async Task<IActionResult> Update(int companyId, int leadId, [FromBody] Lead updated)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var existing = await tenantDb.Leads.FindAsync(leadId);
            if (existing is null) return NotFound();

            existing.LeadCode = updated.LeadCode;
            existing.FullName = updated.FullName;
            existing.ContactNumber = updated.ContactNumber;
            existing.EmailAddress = updated.EmailAddress;
            existing.Address = updated.Address;
            existing.Source = updated.Source;
            existing.Status = updated.Status;
            existing.Notes = updated.Notes;
            existing.IsActive = updated.IsActive;

            try { await tenantDb.SaveChangesAsync(); }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sql && (sql.Number == 2601 || sql.Number == 2627))
            {
                return Conflict(new { message = "A lead with this code already exists." });
            }

            return Ok(existing);
        }

        // ============================================================
        // PUT /leads/{id}/status
        // ============================================================
        [HttpPut("{leadId:int}/status")]
        public async Task<IActionResult> UpdateStatus(int companyId, int leadId, [FromBody] UpdateLeadStatusRequest request)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var existing = await tenantDb.Leads.FindAsync(leadId);
            if (existing is null) return NotFound();

            existing.Status = request.Status;
            await tenantDb.SaveChangesAsync();
            return Ok(existing);
        }

        // ============================================================
        // POST /leads/{id}/convert
        // Converts a lead to a customer. Creates a Customer, links it, sets status = Converted.
        // ============================================================
        [HttpPost("{leadId:int}/convert")]
        public async Task<IActionResult> Convert(int companyId, int leadId, [FromBody] ConvertLeadRequest request)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var lead = await tenantDb.Leads.FindAsync(leadId);
            if (lead is null) return NotFound();

            if (lead.Status == LeadStatus.Converted)
                return BadRequest(new { message = "This lead has already been converted." });

            if (string.IsNullOrWhiteSpace(request.CustomerCode))
                return BadRequest(new { message = "CustomerCode is required to convert a lead." });

            // Ensure the customer code is unique
            var codeInUse = await tenantDb.Customers.AnyAsync(c => c.CustomerCode == request.CustomerCode);
            if (codeInUse)
                return Conflict(new { message = "A customer with this code already exists." });

            // Create the customer
            var customer = new Customer
            {
                CustomerCode = request.CustomerCode.Trim(),
                CustomerName = lead.FullName,
                ContactNumber = lead.ContactNumber,
                EmailAddress = lead.EmailAddress,
                Address = string.IsNullOrWhiteSpace(request.Address) ? lead.Address : request.Address,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            };

            tenantDb.Customers.Add(customer);

            try
            {
                await tenantDb.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sql && (sql.Number == 2601 || sql.Number == 2627))
            {
                return Conflict(new { message = "A customer with this code already exists." });
            }

            // Link and mark the lead as converted
            lead.ConvertedCustomerId = customer.CustomerId;
            lead.ConvertedAt = DateTime.UtcNow;
            lead.Status = LeadStatus.Converted;

            await tenantDb.SaveChangesAsync();

            return Ok(new
            {
                message = $"Lead converted. Customer {customer.CustomerCode} created.",
                customer,
                lead,
            });
        }

        // ============================================================
        // DELETE /leads/{id}
        // ============================================================
        [HttpDelete("{leadId:int}")]
        public async Task<IActionResult> Delete(int companyId, int leadId)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var existing = await tenantDb.Leads.FindAsync(leadId);
            if (existing is null) return NotFound();

            existing.IsActive = false;
            await tenantDb.SaveChangesAsync();
            return Ok(existing);
        }
    }
}