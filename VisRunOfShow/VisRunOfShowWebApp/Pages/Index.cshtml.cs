using IeeeVisRunOfShowWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IeeeVisRunOfShowWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public EventViewModel[] Events { get; set; } = Array.Empty<EventViewModel>();
        public TrackViewModel[] Tracks { get; set; } = Array.Empty<TrackViewModel>();
        public string? ErrorMessage { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool? Refresh { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Key { get; set; }

        public static string AdminKey { get;}
        public static string PrivateKey { get;}

        static IndexModel()
        {
            var settings = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                .Build().GetSection("CustomSettings");
            AdminKey = settings["AdminKey"];
            PrivateKey = settings["PrivateKey"];
        }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(AdminKey) && AdminKey != Key)
                {
                    ErrorMessage = "Access denied";
                    return;
                }
                (Events, Tracks) = DataSourceModel.Default.GetData(Refresh == true);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"browsing schedule failed");
                Events = Array.Empty<EventViewModel>();
                Tracks = Array.Empty<TrackViewModel>();
                ErrorMessage = e.Message;
            }
        }
    }
}