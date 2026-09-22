using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.domain.Entities
{
    public class Promotion
    {
        public int PromotionId { get; set; }
        public string PromotionCode { get; set; } = string.Empty;
        public string PromotionName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
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
