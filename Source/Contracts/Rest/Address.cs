namespace Contracts.Rest
{
    using System;

    public class Address
    {
        public Address()
        {
            
        }

        public Address(string firstName, string lastName, string address1, string address2, string address3, string locality, string countyStateOrArea, string country, string postalCode, string telephoneMobile)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Address1 = address1 ?? throw new ArgumentNullException(nameof(address1));
            Address2 = address2 ?? throw new ArgumentNullException(nameof(address2));
            Address3 = address3;
            Locality = locality ?? throw new ArgumentNullException(nameof(locality));
            CountyStateOrArea = countyStateOrArea;
            Country = country ?? throw new ArgumentNullException(nameof(country));
            PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
            TelephoneMobile = telephoneMobile ?? throw new ArgumentNullException(nameof(telephoneMobile));
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }

        public string Locality { get; set; }

        public string CountyStateOrArea { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        public string TelephoneMobile { get; set; }
    }
}