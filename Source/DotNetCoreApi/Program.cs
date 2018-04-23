namespace DotNetCoreApi
{
    using System.Reflection;
    using Configuration;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            IConfiguration configuration = null;

            return WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, builder) =>
                {
                    configuration = BuildAppConfiguration(builder, hostingContext);
                })
                .UseStartup<Startup>()
                .UseKestrel(options => options.Configure(configuration))
                .UseApplicationInsights()
                .Build();
        }

        private static IConfiguration BuildAppConfiguration(IConfigurationBuilder builder, WebHostBuilderContext hostingContext)
        {
            var environment = hostingContext.HostingEnvironment;
            IConfiguration configuration = builder.Build();

            if (environment.IsDevelopment())
            {
                builder.AddUserSecrets(Assembly.Load(new AssemblyName(environment.ApplicationName)), optional: true);
            }

            // TODO : Work out why the instrumentation key below is ignored, the correct instrumentation key seems to be only
            // set if it is present in the appsettings.json or is passed to the overloaded extensions method on IWebHostBuilder called UseApplicationInsights.
            var applicationInsightsSettings = configuration.GetSectionOrThrow<ApplicationInsightsSettings>("ApplicationInsights");
            builder.AddApplicationInsightsSettings(developerMode: environment.IsDevelopment(), instrumentationKey: applicationInsightsSettings.InstrumentationKey);

            var keyVaultSettings = configuration.GetSectionOrThrow<KeyVaultSettings>("KeyVaultSettings");

            builder.AddAzureKeyVault(
                keyVaultSettings.DnsName,
                keyVaultSettings.AppUserClientId,
                keyVaultSettings.AppUserClientSecret);

            return configuration;
        }
    }
}
