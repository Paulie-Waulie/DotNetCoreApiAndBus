namespace DotNetCoreApi.Configuration
{
    using System;
    using System.Security.Cryptography.X509Certificates;
    using Exceptions;

    internal static class ApiCertificateResolver
    {
        internal static X509Certificate2 GetApiCertificate(ServerSettings serverSettings)
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
                    throw new CertificateLoadException("The certificate does not contain a Private Key or you don't have permission to access it.");
                }
            }
            catch (Exception e)
            {
                throw new CertificateLoadException(e);
            }
        }
    }
}