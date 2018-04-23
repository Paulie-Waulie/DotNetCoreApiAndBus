namespace DotNetCoreApi.Service.Tests.TestDoubles
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Data;
    using Exceptions;
    using Model;

    internal class CosmosDbStub : ISavePaymentCommand, IGetPaymentQuery
    {
        private static readonly Dictionary<string, Payment> Payments = new Dictionary<string, Payment>();

        public Task Save(Payment payment)
        {
            this.ThrowCosmosUnavailableIfTestReference(payment.PaymentReference.ToString());

            if (Payments.ContainsKey(payment.PaymentReference.ToString()))
            {
                throw new DuplicatePaymentException(payment.PaymentReference);
            }

            Payments.Add(payment.PaymentReference.ToString(), payment);
            return Task.CompletedTask;
        }

        public Task<Payment> Get(string paymentReference)
        {
            this.ThrowCosmosUnavailableIfTestReference(paymentReference);

            if (Payments.TryGetValue(paymentReference, out var payment))
            {
                return Task.FromResult(payment);
            }

            throw new PaymentNotFoundException(paymentReference);
        }

        private void ThrowCosmosUnavailableIfTestReference(string paymentReference)
        {
            if (paymentReference.Equals("66666666-6666-6666-6666-666666666666"))
            {
                throw new CosmosDatabaseUnavailableException(HttpStatusCode.InternalServerError, new Exception());
            }
        }
    }
}
