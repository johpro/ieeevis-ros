using IeeeVisRunOfShowWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using VisRunOfShowWebApp.Models;

namespace IeeeVisRunOfShowWebApp.Pages
{
    public class ScheduleModel : PageModel
    {
        private readonly ILogger<ScheduleModel> _logger;

        public ScheduleModel(ILogger<ScheduleModel> logger)
        {
            _logger = logger;
        }
        [BindProperty(SupportsGet = true)]
        public string? Key { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? TrackId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? SessionId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Date { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? EventPrefix { get; set; }

        public string? ErrorMessage { get; set; }
        public string? Message { get; set; }
        public DateTime CurrentDate { get; set; }
        public bool HasElevatedRights { get; set; }

        public SessionViewModel[] Sessions { get; set; } = Array.Empty<SessionViewModel>();

        public void OnGet()
        {
            try
            {
                var hasAuth = !string.IsNullOrWhiteSpace(IndexModel.AdminKey);
                if (string.IsNullOrWhiteSpace(SessionId))
                {
                    if (hasAuth && !AuthHelper.SafeCompareEquality(IndexModel.AdminKey, Key))
                    {
                        ErrorMessage = "Access denied";
                        return;
                    }

                    HasElevatedRights = true;

                    if (string.IsNullOrWhiteSpace(Date) ||
                        (string.IsNullOrWhiteSpace(TrackId) && string.IsNullOrWhiteSpace(EventPrefix)))
                    {
                        Message = "Invalid request";
                        return;
                    }
                }

                var (events, tracks) = DataSourceModel.Default.GetData();
                if (!string.IsNullOrWhiteSpace(SessionId))
                {
                    var session = events.SelectMany(ev => ev.Sessions).FirstOrDefault(s => s.SessionID == SessionId);
                    if (session == null)
                    {
                        Message = "Invalid request";
                        return;
                    }
                    if (hasAuth && !AuthHelper.SafeCompareEquality(IndexModel.AdminKey, Key))
                    {
                        if (!AuthHelper.SafeCompareEquality(session.SessionKey, Key))
                        {
                            ErrorMessage = "Access denied";
                            return;
                        }
                    }
                    else
                    {
                        HasElevatedRights = true;
                    }

                    CurrentDate = session.DateTimeStart!.Value.Date;
                    Sessions = new[] { session };
                    return;
                }

                var eventsF = events.AsEnumerable();
                if (!string.IsNullOrWhiteSpace(EventPrefix))
                    eventsF = eventsF.Where(e => e.EventPrefix == EventPrefix);
                var date = DateTime.ParseExact(Date, "yy-MM-dd", null);
                CurrentDate = date;
                Sessions = eventsF.SelectMany(ev => ev.Sessions.Where(s =>
                    (string.IsNullOrWhiteSpace(TrackId) || s.Track.Track == TrackId)
                    && s.DateTimeStart!.Value.Date == date)).OrderBy(s => s.DateTimeStart).ToArray();

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"retrieving sessions for schedule failed");
                Sessions = Array.Empty<SessionViewModel>();
                ErrorMessage = e.Message;
            }
        }
    }
}
