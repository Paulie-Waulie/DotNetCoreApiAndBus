namespace DotNetCoreApi.Contracts
{
    using global::Contracts.Models;

    public class Payment
    {
        // NOTE: In reality we would want to use different models for our Api than to our messaging, domain and data access.
        public Transaction Transaction { get; set; }

        public Address BillingAddress { get; set; }

        public Address DeliveryAddress { get; set; }

        public Item[] Items { get; set; }

        public Customer Customer { get; set; }
    }
}
