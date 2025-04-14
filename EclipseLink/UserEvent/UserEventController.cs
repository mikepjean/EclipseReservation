using EclipseLink.EventManagement;
using EclipseLink.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EclipseLink.UserEventManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserEventController : ControllerBase
    {
        private readonly IUserEventStore userEventStore;
        private readonly IEventStore eventStore;
        private readonly IUserStore userStore;

        public UserEventController(IUserEventStore userEventStore, IEventStore eventStore, IUserStore userStore)
        {
            this.userEventStore = userEventStore;
            this.eventStore = eventStore;
            this.userStore = userStore;
        }

        [Authorize]
        [HttpPost("RSVP")]
        public IActionResult RSVP([FromQuery] int user_id, [FromQuery] int event_id)
        {
            // Validate the event
            var eventObj = eventStore.Get(event_id);
            if (eventObj == null)
            {
                return NotFound($"Event with ID {event_id} not found.");
            }

            // Validate the user
            var user = userStore.GetUserById(user_id);
            if (user == null)
            {
                return NotFound($"User with ID {user_id} not found.");
            }

            // Check if the user has already RSVP'd
            var userEvent = userEventStore.Get(user_id);
            if (userEvent != null && userEvent.Event_Id == event_id)
            {
                return BadRequest("User has already RSVP'd to this event.");
            }

            // Create or update the UserEvent relationship
            userEvent = new UserEvent(user_id, event_id, "RSVPed", user, eventObj);
            

            userEventStore.RSVP(userEvent);

            // Increment the RSVP count for the event
            eventObj.RSVP_Count++;
            eventStore.Save(eventObj);

            return Ok($"RSVP successful for User ID {user_id} to Event ID {event_id}");
        }
    }
}
