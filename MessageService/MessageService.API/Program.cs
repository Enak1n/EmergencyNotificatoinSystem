using EmergencyNotificationSystem.Application.Senders;
using EmergencyNotificationSystem.Domain.Interfaces.Services.Strategy;
using MessageBroker.Kafka.Lib;
using MessageService.API.Controllers;
using MessageService.API.Services;
using MessageService.Application.Interfaces.Strategy;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<INotificationSenderStrategy, NotificationSenderStrategy>();
builder.Services.AddSingleton<ISendlerType, ConsoleNotificationSender>();
builder.Services.AddSingleton<MessageController>();
builder.Services.AddHostedService<ConsumerService>();

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

app.MapControllers();

app.Run();
