
namespace EmergencyNotificationSystem.Domain.Models.UserAggregate
{
    public class Address : ValueObject
    {
        public string House { get; private set; }
        public string Street { get; private set; }
        public string City { get; private set; }

        public Address(string house, string street, string city)
        {
            House = house;
            Street = street;
            City = city;
        }

        public static Address Create(string house, string street, string city)
        {
            if(string.IsNullOrEmpty(house))
            {
                throw new ArgumentNullException("House can't be null");
            }

            if(string.IsNullOrEmpty(street))
            {
                throw new ArgumentNullException("Street can't be null!");
            }

            if (string.IsNullOrEmpty(city))
            {
                throw new ArgumentNullException("City can't be null!");
            }

            return new Address(house, street, city);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return House;
            yield return Street;
            yield return City;
        }
    }
}
