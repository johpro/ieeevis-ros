using System.Text.Json;

namespace IeeeVisRunOfShowWebApp.Models
{
    public class SheetsCache
    {
        private static readonly string SheetsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache", "sheets.json");
        public Dictionary<string, string?>[] Events { get; set; }
        public Dictionary<string, string?>[] Sessions { get; set; }
        public Dictionary<string, string?>[] Items { get; set; }
        public Dictionary<string, string?>[] Tracks { get; set; }
        public DateTime LastCheckTimeUtc { get; set; }
        public Dictionary<string, string?>[]? FfVideos { get; set; }
        public Dictionary<string, string?>[]? Videos { get; set; }
        public Dictionary<string, string?>[]? Papers { get; set; }


        public void Save()
        {
            Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache"));
            File.WriteAllText(SheetsPath, JsonSerializer.Serialize(this));
        }

        public static SheetsCache LoadOrCreate()
        {
            return (File.Exists(SheetsPath)
                ? JsonSerializer.Deserialize<SheetsCache>(File.ReadAllText(SheetsPath))
                : new SheetsCache()) ?? throw new Exception("could not deserialize cache");
        }


    }
}
