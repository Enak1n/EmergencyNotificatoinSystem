using EmergencyNotificationSystem.Infrastructure.Extensions;
using EmergencyNotificationSystem.Application.Extensions;
using EmergencyNotificationSystem.Api.Middlewares;
using Serilog;
using MessageBroker.Kafka.Lib;

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

var kafkaHost = "localhost:9092"; // or read it from configuration
builder.Services.AddSingleton(kafkaHost);

builder.Services.AddSingleton<MessageBus>(serviceProvider =>
{
    var host = serviceProvider.GetRequiredService<string>();
    return new MessageBus(host);
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
