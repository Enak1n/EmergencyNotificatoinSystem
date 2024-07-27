using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyNotificationSystem.Infrastructure.Entities
{
    public class CompanyEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public List<UserEntity> Users { get; set; } = [];
    }
}
