using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.infrastructure.Services
{
    public interface ITenantDatabaseResolver
    {
        Task<TenantDatabaseInfo> GetDatabaseInfoAsync(int companyId);
    }
}
