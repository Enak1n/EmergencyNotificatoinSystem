using EmergencyNotificationSystem.Domain.Models.CompayAggregate;
using EmergencyNotificationSystem.Domain.Models.UserAggregate;
using FluentAssertions;

namespace EmergencyNotificationSystem.UnitTests.Domain.CompanyAggregate
{
    [TestFixture]
    public class CompanyTests
    {
        [Test]
        public async Task CreateCompany_ReturnNewCompany()
        {
            // Act
            var company = Company.Create(new Guid(), DateTime.UtcNow, "test");

            // Assert
            company.Should().NotBeNull();
        }

        [Test]
        public async Task CreateCompany_NameIsNull_ThrowsArgumentNullException()
        {
            // Act
            var act = () => Company.Create(new Guid(), DateTime.UtcNow, null);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public async Task CreateCompany_NameIsWhiteSpace_ThrowsArgumentNullException()
        {
            // Act
            var act = () => Company.Create(new Guid(), DateTime.UtcNow, "    ");

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public async Task AddUsers_CompanyWithUsers()
        {
            // Arrange
            var company = Company.Create(new Guid(), DateTime.UtcNow, "test");
            var address = Address.Create("testHouse", "testStreet", "testCity");
            var user = User.Create(new Guid(), DateTime.UtcNow, "test", address);

            // Act
            company.AddUser(user);

            // Assert
            company.Users.Should().NotBeEmpty();
        }
    }
}
