using CRM.infrastructure.Data;

namespace CRM.infrastructure.Services
{
    public interface ITenantDbContextFactory
    {
        Task<TenantCrmDbContext> CreateAsync(int companyId);
    }
}