using System.Collections.Generic;
using System.Linq;

namespace EclipseLink.Event
{
    public class Event(int event_id)
    {
        public int Event_Id { get; set; } = event_id;

        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime Event_Date { get; set; }
        public IEnumerable<VisibilityLocation>? Visibility_Locations { get; set; }
        public int RSVP_Count { get; set; }

    }

    public record EventItem(int Event_Id, string Name, string Description, DateTime Event_Date, IEnumerable<VisibilityLocation> Visibility_Locations, int RSVP_Count)
    {
        public virtual bool Equals(EventItem? obj) =>
            obj != null && this.Event_Id.Equals(obj.Event_Id);
        
        public override int GetHashCode() =>
            this.Event_Id.GetHashCode();
    }
}
