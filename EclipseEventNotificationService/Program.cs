using EclipseEventNotificationsService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Bind NotificationSettings from appsettings.json
        services.Configure<NotificationSettings>(context.Configuration.GetSection("NotificationSettings"));
        services.AddOptions<NotificationSettings>()
                .Bind(context.Configuration.GetSection("NotificationSettings"))
                .Validate(settings => settings.IntervalInMinutes > 0, "Interval must be greater than 0");
        // Add the Worker service
        services.AddHostedService<Worker>();
    })
    .Build();


await host.RunAsync();

