namespace DotNetCoreApi.Configuration
{
    using System;

    public class DocumentDbSettings
    {
        public string DatabaseId => "Payments";

        public string CollectionId => "DemoPayments";

        public string AuthKey { get; set; }

        public Uri EndpointUri { get; set; }
    }
}