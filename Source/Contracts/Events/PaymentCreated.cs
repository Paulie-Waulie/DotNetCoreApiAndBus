namespace DotNetCore.Contracts.Events
{
    using Rest;
    using Values;

    public class PaymentCreated
    {
        public PaymentCreated(PaymentReference reference)
        {
            Reference = reference;
        }

        public PaymentReference Reference { get; }

        public Address BillingAddress { get; set; }
    }
}
