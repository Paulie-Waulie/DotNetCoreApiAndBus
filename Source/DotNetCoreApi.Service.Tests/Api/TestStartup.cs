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

        protected override void AddBoundaryDependencies(IServiceCollection services)
        {
            services.AddSingleton<IGetPaymentProviderRedirectQuery, GetPaymentProviderRedirectQuery>()
                .AddSingleton<ISavePaymentCommand, CosmosDbStub>()
                .AddSingleton<IGetPaymentQuery, CosmosDbStub>();
        }
    }
}
