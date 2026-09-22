using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.domain.Entities
{
    public class Campaign
    {
        public int CampaignId { get; set; }
        public string CampaignCode { get; set; } = string.Empty;
        public string CampaignName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
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