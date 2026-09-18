using Service2.Services;

namespace Service2;

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

            var syncService = scope.ServiceProvider
                .GetRequiredService<SyncService>();

            await syncService.SyncDataAsync(stoppingToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error occurred during synchronization.");
        }
    }
}