using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyNotificationSystem.Infrastructure.Entities
{
    public class NotificationEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string Message { get; set; }
        public NotificationType Type { get; set; }
    }
}
