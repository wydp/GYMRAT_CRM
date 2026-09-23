using CRM.domain.Entities;
using CRM.infrastructure.Data;
using CRM.infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.api.Controllers
{
    // ONE-TIME SEED CONTROLLER — DELETE THIS ENTIRE FILE after seeding.
    // Guarded by IsDevelopment() so it can't be hit in Production.
    [ApiController]
    [Route("dev/seed")]
    public class SeedController : ControllerBase
    {
        private readonly MasterCrmDbContext _masterDb;
        private readonly ITenantDbContextFactory _tenantFactory;
        private readonly IWebHostEnvironment _env;

        public SeedController(
            MasterCrmDbContext masterDb,
            ITenantDbContextFactory tenantFactory,
            IWebHostEnvironment env)
        {
            _masterDb = masterDb;
            _tenantFactory = tenantFactory;
            _env = env;
        }

        // POST /dev/seed/run
        [HttpPost("run")]
        public async Task<IActionResult> Run()
        {
            if (!_env.IsDevelopment()) return NotFound();

            // Idempotency guard — don't seed twice
            if (await _masterDb.Roles.AnyAsync())
                return BadRequest(new { message = "Seed already run — Roles table is not empty. Delete all data first if you want to reseed." });

            // ---------- 1. Roles ----------
            var roles = new List<Role>
            {
                new() { RoleName = "SuperAdmin", Description = "Platform-level user — manages tenants, no business data access" },
                new() { RoleName = "Admin",      Description = "Gym owner — full access to all branches of their gym" },
                new() { RoleName = "Manager",    Description = "Branch manager — access limited to assigned branch" },
                new() { RoleName = "Cashier",    Description = "Front-desk staff — limited to daily operations" },
            };
            _masterDb.Roles.AddRange(roles);
            await _masterDb.SaveChangesAsync();

            var roleId = roles.ToDictionary(r => r.RoleName, r => r.RoleId);

            // ---------- 2. Permissions ----------
            var permissions = new List<Permission>
            {
                // Module-level (13)
                new() { Code = "platform.access",                 Module = "Platform Management",   Description = "Access platform admin section" },
                new() { Code = "dashboard.access",                Module = "Dashboard",             Description = "Access tenant dashboard" },
                new() { Code = "dashboard.superadmin",            Module = "Dashboard",             Description = "Access Super Admin subscription dashboard" },
                new() { Code = "branch.access",                   Module = "Branch Management",     Description = "Manage gym branches" },
                new() { Code = "staff.access",                    Module = "Staff Management",      Description = "Manage staff accounts" },
                new() { Code = "support.access",                  Module = "Customer Support",      Description = "Access inquiries and feedback" },
                new() { Code = "marketing.access",                Module = "Marketing Automation",  Description = "Access campaigns and promotions" },
                new() { Code = "sfa.access",                      Module = "Sales Force Automation", Description = "Access sales, memberships, renewals" },
                new() { Code = "retention.access",                Module = "Retention",             Description = "Access retention module" },
                new() { Code = "reports.access",                  Module = "Reports",               Description = "Access reports module" },
                new() { Code = "audit.access",                    Module = "Audit Log",             Description = "Access audit log" },
                new() { Code = "communication.access",            Module = "Communication",         Description = "Send SMS/email" },
                new() { Code = "terms.access",                    Module = "Terms & Conditions",    Description = "View terms and conditions" },

                // Fine-grained (20)
                new() { Code = "support.log_feedback",            Module = "Customer Support",      Description = "Log new customer feedback" },
                new() { Code = "support.manage_feedback",         Module = "Customer Support",      Description = "Update/resolve feedback" },
                new() { Code = "marketing.manage_campaigns",      Module = "Marketing Automation",  Description = "Create/edit/delete campaigns" },
                new() { Code = "sfa.process_sale",                Module = "Sales Force Automation", Description = "Process new membership sale" },
                new() { Code = "sfa.cancel_membership",           Module = "Sales Force Automation", Description = "Cancel a membership" },
                new() { Code = "sfa.freeze_membership",           Module = "Sales Force Automation", Description = "Freeze a membership" },
                new() { Code = "sfa.upgrade_membership",          Module = "Sales Force Automation", Description = "Upgrade a membership" },
                new() { Code = "sfa.issue_refund",                Module = "Sales Force Automation", Description = "Issue a refund" },
                new() { Code = "retention.send_campaign",         Module = "Retention",             Description = "Send retention campaign" },
                new() { Code = "retention.log_outcome",           Module = "Retention",             Description = "Log retention outcome" },
                new() { Code = "reports.view",                    Module = "Reports",               Description = "View reports" },
                new() { Code = "reports.export",                  Module = "Reports",               Description = "Export reports to file" },
                new() { Code = "reports.branch_comparison",       Module = "Reports",               Description = "View branch comparison report" },
                new() { Code = "staff.create_manager",            Module = "Staff Management",      Description = "Create Manager accounts" },
                new() { Code = "staff.create_cashier",            Module = "Staff Management",      Description = "Create Cashier accounts" },
                new() { Code = "audit.view",                      Module = "Audit Log",             Description = "View audit log entries" },
                new() { Code = "communication.send",              Module = "Communication",         Description = "Send SMS/email messages" },
                new() { Code = "communication.schedule_followup", Module = "Communication",         Description = "Schedule follow-up" },
                new() { Code = "terms.manage_platform",           Module = "Terms & Conditions",    Description = "Manage platform T&C" },
                new() { Code = "terms.manage_gym",                Module = "Terms & Conditions",    Description = "Manage gym T&C" },
            };
            _masterDb.Permissions.AddRange(permissions);
            await _masterDb.SaveChangesAsync();

            var permId = permissions.ToDictionary(p => p.Code, p => p.PermissionId);

            // ---------- 3. Role → Permission mappings ----------
            var rolePermissions = new List<RolePermission>();

            void Grant(string role, params string[] codes)
            {
                foreach (var code in codes)
                    rolePermissions.Add(new RolePermission { RoleId = roleId[role], PermissionId = permId[code] });
            }

            // SuperAdmin
            Grant("SuperAdmin",
                "platform.access", "dashboard.superadmin", "terms.access", "terms.manage_platform",
                "audit.access", "audit.view");

            // Admin
            Grant("Admin",
                "dashboard.access", "branch.access",
                "staff.access", "staff.create_manager", "staff.create_cashier",
                "support.access", "support.manage_feedback", "support.log_feedback",
                "marketing.access", "marketing.manage_campaigns",
                "sfa.access", "sfa.process_sale", "sfa.cancel_membership", "sfa.freeze_membership",
                "sfa.upgrade_membership", "sfa.issue_refund",
                "retention.access", "retention.send_campaign", "retention.log_outcome",
                "reports.access", "reports.view", "reports.export", "reports.branch_comparison",
                "audit.access", "audit.view",
                "communication.access", "communication.send", "communication.schedule_followup",
                "terms.access", "terms.manage_gym");

            // Manager
            Grant("Manager",
                "dashboard.access",
                "staff.access", "staff.create_cashier",
                "support.access", "support.manage_feedback", "support.log_feedback",
                "marketing.access", "marketing.manage_campaigns",
                "sfa.access", "sfa.process_sale", "sfa.cancel_membership", "sfa.freeze_membership",
                "sfa.upgrade_membership", "sfa.issue_refund",
                "retention.access", "retention.send_campaign", "retention.log_outcome",
                "reports.access", "reports.view", "reports.export",
                "audit.access", "audit.view",
                "communication.access", "communication.send", "communication.schedule_followup",
                "terms.access");

            // Cashier
            Grant("Cashier",
                "dashboard.access",
                "support.access", "support.log_feedback",
                "sfa.access", "sfa.process_sale",
                "communication.access", "communication.send",
                "terms.access");

            _masterDb.RolePermissions.AddRange(rolePermissions);
            await _masterDb.SaveChangesAsync();

            // ---------- 4. Initial users ----------
            const string defaultPassword = "Admin@123";
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(defaultPassword);

            var users = new List<User>
            {
                new() { Username = "superadmin", PasswordHash = passwordHash, FullName = "Platform Super Admin", Email = "superadmin@gymrat.local", RoleId = roleId["SuperAdmin"], CompanyId = null, BranchId = null, IsActive = true },
                new() { Username = "admin",      PasswordHash = passwordHash, FullName = "Gym Owner",             Email = "admin@gymrat.local",      RoleId = roleId["Admin"],      CompanyId = 1,    BranchId = null, IsActive = true },
                new() { Username = "manager",    PasswordHash = passwordHash, FullName = "Branch Manager",        Email = "manager@gymrat.local",    RoleId = roleId["Manager"],    CompanyId = 1,    BranchId = 1,    IsActive = true },
                new() { Username = "cashier",    PasswordHash = passwordHash, FullName = "Front Desk Cashier",    Email = "cashier@gymrat.local",    RoleId = roleId["Cashier"],    CompanyId = 1,    BranchId = 1,    IsActive = true },
            };
            _masterDb.Users.AddRange(users);
            await _masterDb.SaveChangesAsync();

            // ---------- 5. Initial branch (tenant DB) ----------
            var createdBranchId = 0;
            try
            {
                await using var tenantDb = await _tenantFactory.CreateAsync(1);
                if (!await tenantDb.Branches.AnyAsync())
                {
                    var branch = new Branch
                    {
                        BranchCode = "MAIN-01",
                        BranchName = "Main Branch",
                        Address = "Davao City",
                        ContactNumber = "09171234567",
                        IsActive = true,
                    };
                    tenantDb.Branches.Add(branch);
                    await tenantDb.SaveChangesAsync();
                    createdBranchId = branch.BranchId;
                }
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    message = "Master data seeded, but branch seeding failed.",
                    error = ex.Message,
                    rolesCreated = roles.Count,
                    permissionsCreated = permissions.Count,
                    rolePermissionsCreated = rolePermissions.Count,
                    usersCreated = users.Count,
                    branchCreated = 0,
                    defaultPassword,
                });
            }

            return Ok(new
            {
                message = "Seed complete.",
                rolesCreated = roles.Count,
                permissionsCreated = permissions.Count,
                rolePermissionsCreated = rolePermissions.Count,
                usersCreated = users.Count,
                branchCreated = createdBranchId > 0 ? 1 : 0,
                defaultPassword,
                users = users.Select(u => new { u.Username, Role = u.RoleId }).ToList(),
            });
        }
    }
}