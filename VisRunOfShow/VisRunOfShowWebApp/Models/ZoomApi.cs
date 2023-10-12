using System.Net;

namespace IeeeVisRunOfShowWebApp.Models
{
    public class ZoomApi
    {
        private readonly string _jwtToken;

        public ZoomApi(string jwtToken)
        {
            _jwtToken = jwtToken;
        }

        public async Task<ZoomMeetingInfo?> GetMeetingInfo(string id)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_jwtToken}");
            var res = await client.GetFromJsonAsync<ZoomMeetingInfo>($"https://api.zoom.us/v2/meetings/{id}");
            return res;
        }
    }
}
