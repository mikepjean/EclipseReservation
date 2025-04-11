using System;
using System.Collections.Generic;
using System.Linq;
using EclipseLink.Event;
using EclipseLink.User;

namespace EclipseLink.UserEvent
{
    public interface IUserEventStore
    {
        UserEvent Get(int user_event_id);

        UserEvent DiscoverUserEvents(int user_Id, DateTime? date, string location);
        void RSVP(UserEvent user_event);

        IEnumerable<UserEvent> GetAllUserEvents();
    }

    public class UserEventStore : IUserEventStore
    {
        private static readonly Dictionary<int, EclipseLink.UserEvent.UserEvent> Database = new Dictionary<int, UserEvent>();
        private readonly IUserStore userStore;
        private readonly IEventStore eventStore;

        public UserEventStore(IUserStore userStore, IEventStore eventStore)
        {
            this.userStore = userStore;
            this.eventStore = eventStore;
        }

        public UserEvent Get(int user_event_id) =>
            Database.ContainsKey(user_event_id) ? Database[user_event_id] : null;

        public UserEvent DiscoverUserEvents(int user_Id, DateTime? date, string location) =>
            Database.Values.FirstOrDefault(e =>
                e.User_Id == user_Id &&
                (!date.HasValue || e.Event.Event_Date.Date == date.Value.Date) &&
                (string.IsNullOrEmpty(location) || e.Event.Visibility_Locations.Any(v => v.LocationName == location)));

        public void RSVP(UserEvent user_event)
        {
            // Ensure User and Event are valid
            if(userStore == null)
            {
                throw new ArgumentNullException(nameof(userStore), "User store cannot be null.");
            }
            int _user_id = user_event.User_Id;
            var user = userStore.GetUserById(_user_id);
            if (user == null)
            {
                throw new ArgumentException($"User with ID {user_event.User_Id} not found.");
            }

            var eventObj = eventStore.Get(user_event.Event_Id);
            if (eventObj == null)
            {
                throw new ArgumentException($"Event with ID {user_event.Event_Id} not found.");
            }

            // Update the UserEvent relationship
            user_event.User = user;
            user_event.Event = eventObj;
            user_event.Status = "RSVPed";

            // Save the UserEvent
            Database[user_event.UserEvent_Id] = user_event;

            // Increment the RSVP count for the event
            eventObj.RSVP_Count++;
            eventStore.Save(eventObj);
        }

        public IEnumerable<UserEvent> GetAllUserEvents()
        {
            // Return all user events
            return Database.Values.ToList();
        }
    }
}
