namespace DotNetCoreApi
{
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
                .ConfigureAppConfiguration(builder =>
                {
                    configuration = builder.Build();
                })
                .UseStartup<Startup>()
                .UseKestrel(options => options.Configure(configuration))
                .Build();
        }
    }
}
