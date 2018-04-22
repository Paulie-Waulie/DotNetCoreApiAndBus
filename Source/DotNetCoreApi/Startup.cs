namespace DotNetCoreApi
{
    using Configuration;
    using Data;
    using Mapping;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Middleware;
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

            services.Configure<DocumentDbSettings>(settings => { Configuration.BindOrThrow("DocumentDB", settings); } );
            services.Configure<PaymentProviderSettings>(settings => { Configuration.BindOrThrow("PaymentProvider", settings); } );

            services.AddSingleton<IPaymentService, PaymentService>()
                .AddSingleton<IGetPaymentProviderRedirectQuery, GetPaymentProviderRedirectQuery>()
                .AddSingleton<ISavePaymentCommand, SavePaymentCommand>()
                .AddSingleton<IGetPaymentQuery, GetPaymentQuery>()
                .AddSingleton<IDocumentClientFactory, DocumentClientFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionHandling>();
            app.UseMvc();
            MappingConfiguration.Configure();
        }
    }
}
