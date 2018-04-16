namespace DotNetCoreApi.Service
{
    using System.Threading.Tasks;
    using Contracts;
    using Data;
    using global::Contracts.Rest;
    using Provider.Query;

    public interface IPaymentService
    {
        Task<PaymentRedirect> RegisterAttempt(string paymentReference, Payment payment);
    }

    public class PaymentService : IPaymentService
    {
        private readonly IGetPaymentProviderRedirectQuery getRedirectQuery;
        private readonly ISavePaymentCommand savePaymentCommand;

        public PaymentService(IGetPaymentProviderRedirectQuery getRedirectQuery, ISavePaymentCommand savePaymentCommand)
        {
            this.getRedirectQuery = getRedirectQuery;
            this.savePaymentCommand = savePaymentCommand;
        }

        public async Task<PaymentRedirect> RegisterAttempt(string paymentReference, Payment payment)
        {
            // TODO : Validate.
            PaymentReference reference = new PaymentReference(paymentReference);
            PaymentRedirect redirect = await this.getRedirectQuery.Get(payment);
            await this.savePaymentCommand.Save(reference, payment);

            return redirect;
        }
    }
}
