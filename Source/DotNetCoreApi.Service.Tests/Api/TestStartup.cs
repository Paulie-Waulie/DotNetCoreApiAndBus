namespace DotNetCoreApi.Service.Tests.Api
{
    using AutoMapper;
    using Data;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Provider.Query;
    using TestDoubles;

    internal class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
            Mapper.Reset();
        }

        protected override void AddDependencies(IServiceCollection services)
        {
            services.AddSingleton<IPaymentService, PaymentService>()
                .AddSingleton<IGetPaymentProviderRedirectQuery, GetPaymentProviderRedirectQuery>()
                .AddSingleton<ISavePaymentCommand, CosmosDbStub>()
                .AddSingleton<IGetPaymentQuery, CosmosDbStub>();
        }
    }
}
