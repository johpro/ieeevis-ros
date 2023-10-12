namespace IeeeVisRunOfShowWebApp.Models
{
    public class SessionViewModel
    {
        public string SessionID { get; init; }
        public EventViewModel Event { get; init; }
        public SlotViewModel[] Slots { get; set; } = Array.Empty<SlotViewModel>();
        public string SessionTitle { get; init; }
        public DateTime? DateTimeStart { get; set; }
        public DateTime? DateTimeEnd { get; set; }
        public TrackViewModel Track { get; set; }
        public string? LivestreamID { get; set; }
        public string? SessionImage { get; set; }
        public string[]? SessionChairs { get; set; }
        public string[]? SessionChairsEmails { get; set; }
        public string? SessionYouTubeURL { get; set; }
        public string? SessionFFPlaylistURL { get; set; }
        public string? SessionFFURL { get; set; }
        public string? SlidoURL { get; set; }
        public string? DiscordChannel { get; set; }
        public string? DiscordChannelID { get; set; }
        public string? DiscordURL { get; set; }
        public string? ZoomMeetingID { get; set; }
        public string? ZoomPassword { get; set; }
        public string? ZoomURL { get; set; }
        public string SessionKey { get; set; }
    }
}
