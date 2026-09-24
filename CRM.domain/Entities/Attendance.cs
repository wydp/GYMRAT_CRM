using System;

namespace CRM.domain.Entities
{
    public class Attendance
    {
        public int AttendanceId { get; set; }
        public int CustomerId { get; set; }

        // Where they checked in. Cross-reference to Branch (same tenant DB),
        // but no FK enforced — Branch is a lookup, not a dependency.
        public int? BranchId { get; set; }

        public DateTime CheckInTime { get; set; } = DateTime.UtcNow;
        public DateTime? CheckOutTime { get; set; }

        public string? Notes { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation
        public Customer? Customer { get; set; }
    }
}