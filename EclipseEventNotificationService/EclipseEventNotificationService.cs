using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EclipseLink.Services
{
    public class EclipseEventNotificationService : BackgroundService
    {
        private readonly ILogger<EclipseEventNotificationService> _logger;
        private readonly double _interval;
        private Timer? _timer;

        public EclipseEventNotificationService(ILogger<EclipseEventNotificationService> logger)
        {
            _logger = logger;

            // Read interval from appsettings.json
            _interval = double.TryParse(
                Environment.GetEnvironmentVariable("NotificationIntervalInMinutes"),
                out var interval) ? interval * 60 * 1000 : 60000; // Default to 1 minute
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("EclipseEventNotificationService is starting.");

            _timer = new Timer(SendNotifications, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(_interval));

            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("EclipseEventNotificationService is stopping.");

            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void SendNotifications(object? state)
        {
            _logger.LogInformation("Sending notifications to users about RSVP'd events...");
            // TODO: Implement notification logic here
        }
    }
}
