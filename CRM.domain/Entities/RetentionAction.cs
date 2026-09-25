using System;

namespace CRM.domain.Entities
{
    public class RetentionAction
    {
        public int RetentionActionId { get; set; }
        public int CustomerId { get; set; }

        // Who performed the action. Cross-DB reference (User lives in Master DB).
        // No FK enforced — validated in the API layer.
        public int PerformedByUserId { get; set; }

        public RetentionActionType ActionType { get; set; }
        public string? Notes { get; set; }

        // For "Schedule Follow-Up Call" — when the follow-up is due.
        public DateTime? FollowUpDate { get; set; }

        public RetentionOutcome Outcome { get; set; } = RetentionOutcome.Pending;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        // Navigation
        public Customer? Customer { get; set; }
    }

    public enum RetentionActionType
    {
        CampaignSent,       // "Send Retention Campaign"
        FollowUpScheduled,  // "Schedule Follow-Up Call"
        FreezeApplied,      // "Manage Membership Freeze"
        FreezeLifted,       // unfrozen
        WinBackAttempt,     // for the win-back pipeline
        Cancelled           // membership cancelled via SFA

    }

    public enum RetentionOutcome
    {
        Pending,    // no result yet
        Renewed,    // they renewed
        Lost,       // they left
        Ignored     // they didn't respond
    }
}