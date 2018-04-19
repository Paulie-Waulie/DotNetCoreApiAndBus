namespace DotNetCoreApi.Provider.Query
{
    using System;
    using System.Threading.Tasks;
    using Configuration;
    using Contracts;
    using Microsoft.Extensions.Options;

    public interface IGetPaymentProviderRedirectQuery
    {
        Task<PaymentRedirect> Get(global::DotNetCoreApi.Model.Payment payment);
    }

    public class GetPaymentProviderRedirectQuery : IGetPaymentProviderRedirectQuery
    {
        private readonly PaymentProviderSettings paymentProviderSettings;

        public GetPaymentProviderRedirectQuery(IOptions<PaymentProviderSettings> paymentProviderSettings)
        {
            this.paymentProviderSettings = paymentProviderSettings.Value;
        }

        public Task<PaymentRedirect> Get(Model.Payment payment)
        {
            // In reality, call the provider using the provided settings.
            return Task.FromResult(new PaymentRedirect(new Uri("https://paymentProvider/Redirect")));
        }
    }
}