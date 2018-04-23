namespace DotNetCoreApi.Exceptions
{
    using System;

    public class CertificateLoadException : Exception
    {
        public CertificateLoadException()
            : base("Could not load certificate")
        {
        }

        public CertificateLoadException(string message)
            :base (message)
        {
        }

        public CertificateLoadException(Exception e)
            : base("Could not load certificate", e)
        {
            
        }
    }
}