using Microsoft.EntityFrameworkCore;

using WindowService1.Data;

namespace Service1;

public class Worker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<Worker> _logger;

    public Worker(
        IServiceScopeFactory scopeFactory,
        ILogger<Worker> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        try
        {
            using var scope = _scopeFactory.CreateScope();

            var dbContext =
                scope.ServiceProvider
                    .GetRequiredService<Service1DbContext>();

            bool canConnect =
                await dbContext.Database.CanConnectAsync(stoppingToken);

            if (canConnect)
            {
                _logger.LogInformation(
                    "Successfully connected to Service1DB.");
            }
            else
            {
                _logger.LogError(
                    "Could not connect to Service1DB.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Database connection failed.");
        }
    }
}