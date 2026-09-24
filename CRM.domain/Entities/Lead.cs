using System;

namespace CRM.domain.Entities
{
    public class Lead
    {
        public int LeadId { get; set; }
        public string LeadCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? ContactNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? Address { get; set; }

        public LeadSource Source { get; set; }
        public LeadStatus Status { get; set; } = LeadStatus.New;
        public string? Notes { get; set; }

        // Converted customer link — set when the lead becomes a paying member.
        // Cross-navigation to Customer; FK enforced since both are in the tenant DB.
        public int? ConvertedCustomerId { get; set; }
        public DateTime? ConvertedAt { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Customer? ConvertedCustomer { get; set; }
    }

    public enum LeadSource
    {
        WalkIn,
        Referral,
        SocialMedia,
        Website,
        Advertisement,
        Other
    }

    public enum LeadStatus
    {
        New,
        Contacted,
        Interested,
        Converted,
        Lost
    }
}