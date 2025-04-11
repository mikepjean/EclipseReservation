namespace EclipseLink.UserEvent
{
    public class UserEvent
    {
        // Unique identifier for the UserEvent relationship
        public int UserEvent_Id { get; set; }

        // Foreign keys for the User and Event
        public int User_Id { get; set; }
        public int Event_Id { get; set; }

        // Status of the relationship (e.g., "RSVPed")
        public string Status { get; set; }

        // Navigation properties for related User and Event
        public EclipseLink.User.User User { get; set; }
        public EclipseLink.Event.Event Event { get; set; }

        // Constructor to initialize the UserEvent with required fields
        public UserEvent(int user_Id, int event_Id, string status, EclipseLink.User.User user, EclipseLink.Event.Event _event)
        {
            User_Id = user_Id;
            Event_Id = event_Id;
            Status = status;
            User = user ?? throw new ArgumentNullException(nameof(user), "User cannot be null.");
            Event = _event ?? throw new ArgumentNullException(nameof(_event), "Event cannot be null.");
        }
    }
}


