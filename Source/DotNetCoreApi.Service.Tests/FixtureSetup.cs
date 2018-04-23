namespace DotNetCoreApi.Service.Tests
{
    using Api;
    using Microsoft.Extensions.Configuration;
    using NUnit.Framework;

    [SetUpFixture]
    public class FixtureSetup
    {
        public static IConfiguration Configuration;

        public static ApiServerSettings ApiServerSettings;

        [OneTimeSetUp]
        public void Setup()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();


            ApiServerSettings = new ApiServerSettings();
            Configuration.Bind("ApiServerSettings", ApiServerSettings);
        }
    }
}
