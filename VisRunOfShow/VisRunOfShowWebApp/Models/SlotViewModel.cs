namespace IeeeVisRunOfShowWebApp.Models
{
    public class SlotViewModel
    {
        public string ItemId { get; init; }
        public SessionViewModel Session { get; init; }
        public string Title { get; init; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string SlotType { get; init; }
        public string[]? Contributors { get; set; }
        public string[]? ContributorEmails { get; set; }
        public string? AttendanceMode { get; set; }
        public AttendanceModeEnum ParsedAttendanceMode { get; set; }
        public string? PaperUid { get; set; }
        public string? FileName { get; set; }
        public string[]? Authors { get; set; }
        public string? Abstract { get; set; }
        public string? SpeakerAffiliation { get; set; }
        public string? SpeakerBio { get; set; }
        public string? SpecialNotes { get; set; }
        public string? Hints { get; set; }
        public string FastForwardLink { get; set; }
        public string VideoLink { get; set; }
        public bool RecordingDeclined { get; set; }
        public string? IeeeUrl { get; set; }
        public string? DoiUrl { get; set; }
        public string? PdfUrl { get; set; }
        public bool HasPdf { get; set; }
        public bool HasVideo { get; set; }

        public string GetSlotTypeCss()
        {
            return SlotType switch
            {
                "In Person Presentation" => "person-pres",
                "In Person Q+A" => "person-qa",
                "In Person Other" => "person-other",
                "Virtual Presentation (pre-recorded)" => "virtual-pres-rec",
                "Virtual Presentation (live)" => "virtual-pres",
                "Virtual Q+A" => "virtual-qa",
                "In Person Panel" => "onsite-panel",
                "Mixed Panel (virtual/in person)" => "mixed-panel",
                _ => "unknown"

            };
        }
    }

    public enum AttendanceModeEnum
    {
        InPerson, Virtual, Mixed,
        Unknown
    }
}
