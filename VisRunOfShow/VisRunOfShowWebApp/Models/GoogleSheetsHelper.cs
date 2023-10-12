using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace IeeeVisRunOfShowWebApp.Models
{
    public class GoogleSheetsHelper
    {
        private readonly string _dataUrl;

        public GoogleSheetsHelper(string dataUrl)
        {
            _dataUrl = dataUrl;
        }

        public Dictionary<string, string?>[] GetEvents()
        {
            return GoogleJsonResponse.LoadSheet(_dataUrl, "Events").RowsToDict();
        }

        public Dictionary<string, string?>[] GetSessions()
        {
            return GoogleJsonResponse.LoadSheet(_dataUrl, "Sessions").RowsToDict();
        }
        public Dictionary<string, string?>[] GetTracks()
        {
            return GoogleJsonResponse.LoadSheet(_dataUrl, "Tracks").RowsToDict();
        }
        public Dictionary<string, string?>[] GetFfVideos()
        {
            return GoogleJsonResponse.LoadSheet(_dataUrl, "FFVideos").RowsToDict();
        }

        public Dictionary<string, string?>[] GetVideos()
        {
            return GoogleJsonResponse.LoadSheet(_dataUrl, "Videos").RowsToDict();

        }

        public Dictionary<string, string?>[] GetPapers()
        {
            return GoogleJsonResponse.LoadSheet(_dataUrl, "PapersDB").RowsToDict();
        }
        public Dictionary<string, string?>[] GetItems()
        {
            var r1 = GoogleJsonResponse.LoadSheet(_dataUrl, "ItemsVISPapers-A").RowsToDict();
            var r2 = GoogleJsonResponse.LoadSheet(_dataUrl, "ItemsVISSpecial").RowsToDict();
            var r3 = GoogleJsonResponse.LoadSheet(_dataUrl, "ItemsEXT").RowsToDict();
            return r1.Concat(r2).Concat(r3).ToArray();
        }

    }


    public class GoogleJsonResponse
    {
        public string version { get; set; }
        public string reqId { get; set; }
        public string status { get; set; }
        public string sig { get; set; }
        public Table table { get; set; }

        public Dictionary<string, string?>[] RowsToDict()
        {
            return table.rows.Length <= 1
                ? Array.Empty<Dictionary<string, string?>>()
                : table.rows.Skip(1)
                    .Select(r => r.c.Select((c, i) => (c, i))
                        .Where(k => !string.IsNullOrWhiteSpace(table.rows[0]?.c?[k.i]?.v?.ToString()))
                        .ToDictionary(k => table.rows[0].c[k.i].v.ToString()!, k => k.c?.v?.ToString())).ToArray();
        }

        public static GoogleJsonResponse LoadSheet(string dataUrl, string sheetName)
        {
            try
            {
                dataUrl += "/gviz/tq?tqx=out:json&sheet=" + sheetName;
                using var client = new WebClient();
                var data = client.DownloadString(dataUrl);
                //Trace.WriteLine(data);
                var idx = data.IndexOf("setResponse(", StringComparison.Ordinal);
                if (idx == -1)
                    throw new Exception("invalid response: " + data);
                data = data[(idx + "setResponse(".Length)..];
                data = data.TrimEnd(')', ';');
                return JsonSerializer.Deserialize<GoogleJsonResponse>(data) ??
                       throw new Exception("could not deserialize google response");
            }
            catch (Exception e)
            {
                Trace.WriteLine("ERROR while loading sheet " + sheetName);
                throw;
            }

        }
    }

    public class Table
    {
        public Col[] cols { get; set; }
        public Row[] rows { get; set; }
        public int parsedNumHeaders { get; set; }
    }

    public class Col
    {
        public string id { get; set; }
        public string label { get; set; }
        public string type { get; set; }
    }

    public class Row
    {
        public C[] c { get; set; }
    }

    public class C
    {
        public object v { get; set; }
    }
}
