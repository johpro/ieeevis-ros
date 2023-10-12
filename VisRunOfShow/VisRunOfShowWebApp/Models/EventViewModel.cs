namespace IeeeVisRunOfShowWebApp.Models
{
    public class EventViewModel
    {
        public string Event { get; init; }
        public string? EventType { get; set; }
        public string EventPrefix { get; init; }
        public string? EventDescription { get; set; }
        public string? EventUrl { get; set; }
        public string[]? Organizers { get; set; }
        public string[]? OrganizerEmails { get; set; }
        public SessionViewModel[] Sessions { get; set; } = Array.Empty<SessionViewModel>();
    }
}
