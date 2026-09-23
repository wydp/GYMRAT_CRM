namespace CRM.domain.Entities
{
    public class AuditLog
    {
        public int AuditLogId { get; set; }

        // Who
        public int UserId { get; set; }

        // What
        public string Action { get; set; } = string.Empty;       // "create", "update", "delete", "login", "logout"
        public string EntityType { get; set; } = string.Empty;   // "Customer", "Promotion", "User", etc.
        public int? EntityId { get; set; }                       // nullable — some actions have no specific entity
        public string? Details { get; set; }                     // free-text; e.g. "Changed price from 799 to 899"

        // When
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        // Where (branch scoping)
        public int? BranchId { get; set; }

        // Navigation
        public User? User { get; set; }
    }
}