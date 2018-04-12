namespace DotNetCoreApi
{
    using Configuration;
    using Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Azure.Documents;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Provider.Query;
    using Service;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton<IPaymentService, PaymentService>()
                .AddSingleton<IGetPaymentProviderRedirectQuery, GetPaymentProviderRedirectQuery>()
                .AddSingleton<ISavePaymentCommand, SavePaymentCommand>()
                .AddSingleton<IDocumentClient>(DocumentClientFactory.Create(Configuration).Result);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
