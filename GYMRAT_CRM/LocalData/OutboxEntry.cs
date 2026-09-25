using System;

namespace CRM.winforms.LocalData
{
    public class OutboxEntry
    {
        public Guid OutboxEntryId { get; set; } = Guid.NewGuid();
        public string EntityType { get; set; } = string.Empty; // e.g., "Customer", "MembershipPlan"
        public string Operation { get; set; } = string.Empty; // Create|Update|Delete
        public string Payload { get; set; } = string.Empty; // JSON payload
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public int AttemptCount { get; set; } = 0;
    }
}
