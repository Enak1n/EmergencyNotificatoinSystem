using EmergencyNotificationSystem.Domain.Models.NotificationAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyNotificationSystem.Domain.Interfaces.Services.Strategy
{
    public interface INotificationSenderStrategy
    {
        Task Send(Notification notification, SendlerType sendler);
    }
}
