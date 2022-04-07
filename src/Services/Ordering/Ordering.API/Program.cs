using EventBus.Message.Common;
using MassTransit;
using Ordering.API.EventBusConsumer;
using Ordering.API.Extensions;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//For Application layer services
builder.Services.AddApplicationsService();
//For Infrastructure layer services
builder.Services.AddInfrastructureServices(configuration);

// for RabbitMQ -MassTransit
builder.Services.AddMassTransit(configMassTransit =>
{
    configMassTransit.AddConsumer<BasketCheckoutConsumer>();
    configMassTransit.UsingRabbitMq((busRegistrationContext, configRabbitMQ) =>
    {
        configRabbitMQ.Host(configuration["EventBusSettings:HostAddress"]);

        configRabbitMQ.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, configConsumer =>
        {
            configConsumer.ConfigureConsumer<BasketCheckoutConsumer>(busRegistrationContext);
        });
    });
});

//general Config
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<BasketCheckoutConsumer>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MigrateDatabase<OrderContext>((context, services) =>
{
    var logger = services.GetService<ILogger<OrderContextSeed>>();
    OrderContextSeed
        .SeedAsync(context, logger)
        .Wait();
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
