using EclipseLink.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using EclipseLink.EventManagement;
using EclipseLink.UserManagement;

namespace EclipseLink.UserEventManagement
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
        private readonly ApplicationDbContext _context;
        private readonly IUserStore userStore;
        private readonly IEventStore eventStore;

        public UserEventStore(ApplicationDbContext context, IUserStore userStore, IEventStore eventStore)
        {
            _context = context;
            this.userStore = userStore;
            this.eventStore = eventStore;
        }

        public UserEvent Get(int user_event_id)
        {
            return _context.UserEvents.Find(user_event_id);
        }

        public UserEvent DiscoverUserEvents(int user_Id, DateTime? date, string location)
        {
            return _context.UserEvents
                .Where(ue => ue.User_Id == user_Id)
                .Select(ue => new
                {
                    UserEvent = ue,
                    Event = _context.Events.FirstOrDefault(e =>
                        e.Event_Id == ue.Event_Id &&
                        (!date.HasValue || e.Event_Date.Date == date.Value.Date) &&
                        (string.IsNullOrEmpty(location) || e.Visibility_Locations.Any(v => v.Location == location)))
                })
                .Where(x => x.Event != null)
                .Select(x => x.UserEvent)
                .FirstOrDefault();
        }

        public void RSVP(UserEvent user_event)
        {
            // Ensure User and Event are valid
            if (userStore == null)
            {
                throw new ArgumentNullException(nameof(userStore), "User store cannot be null.");
            }

            var user = userStore.GetUserById(user_event.User_Id);
            if (user == null)
            {
                throw new ArgumentException($"User with ID {user_event.User_Id} not found.");
            }

            var eventObj = eventStore.Get(user_event.Event_Id);
            if (eventObj == null)
            {
                throw new ArgumentException($"Event with ID {user_event.Event_Id} not found.");
            }

            // Check if the user has already RSVP'd to the event
            var existingUserEvent = _context.UserEvents
                .FirstOrDefault(ue => ue.User_Id == user_event.User_Id && ue.Event_Id == user_event.Event_Id);
            if (existingUserEvent != null)
            {
                throw new InvalidOperationException("User has already RSVP'd to this event.");
            }

            // Update the UserEvent relationship
            user_event.User = user;
            user_event.Event = eventObj;
            user_event.Status = "RSVPed";

            // Save the UserEvent to the database
            _context.UserEvents.Add(user_event);
            _context.SaveChanges();

            // Increment the RSVP count for the event
            eventObj.RSVP_Count++;
            eventStore.Save(eventObj);
        }

        public IEnumerable<UserEvent> GetAllUserEvents()
        {
            // Return all user events
            return _context.UserEvents.ToList();
        }
    }
}
