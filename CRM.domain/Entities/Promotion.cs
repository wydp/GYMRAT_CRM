using System;

namespace CRM.domain.Entities
{
    public class Promotion
    {
        public int PromotionId { get; set; }
        public string PromotionCode { get; set; } = string.Empty;
        public string PromotionName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Business or legal justification for this promotion.
        // e.g. "Mandated 20% discount for PWD members under RA 9994."
        public string? Reason { get; set; }

        // Audience targeting — a promotion can apply to multiple groups.
        public bool TargetAllMembers { get; set; } = true;
        public bool TargetPWD { get; set; } = false;
        public bool TargetSenior { get; set; } = false;
        public bool TargetStudent { get; set; } = false;
        public bool TargetCorporate { get; set; } = false;

        public DiscountType DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public enum DiscountType
    {
        Percentage,
        FixedAmount
    }
}