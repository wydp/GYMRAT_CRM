using System;

namespace CRM.domain.Entities
{
    public class Inquiry
    {
        public int InquiryId { get; set; }
        public int CustomerId { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public RequestStatus Status { get; set; } = RequestStatus.Open;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Customer? Customer { get; set; }
    }
}