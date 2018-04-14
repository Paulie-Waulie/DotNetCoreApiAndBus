namespace DotNetCoreApi
{
    using System.Reflection;
    using Configuration;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;

    public class Program
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
                .Build();
        }

        private static IConfiguration BuildAppConfiguration(IConfigurationBuilder builder, WebHostBuilderContext hostingContext)
        {
            IConfiguration configuration = builder.Build();
            var environment = hostingContext.HostingEnvironment;

            if (environment.IsDevelopment())
            {
                builder.AddUserSecrets(Assembly.Load(new AssemblyName(environment.ApplicationName)), optional: true);
            }

            KeyVaultSettings keyVaultSettings = new KeyVaultSettings();
            configuration.BindOrThrow("KeyVaultSettings", keyVaultSettings);

            builder.AddAzureKeyVault(
                keyVaultSettings.DnsName,
                keyVaultSettings.AppUserClientId,
                keyVaultSettings.AppUserClientSecret);

            return configuration;
        }
    }
}
