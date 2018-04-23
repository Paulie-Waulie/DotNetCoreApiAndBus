namespace DotNetCoreApi.Service.Tests.Api
{
    using System;
    using System.Net.Http;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Refit;

    internal class ApiFactory : IDisposable
    {
        private HttpClientHandler httpClientHandler;
        private HttpClient httpClient;

        internal IDotNetCoreApi Create()
        {
            this.httpClientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = ServerCertificateValidationCallback };

            if (FixtureSetup.ApiServerSettings.InProcess)
            {
                var server = new TestServer(new WebHostBuilder().UseApplicationInsights().UseStartup<TestStartup>());
                this.httpClient = server.CreateClient();
            }
            else
            {
                this.httpClient = new HttpClient(this.httpClientHandler);
                this.httpClient.BaseAddress = new Uri(FixtureSetup.ApiServerSettings.BaseAddress);
            }

            return RestService.For<IDotNetCoreApi>(this.httpClient);
        }

        public void Dispose()
        {
            httpClientHandler?.Dispose();
            httpClient?.Dispose();
        }

        private bool ServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            var message = (HttpRequestMessage)sender;
            return message.RequestUri.IsLoopback;
        }
    }
}