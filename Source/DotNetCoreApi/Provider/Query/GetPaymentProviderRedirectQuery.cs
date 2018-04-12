namespace DotNetCoreApi.Provider.Query
{
    using System;
    using System.Threading.Tasks;
    using Contracts;

    public interface IGetPaymentProviderRedirectQuery
    {
        Task<PaymentRedirect> Get(Payment payment);
    }

    public class GetPaymentProviderRedirectQuery : IGetPaymentProviderRedirectQuery
    {
        public Task<PaymentRedirect> Get(Payment payment)
        {
            return Task.FromResult(new PaymentRedirect(new Uri("https://paymentProvider/Redirect")));
        }
    }
}