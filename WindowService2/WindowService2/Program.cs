using Microsoft.EntityFrameworkCore;
using Service2.Services;
using WindowService2;
using WindowService2.Data;

var builder = Host.CreateApplicationBuilder(args);


builder.Services.AddDbContext<Service1DbContext>(option =>

    option.UseSqlServer(builder.Configuration.GetConnectionString("Service1Connection"))
);

builder.Services.AddDbContext<Service2DbContext>(option =>

option.UseSqlServer(builder.Configuration.GetConnectionString("Service2Connection"))
);




builder.Services.AddHostedService<Worker>();


builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "Service2";
});

builder.Services.AddScoped<SyncService>();

var host = builder.Build();
host.Run();
