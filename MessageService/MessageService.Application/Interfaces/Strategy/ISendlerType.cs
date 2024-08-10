namespace EmergencyNotificationSystem.Domain.Interfaces.Services.Strategy
{
    public interface ISendlerType
    {
        SendlerType Sendler { get; }

        Task Send(string notification);
    }
}
