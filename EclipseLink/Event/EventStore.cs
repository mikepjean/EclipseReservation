using EclipseLink.Persistence;
using EclipseLink.EventManagement;
using System.Linq;

namespace EclipseLink.EventManagement
{
    public interface IEventStore
    {
        Event Get(int event_id);
        Event? DiscoverEvents(DateTime? date, string location);
        void Save(Event _event);
        void Delete(int event_id);
    }
    public class EventStore : IEventStore
    {
        private readonly ApplicationDbContext _context;

        public EventStore(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves an Event by its unique ID.
        /// </summary>
        /// <param name="event_id">The unique ID of the Event.</param>
        /// <returns>The Event object if found, otherwise null.</returns>
        public Event Get(int event_id)
        {
            return _context.Events.Find(event_id);
        }

        /// <summary>
        /// Discovers Events based on date and location.
        /// </summary>
        /// <param name="date">The date of the event.</param>
        /// <param name="location">The location of the event.</param>
        /// <returns>The matching Event object if found, otherwise null.</returns>
        public Event? DiscoverEvents(DateTime? date, string location)
        {
            return _context.Events
                .FirstOrDefault(e =>
                    (!date.HasValue || e.Event_Date.Date == date.Value.Date) &&
                    (string.IsNullOrEmpty(location) || e.Visibility_Locations.Any(v => v.Location == location)));
        }

        /// <summary>
        /// Saves or updates an Event in the store.
        /// </summary>
        /// <param name="_event">The Event object to save.</param>
        public void Save(Event _event)
        {
            if (_context.Events.Any(e => e.Event_Id == _event.Event_Id))
            {
                _context.Events.Update(_event);
            }
            else
            {
                _context.Events.Add(_event);
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Deletes an Event by its unique ID.
        /// </summary>
        /// <param name="event_id">The unique ID of the Event to delete.</param>
        public void Delete(int event_id)
        {
            var eventObj = _context.Events.Find(event_id);
            if (eventObj != null)
            {
                _context.Events.Remove(eventObj);
                _context.SaveChanges();
            }
        }
    }
}

