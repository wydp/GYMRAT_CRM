namespace CRM.domain.Entities
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public string Code { get; set; } = string.Empty;  // e.g. "customers.create", "reports.view"
        public string Description { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty; // e.g. "Customers", "Reports" — for grouping

        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}