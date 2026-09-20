namespace CRM.domain.Entities
{
    // Shared by Inquiry and Feedback — a single, simple support-ticket lifecycle.
    public enum RequestStatus
    {
        Open,
        InProgress,
        Resolved,
    }

    // Lives on Feedback — distinguishes ordinary feedback from a complaint
    // without needing a separate entity/table.
    public enum FeedbackType
    {
        Feedback,
        Complaint,
    }
}