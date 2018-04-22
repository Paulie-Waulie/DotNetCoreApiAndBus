namespace DotNetCore.Contracts.Values
{
    using System;

    public class PaymentReference
    {
        public PaymentReference()
            : this(Guid.NewGuid())
        {
        }

        public PaymentReference(Guid value)
        {
            this.Value = value;
        }

        public PaymentReference(string value)
            :this(Guid.Parse(value))
        {
        }

        public Guid Value { get; private set; }

        public static PaymentReference Create()
        {
            return new PaymentReference(Guid.NewGuid());
        }

        public override string ToString()
        {
            return this.Value.ToString("D");
        }
    }
}