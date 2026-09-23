namespace CRM.domain.Entities
{
    public class RolePermission
    {
        public int RolePermissionId { get; set; }

        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        // Navigation
        public Role? Role { get; set; }
        public Permission? Permission { get; set; }
    }
}