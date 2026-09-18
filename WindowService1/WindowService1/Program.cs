using Microsoft.EntityFrameworkCore;
using Service1;
using WindowService1;
using WindowService1.Data;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<Service1DbContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("Dbconn")
));
builder.Services.AddHostedService<Worker>();

builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "Service1";
});


var host = builder.Build();
host.Run();
