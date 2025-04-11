using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EclipseLink.CommunityInteraction
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityInteractionController : ControllerBase
    {
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            // Simulate saving the community interaction
            // In a real application, you would save this to a database or other storage
            return Ok($"Community interaction saved: {value}");
        }
    }
}
