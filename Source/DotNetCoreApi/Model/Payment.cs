namespace DotNetCoreApi.Model
{
    using DotNetCore.Contracts.Values;

    public class Payment
    {
        public PaymentReference PaymentReference { get; set; }

        public Transaction Transaction { get; set; }

        public Address BillingAddress { get; set; }

        public Address DeliveryAddress { get; set; }

        public Item[] Items { get; set; }

        public Customer Customer { get; set; }
    }
}
