using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class SanitizeService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public SanitizeService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using(var scope = _serviceProvider.CreateScope())
                {
                    var programmingService = scope.ServiceProvider.GetRequiredService<IProgrammingService>();
                    await programmingService.Sanitize();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"SanitizeService encountered an error: {ex.Message}");
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}
