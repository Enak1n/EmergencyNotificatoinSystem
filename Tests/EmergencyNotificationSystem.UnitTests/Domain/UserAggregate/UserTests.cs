using EmergencyNotificationSystem.Domain.Models.UserAggregate;
using FluentAssertions;

namespace EmergencyNotificationSystem.UnitTests.Domain.UserAggregate
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public async Task CreateUser_ReturnNewUser()
        {
            // Arrange
            var address = Address.Create("testHouse", "testStreet", "testCity");

            // Act
            var user = User.Create(new Guid(), DateTime.UtcNow, "test", address);

            // Assert
            user.Should().NotBeNull();
            user.Address.Should().BeEquivalentTo(address);
        }

        [Test]
        public async Task CreateUser_NameIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var address = Address.Create("testHouse", "testStreet", "testCity");

            // Act
            var act = () => User.Create(new Guid(), DateTime.UtcNow, null, address);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public async Task CreateUser_NameIsWhiteSpaces_ThrowsArgumentNulException()
        {
            // Arrange 
            var address = Address.Create("testHouse", "testStreet", "testCity");

            // Act
            var act = () => User.Create(new Guid(), DateTime.UtcNow, "  ", address);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
