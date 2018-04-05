using System;

namespace Contracts.Models
{
    public class Address
    {
        public Address()
        {
            
        }

        public Address(string address1, string address2, string address3, string locality, string countyStateOrArea, string countryCode, string postalCode)
        {
            Address1 = address1 ?? throw new ArgumentNullException(nameof(address1));
            Address2 = address2 ?? throw new ArgumentNullException(nameof(address2));
            Address3 = address3;
            Locality = locality ?? throw new ArgumentNullException(nameof(locality));
            CountyStateOrArea = countyStateOrArea ?? throw new ArgumentNullException(nameof(countyStateOrArea));
            CountryCode = countryCode ?? throw new ArgumentNullException(nameof(countryCode));
            PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
        }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }

        public string Locality { get; set; }

        public string CountyStateOrArea { get; set; }

        public string CountryCode { get; set; }

        public string PostalCode { get; set; }
    }
}