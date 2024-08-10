using EmergencyNotificationSystem.Application.Senders;
using EmergencyNotificationSystem.Domain.Interfaces.Services.Strategy;
using MessageBroker.Kafka.Lib;
using MessageService.API.Controllers;
using MessageService.API.Options;
using MessageService.API.Services;
using MessageService.Application.Interfaces.Strategy;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<INotificationSenderStrategy, NotificationSenderStrategy>();
builder.Services.AddSingleton<ISendlerType, ConsoleNotificationSender>();
builder.Services.AddSingleton<MessageController>();
builder.Services.AddHostedService<ConsumerService>();

builder.Services.Configure<KafkaSettings>(configuration.GetSection(nameof(KafkaSettings)));

var kafkaHost = configuration.GetSection(nameof(KafkaSettings)).Get<KafkaSettings>();

builder.Services.AddSingleton(kafkaHost.Host);

builder.Services.AddSingleton<MessageBus>(serviceProvider =>
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

app.MapControllers();

app.Run();
