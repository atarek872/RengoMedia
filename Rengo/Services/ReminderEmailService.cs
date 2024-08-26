using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Rengo.Data;
using Rengo.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Rengo.Services
{
    public class ReminderEmailService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<ReminderEmailService> _logger;

        public ReminderEmailService(IServiceScopeFactory serviceScopeFactory, ILogger<ReminderEmailService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                    // Perform your background task here, e.g., sending reminder emails
                    _logger.LogInformation("Reminder Email Service is running.");

                     var reminders = await dbContext.Reminders.ToListAsync();
                    foreach (var reminder in reminders)
                    {
                        await emailService.SendEmailAsync("gmail@gmail.com", reminder.Title, "Reminder content with Date : "+ reminder.DateTime );
                    }

                    // Pause for a while before running the task again
                    await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
                }
            }
        }
    }
}
