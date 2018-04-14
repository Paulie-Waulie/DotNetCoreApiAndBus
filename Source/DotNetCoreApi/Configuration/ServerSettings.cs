namespace DotNetCoreApi.Configuration
{
    public class ServerSettings
    {
        public int Port { get; set; }

        public bool UseHttps { get; set; }

        public string ApiCertificateThumbprint { get; set; }
    }
}