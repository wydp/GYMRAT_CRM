using System;

namespace CRM.domain.Entities
{
    public class Campaign
    {
        public int CampaignId { get; set; }
        public string CampaignCode { get; set; } = string.Empty;
        public string CampaignName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Business justification for this campaign.
        // e.g. "Retain PWD members whose memberships expire next month."
        public string? Reason { get; set; }

        // Audience targeting — a campaign can target multiple groups.
        public bool TargetAllMembers { get; set; } = true;
        public bool TargetPWD { get; set; } = false;
        public bool TargetSenior { get; set; } = false;
        public bool TargetStudent { get; set; } = false;
        public bool TargetCorporate { get; set; } = false;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CampaignStatus Status { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public enum CampaignStatus
    {
        Planned,
        Active,
        Completed,
        Cancelled
    }
}