using System;

namespace CRM.domain.Entities
{
    public class PromoCode
    {
        public int PromoCodeId { get; set; }
        public string Code { get; set; } = string.Empty;
        public int PromotionId { get; set; }

        // null MaxUses = unlimited
        public int? MaxUses { get; set; }
        public int CurrentUses { get; set; } = 0;

        // null ExpiresAt = follows the linked promotion's EndDate
        public DateTime? ExpiresAt { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Promotion? Promotion { get; set; }
    }
}