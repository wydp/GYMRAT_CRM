namespace CRM.domain.Entities
{
    public class Branch
    {
        public int BranchId { get; set; }
        public string BranchCode { get; set; } = string.Empty;   // unique within tenant
        public string BranchName { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? ContactNumber { get; set; }

        // ManagerUserId — nullable; a branch might not have a manager yet.
        // Cross-DB reference — no FK enforced by SQL Server.
        public int? ManagerUserId { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}