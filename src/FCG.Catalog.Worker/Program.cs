using FCG.Catalog.Worker;
using FCG.Catalog.Worker.Extensions;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services
    .ConfigureSettings(builder.Configuration)
    .ConfigureWorker()
    .ConfigureApplication()
    .ConfigureDomain()
    .ConfigureInfrastructure(builder.Configuration);

IHost host = builder.Build();
host.ApplyMigrations();
host.Run();
