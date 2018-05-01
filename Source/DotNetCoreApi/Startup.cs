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
    using Swashbuckle.AspNetCore.Swagger;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSwaggerGen(options => options.SwaggerDoc(SwaggerSettings.DocumentVersion, new Info { Title = SwaggerSettings.DocumentTitle, Version = SwaggerSettings.DocumentTitle }));
            services.AddSingleton<IPaymentService, PaymentService>();

            this.AddInternalDependencies(services);
            this.AddBoundaryDependencies(services);
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
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(SwaggerSettings.JsonEndpointAddress, SwaggerSettings.DocumentVersion);
                options.RoutePrefix = string.Empty;
            });

            MappingConfiguration.Configure();
        }

        protected virtual void AddBoundaryDependencies(IServiceCollection services)
        {
            services.Configure<DocumentDbSettings>(settings => { Configuration.BindOrThrow("DocumentDB", settings); });
            services.Configure<PaymentProviderSettings>(settings => { Configuration.BindOrThrow("PaymentProvider", settings); });

            services.AddSingleton<IGetPaymentProviderRedirectQuery, GetPaymentProviderRedirectQuery>()
                .AddSingleton<ISavePaymentCommand, SavePaymentCommand>()
                .AddSingleton<IGetPaymentQuery, GetPaymentQuery>()
                .AddSingleton<IDocumentClientFactory, DocumentClientFactory>();
        }

        private void AddInternalDependencies(IServiceCollection services)
        {
            services.AddSingleton<IPaymentService, PaymentService>();
        }
    }
}
