using FCG.Catalog.Worker;
using FCG.Catalog.Worker.Extensions;

var builder = Host.CreateApplicationBuilder(args);
builder.Services
    .ConfigureSettings(builder.Configuration)
    .ConfigureWorker()
    .ConfigureApplication()
    .ConfigureDomain()
    .ConfigureInfrastructure(builder.Configuration);

var host = builder.Build();
host.Run();
