using CRM.domain.Entities;
using CRM.infrastructure.Data;
using CRM.infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.api.Controllers
{
    [ApiController]
    [Route("")]
    public class MasterController : ControllerBase
    {
        private readonly MasterCrmDbContext _masterDb;
        private readonly ITenantDbContextFactory _tenantFactory;

        public MasterController(MasterCrmDbContext masterDb, ITenantDbContextFactory tenantFactory)
        {
            _masterDb = masterDb;
            _tenantFactory = tenantFactory;
        }

        // POST /companies
        [HttpPost("companies")]
        public async Task<IActionResult> CreateCompany([FromBody] Company company)
        {
            _masterDb.Companies.Add(company);
            await _masterDb.SaveChangesAsync();
            return Created($"/companies/{company.CompanyId}", company);
        }

        // POST /devices
        [HttpPost("devices")]
        public async Task<IActionResult> CreateDevice([FromBody] Device device)
        {
            _masterDb.Devices.Add(device);
            await _masterDb.SaveChangesAsync();
            return Created($"/devices/{device.DeviceId}", device);
        }

        // POST /company-databases
        [HttpPost("company-databases")]
        public async Task<IActionResult> CreateCompanyDatabase([FromBody] CompanyDatabase companyDatabase)
        {
            _masterDb.CompanyDatabases.Add(companyDatabase);
            await _masterDb.SaveChangesAsync();
            return Created($"/company-databases/{companyDatabase.CompanyDatabaseId}", companyDatabase);
        }

        // GET /test-tenant/{companyId}
        [HttpGet("test-tenant/{companyId:int}")]
        public async Task<IActionResult> TestTenant(int companyId)
        {
            await using var tenantDb = await _tenantFactory.CreateAsync(companyId);
            var productCount = await tenantDb.Products.CountAsync();
            return Ok(new { companyId, productCount });
        }
    }
}