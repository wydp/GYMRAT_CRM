using CRM.domain.Entities;

namespace CRM.domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;      // unique
        public string PasswordHash { get; set; } = string.Empty;  // never store plain text
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }

        // RoleId FK — every user must have a role
        public int RoleId { get; set; }

        // CompanyId — null for Super Admin (platform-level user)
        // set for Admin / Manager / Cashier (tenant-scoped)
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }   // navigation property for the FK

        // BranchId — null for Super Admin and Admin (Admin sees all branches)
        // set for Manager and Cashier (they belong to one branch)
        public int? BranchId { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; set; }

        // Navigation
        public Role? Role { get; set; }
    }
}