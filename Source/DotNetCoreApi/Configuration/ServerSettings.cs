namespace DotNetCoreApi.Configuration
{
    using System;

    public class ServerSettings
    {
        public int Port { get; set; }

        public bool UseHttps { get; set; }

        public string ApiCertificateThumbprint { get; set; }
    }
}