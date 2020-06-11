using System;

namespace Mews.Fiscalization.Hungary.Models
{
    public class Address
    {
        internal Address(
            string countryCode,
            string postalCode,
            string city,
            string streetName,
            string number,
            string floor,
            string door,
            AddressType type)
        {
            CountryCode = countryCode;
            PostalCode = postalCode;
            City = city;
            StreetName = streetName;
            Number = number;
            Floor = floor;
            Door = door;
            Type = type;
        }

        public string CountryCode { get; }

        public string PostalCode { get; }

        public string City { get; }

        public string StreetName { get; }

        public string Number { get; }

        public string Floor { get; }

        public string Door { get; }

        public AddressType Type { get; }

        internal static Address Map(Dto.TaxpayerAddressItem addressItem)
        {
            var address = addressItem.TaxpayerAddress;
            return new Address(
                countryCode: address.CountryCode,
                postalCode: address.PostalCode,
                city: address.City,
                streetName: address.StreetName,
                number: address.Number,
                floor: address.Floor,
                door: address.Door,
                type: (AddressType)Enum.Parse(typeof(AddressType), addressItem.TaxpayerAddressType, true)
            );
        }
    }
}
