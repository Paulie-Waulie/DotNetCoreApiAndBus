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
                    configuration = builder.Build();
                    var environment = hostingContext.HostingEnvironment;

                    if (environment.IsDevelopment())
                    {
                        builder.AddUserSecrets(Assembly.Load(new AssemblyName(environment.ApplicationName)), optional: true);
                    }
                })
                .UseStartup<Startup>()
                .UseKestrel(options => options.Configure(configuration))
                .Build();
        }
    }
}
