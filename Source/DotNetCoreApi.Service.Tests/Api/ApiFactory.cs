﻿namespace DotNetCoreApi.Service.Tests.Api
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
            this.httpClientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = this.ServerCertificateValidationCallback };

            if (FixtureSetup.ApiServerSettings.InProcess)
            {
                var server = new TestServer(new WebHostBuilder().UseApplicationInsights().UseStartup<TestStartup>());
                this.httpClient = server.CreateClient();
            }
            else
            {
                this.httpClient = new HttpClient(this.httpClientHandler)
                {
                    BaseAddress = new Uri(FixtureSetup.ApiServerSettings.BaseAddress)
                };
            }

            return RestService.For<IDotNetCoreApi>(this.httpClient);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this.httpClientHandler?.Dispose();
            this.httpClient?.Dispose();
        }

        private bool ServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            var message = (HttpRequestMessage)sender;
            return message.RequestUri.IsLoopback;
        }
    }
}