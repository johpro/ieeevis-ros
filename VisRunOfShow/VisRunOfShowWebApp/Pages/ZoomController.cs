using IeeeVisRunOfShowWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IeeeVisRunOfShowWebApp.Pages
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZoomController : ControllerBase
    {
        [HttpGet()]
        public async Task<ActionResult<ZoomMeetingInfo?>> MeetingInfo(string? key, long id)
        {
            var data = DataSourceModel.Default;
            if (!string.IsNullOrWhiteSpace(data.AdminKey) && data.AdminKey != key)
            {
                return Unauthorized();
            }

            if (!data.HasZoomApi)
                return NotFound(new{ message = "Zoom API not available"});
            var info = await data.GetMeetingInfo(id);
            
            if (info == null)
                return NotFound();
            return info;
        }
    }
}
