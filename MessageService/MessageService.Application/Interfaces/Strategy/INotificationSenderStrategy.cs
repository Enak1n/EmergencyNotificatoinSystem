namespace MessageService.Application.Interfaces.Strategy
{
    public interface INotificationSenderStrategy
    {
        Task Send(string notification, SendlerType sendler);
    }
}
