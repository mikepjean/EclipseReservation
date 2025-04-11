using System.Collections.Generic;

namespace EclipseLink.Event
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
        private static readonly Dictionary<int, Event> Database = new Dictionary<int, Event>();

        public Event Get(int event_id) =>
            Database.ContainsKey(event_id) ? Database[event_id] : new Event(event_id);

        public Event? DiscoverEvents(DateTime? date, string location)
        {
            if (date == null || string.IsNullOrEmpty(location))
            {
                return null; // Handle null inputs gracefully
            }

            return Database.Values.FirstOrDefault(e =>
                e.Event_Date.Date == date.Value.Date &&
                e.Visibility_Locations != null && // Ensure Visibility_Locations is not null
                e.Visibility_Locations.Any(v => v.Location == location));
        }

        public void Save(Event _event) =>
            Database[_event.Event_Id] = _event;
        public void Delete(int event_id)
        {
            if (Database.ContainsKey(event_id))
            {
                Database.Remove(event_id);
            }
        }
    }

}
