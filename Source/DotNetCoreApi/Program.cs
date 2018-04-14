namespace DotNetCoreApi
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    using Configuration;
    using Exceptions;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
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
                .UseKestrel(options => ConfigureServerOptions(options, configuration))
                .Build();
        }

        private static void ConfigureServerOptions(KestrelServerOptions kestrelServerOptions, IConfiguration configuration)
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
                listenOptions.UseHttps(GetApiCertificate(serverSettings));
            }
        }

        private static X509Certificate2 GetApiCertificate(ServerSettings serverSettings)
        {
            X509Certificate2 apiCertificate;
            using (var store = new X509Store(StoreName.My, StoreLocation.LocalMachine))
            {
                store.Open(OpenFlags.ReadOnly);
                var certifacates = store.Certificates.Find(X509FindType.FindByThumbprint, serverSettings.ApiCertificateThumbprint, false);
                store.Close();

                if (certifacates.Count == 0)
                {
                    throw new CertificateLoadException();
                }

                apiCertificate = certifacates[0];

                CheckPrivateKeyExists(apiCertificate);
            }

            return apiCertificate;
        }


        private static void CheckPrivateKeyExists(X509Certificate2 cert)
        {
            try
            {
                if (!cert.HasPrivateKey || cert.PrivateKey.KeySize == 0)
                {
                    throw new Exception("The certificate does not contain a Private Key or you don't have permission to access it.");
                }
            }
            catch (Exception e)
            {
                throw new CertificateLoadException(e);
            }
        }
    }
}
