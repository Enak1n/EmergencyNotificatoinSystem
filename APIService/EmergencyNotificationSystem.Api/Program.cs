using EmergencyNotificationSystem.Infrastructure.Extensions;
using EmergencyNotificationSystem.Application.Extensions;
using EmergencyNotificationSystem.Api.Middlewares;
using Serilog;
using MessageBroker.Kafka.Lib;
using EmergencyNotificationSystem.Api.Options;
using EmergencyNotificationSystem.Api.Services;
using EmergencyNotificationSystem.Api.Controllers;

var builder = WebApplication.CreateBuilder(args);

var options = new RequestLoggingOptions();
Action<RequestLoggingOptions> configureOptions = null;
configureOptions?.Invoke(options);
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureService();
builder.Services.AddApplicationService();
builder.Services.Configure<KafkaSettings>(configuration.GetSection(nameof(KafkaSettings)));
builder.Services.AddTransient<NotificationController>();
builder.Services.AddHostedService<SendNotificationConsumer>();

var kafkaHost = configuration.GetSection(nameof(KafkaSettings)).Get<KafkaSettings>();
builder.Services.AddSingleton(kafkaHost.Host);

builder.Services.AddSingleton(serviceProvider =>
{
    var host = serviceProvider.GetRequiredService<string>();
    return new MessageBus(kafkaHost.Host);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<HttpExceptionMiddleware>();
app.UseMiddleware<SerilogMiddleware>(options);

app.MapControllers();

app.Run();
