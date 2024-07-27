using EmergencyNotificationSystem.Domain.Models.NotificationAggregate;
using FluentAssertions;

namespace EmergencyNotificationSystem.UnitTests.Domain.NotificationAggregate
{
    [TestFixture]
    public class NotificationTests
    {
        [Test]
        public async Task CreateNotification_ReturnNewNotification()
        {
            // Act
            var notification = Notification.Create(new Guid(), DateTime.UtcNow, "test", NotificationType.Critical);

            // Assert
            notification.Should().NotBeNull();
        }

        [Test]
        public async Task CreateNotification_EmptyMessage_ShouldThrowArgumentNullException()
        {
            // Act
            var action = () => Notification.Create(new Guid(), DateTime.UtcNow, "", NotificationType.Critical);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public async Task CreateNotification_MessageIsNull_ShouldThrowArgumentNullException()
        {
            // Act
            var action = () => Notification.Create(new Guid(), DateTime.UtcNow, null, NotificationType.Critical);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public async Task CreateNotification_MessageIsOnlytWithSpaces_ShouldThrowArgumentNullException()
        {
            // Act
            var action = () => Notification.Create(new Guid(), DateTime.UtcNow, "   ", NotificationType.Critical);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }
    }
}
