using EclipseLink.EventManagement;
using EclipseLink.UserManagement;
using EclipseLink.UserEventManagement;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EclipseEventNotificationsService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IEventStore _eventStore;
        private readonly IUserStore _userStore;
        private readonly IUserEventStore _userEventStore;

        public Worker(ILogger<Worker> logger, IEventStore eventStore, IUserStore userStore, IUserEventStore userEventStore)
        {
            _logger = logger;
            _eventStore = eventStore;
            _userStore = userStore;
            _userEventStore = userEventStore;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Send notifications
                    SendWeeklyNotifications();

                    // Wait for a week (7 days)
                    await Task.Delay(TimeSpan.FromDays(7), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while sending notifications.");
                }
            }
        }

        private void SendWeeklyNotifications()
        {
            _logger.LogInformation("Sending weekly notifications...");

            // Get all users who RSVP'd to events
            var userEvents = _userEventStore.GetAllUserEvents();

            foreach (var userEvent in userEvents)
            {
                // Get user and event details
                var user = _userStore.GetUserById(userEvent.User_Id);
                var eventObj = _eventStore.Get(userEvent.Event_Id);

                if (user != null && eventObj != null)
                {
                    // Send notification (e.g., log it or integrate with an email/SMS service)
                    _logger.LogInformation($"Notifying {user.FirstName} {user.LastName} ({user.Username}) about event '{eventObj.Name}' on {eventObj.Event_Date}.");
                }
            }
        }
    }
}

