using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EclipseLink.EventManagement
{
    [Route("/EclipseEvent")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventStore eventStore;
        public EventController(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }

        // Endpoint to get an Eclipse Event
        [Authorize]
        [HttpGet("{event_id:int}")]
        public IActionResult Get(int event_id)
        {
            var result = this.eventStore.Get(event_id);
            if (result == null)
            {
                return NotFound($"Event with ID {event_id} not found.");
            }
            return Ok(result);
        }

        // Endpoint to add an Eclipse Event
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] Event newEvent)
        {
            if (newEvent == null)
            {
                return BadRequest("Event data is required.");
            }

            try
            {
                eventStore.Save(newEvent);
                return Ok("Event created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error saving event: {ex.Message}");
            }
        }

        // Endpoint to update an Eclipse Event by id
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Event updatedEvent)
        {
            if (updatedEvent == null || updatedEvent.Event_Id != id)
            {
                return BadRequest("Invalid event data or mismatched event ID.");
            }

            // Check if the event exists in the store
            var existingEvent = eventStore.Get(id);
            if (existingEvent == null || existingEvent.Event_Id != id)
            {
                return NotFound($"Event with ID {id} not found.");
            }

            // Update the event details
            existingEvent.Name = updatedEvent.Name;
            existingEvent.Description = updatedEvent.Description;
            existingEvent.Event_Date = updatedEvent.Event_Date;
            existingEvent.Visibility_Locations = updatedEvent.Visibility_Locations;

            // Save the updated event back to the store
            eventStore.Save(existingEvent);

            return Ok($"Event with ID {id} updated successfully.");
        }

        // Endpoint to delete an Eclipse Event by Event_Id
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Check if the event exists in the store
            var existingEvent = eventStore.Get(id);
            if (existingEvent == null || existingEvent.Event_Id != id)
            {
                return NotFound($"Event with ID {id} not found.");
            }

            // Remove the event from the store
            eventStore.Delete(id);

            return Ok($"Event with ID {id} deleted successfully.");
        }


        // Endpoint to discover events based on date and location
        [Authorize]
        [HttpGet("EventDiscovery")]
        public IActionResult DiscoverEvents([FromQuery] DateTime? date, [FromQuery] string location)
        {
            // Fetch all events from the EventStore
            var events = eventStore.DiscoverEvents(date, location);
            return Ok(events);
        }
    }
}
