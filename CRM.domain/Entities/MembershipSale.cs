using System;

namespace CRM.domain.Entities
{
    public class MembershipSale
    {
        public int MembershipSaleId { get; set; }
        public int CustomerId { get; set; }
        public int MembershipPlanId { get; set; }
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;

        // Snapshot of what was actually paid — not a live lookup of the plan's
        // current price, so past sales stay accurate even if prices change later.
        public decimal AmountPaid { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // ---- cancellation ----
        // Separate from IsActive: IsActive is soft-delete of the row;
        // IsCancelled is the business fact that the membership was cancelled.
        public bool IsCancelled { get; set; } = false;
        public DateTime? CancelledAt { get; set; }
        public string? CancellationReason { get; set; }

        public Customer? Customer { get; set; }
        public MembershipPlan? MembershipPlan { get; set; }
    }
}