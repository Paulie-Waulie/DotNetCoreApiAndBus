namespace DotNetCoreApi.Exceptions
{
    using System;
    using DotNetCore.Contracts.Values;

    public class PaymentNotFoundException : Exception
    {
        public PaymentNotFoundException(string paymentReference)
            : base($"Payment with reference {paymentReference} does not exist")
        {
        }
    }
}