using Play.Catalog.Service.Model;
using Play.Common.MongoDB;
using Play.Common.Settings;
using MassTransit;


using Play.Catalog.Service.Settings;
// using MassTransit.KebabCaseEndpointNameFormatter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// at the runtime the compiler verify configuration from appsettings.json file
// and get the props of ServiceSettings (serviceName)
ServiceSettings serviceSettings = builder.Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>()!;

builder.Services.AddMongo().AddMongoRepository<Item>("items");
builder.Services.AddMassTransit(x => {
    x.UsingRabbitMq((context, configurator) => {
        var rabbitMQSettings = builder.Configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();
        configurator!.Host(rabbitMQSettings!.Host);
        configurator!.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(serviceSettings.ServiceName, false));

    });
});
// builder.Services.AddMassTransitHostedService();
builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});
// store document id in mongo as string
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();
