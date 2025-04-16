using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EclipseLink.EventManagement
{
    public class EventCreationRequest()
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime Event_Date { get; set; }
        public IEnumerable<VisibilityLocation>? Visibility_Locations { get; set; }
        public int RSVP_Count { get; set; }

    }

}
