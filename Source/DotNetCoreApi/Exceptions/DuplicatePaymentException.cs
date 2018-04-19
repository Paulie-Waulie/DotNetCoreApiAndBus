namespace DotNetCoreApi.Exceptions
{
    using System;
    using DotNetCore.Contracts.Values;

    public class DuplicatePaymentException : Exception
    {
        public DuplicatePaymentException(PaymentReference paymentReference)
            : base($"Payment with reference {paymentReference.Value} already exists")
        {
        }
    }
}
