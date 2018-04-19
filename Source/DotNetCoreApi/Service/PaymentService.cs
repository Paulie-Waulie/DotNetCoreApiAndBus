namespace DotNetCoreApi.Service
{
    using System.Threading.Tasks;
    using Contracts;
    using Data;
    using DotNetCoreApi.Model;
    using Provider.Query;

    public interface IPaymentService
    {
        Task<PaymentRedirect> RegisterPayment(Payment payment);
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

        public async Task<PaymentRedirect> RegisterPayment(Payment payment)
        {
            // TODO : Validate.
            PaymentRedirect redirect = await this.getRedirectQuery.Get(payment);
            await this.savePaymentCommand.Save(payment);

            return redirect;
        }
    }
}
