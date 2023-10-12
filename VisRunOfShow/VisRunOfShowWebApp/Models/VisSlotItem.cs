namespace IeeeVisRunOfShowWebApp.Models;

public class VisSlotItem
{
    //Item ID	Session ID	Slot Title	Slot DateTime Start	Slot DateTime End	Slot Type	Slot Contributors	Slot Contributors Emails	Attendance Mode	Paper UID	File Name	Authors	Abstract	Speaker Affiliation	Speaker Bio	Special Notes
    public string? ItemId { get; set; }
    public string? SessionId { get; set; }
    public string? Title { get; set; }
    public string? Start { get; set; }
    public string? End { get; set; }
    public string? SlotType { get; set; }
    public string? Contributors { get; set; }
    public string? ContributorEmails { get; set; }
    public string? AttendanceMode { get; set; }
    public string? PaperUid { get; set; }
    public string? FileName { get; set; }
    public string? Authors { get; set; }
    public string? Abstract { get; set; }
    public string? SpeakerAffiliation { get; set; }
    public string? SpeakerBio { get; set; }
    public string? SpeakerNotes { get; set; }
        
}