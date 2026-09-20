namespace CRM.domain.Entities
{
    // Body shape for PUT /inquiries/{id}/status and /feedbacks/{id}/status
    public class UpdateStatusRequest
    {
        public RequestStatus Status { get; set; }
    }
}