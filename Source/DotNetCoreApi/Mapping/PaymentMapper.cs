namespace DotNetCoreApi.Mapping
{
    using AutoMapper;
    using DotNetCore.Contracts.Values;
    using Model;

    internal static class PaymentMapper
    {
        internal static Payment ToModel(this DotNetCore.Contracts.Rest.Payment contract, string paymentReference)
        {
            var model = Mapper.Map<Payment>(contract);
            model.PaymentReference = new PaymentReference(paymentReference);

            return model;
        }

        internal static DotNetCore.Contracts.Rest.Payment ToContract(this Payment model)
        {
            return Mapper.Map<DotNetCore.Contracts.Rest.Payment>(model);
        }
    }
}
