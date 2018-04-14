namespace DotNetCoreApi.Configuration
{
    using System.Net;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Microsoft.Extensions.Configuration;

    internal static class KestrelServerOptionsExtensions
    {
        internal static void Configure(this KestrelServerOptions kestrelServerOptions, IConfiguration configuration)
        {
            var secureSettings = new ServerSettings();
            configuration.BindOrThrow("ServerSettings", secureSettings);

            ConfigureListener(kestrelServerOptions, secureSettings);
        }

        private static void ConfigureListener(KestrelServerOptions kestrelServerOptions, ServerSettings serverSettings)
        {
            kestrelServerOptions.Listen(
                new IPEndPoint(IPAddress.Any, serverSettings.Port),
                options => ConfigureListenOptions(options, serverSettings));
        }

        private static void ConfigureListenOptions(ListenOptions listenOptions, ServerSettings serverSettings)
        {
            listenOptions.KestrelServerOptions.AddServerHeader = false;

            if (serverSettings.UseHttps)
            {
                listenOptions.UseHttps(ApiCertificateResolver.GetApiCertificate(serverSettings));
            }
        }
    }
}