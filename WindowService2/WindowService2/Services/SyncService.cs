using Microsoft.EntityFrameworkCore;
using WindowService2.Data;

namespace Service2.Services
{
    public class SyncService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<SyncService> _logger;

        public SyncService(
            IServiceScopeFactory scopeFactory,
            ILogger<SyncService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        public async Task SyncDataAsync(
            CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();

            var sourceDb = scope.ServiceProvider
                .GetRequiredService<Service1DbContext>();

            var targetDb = scope.ServiceProvider
                .GetRequiredService<Service2DbContext>();

            // 1. Get last successful sync time
            var syncControl = await targetDb.SyncControls
                .FirstAsync(x => x.Id == 1, cancellationToken);

            DateTime? lastSyncTime = syncControl.LastSyncTime;

            _logger.LogInformation(
                "Last Sync Time: {LastSyncTime}",
                lastSyncTime);

            // 2. Get new records from Service 1 DB
            var newEmployees = await sourceDb.Employees
                .Where(x =>
                    lastSyncTime == null ||
                    x.CreatedAt > lastSyncTime)
                .OrderBy(x => x.CreatedAt)
                .ToListAsync(cancellationToken);

            _logger.LogInformation(
                "Found {Count} new records.",
                newEmployees.Count);

            if (newEmployees.Count == 0)
            {
                _logger.LogInformation(
                    "No new records found.");

                return;
            }

            // 3. Insert records into Service 2 DB
            foreach (var employee in newEmployees)
            {
                bool alreadyExists = await targetDb.Employees
                    .AnyAsync(
                        x => x.Id == employee.Id,
                        cancellationToken);

                if (!alreadyExists)
                {
                    targetDb.Employees.Add(new Models.Employee
                    {
                        Id = employee.Id,
                        Name = employee.Name,
                        Salary = employee.Salary,
                        Department = employee.Department,
                        CreatedAt = employee.CreatedAt
                    });
                }
            }

            // 4. Save employee records
            await targetDb.SaveChangesAsync(cancellationToken);

            // 5. Find latest synchronized record
            var latestCreatedAt = newEmployees
                .Max(x => x.CreatedAt);

            // 6. Update LastSyncTime
            syncControl.LastSyncTime = latestCreatedAt;

            await targetDb.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                "Successfully synchronized {Count} records.",
                newEmployees.Count);

            _logger.LogInformation(
                "New Last Sync Time: {LastSyncTime}",
                latestCreatedAt);
        }
    }
}