namespace DotNetCoreApi.Contracts
{
    using System;

    public class PaymentRedirect
    {
        public PaymentRedirect()
        {
        }

        public PaymentRedirect(Uri redirectUri)
        {
            RedirectUri = redirectUri ?? throw new ArgumentNullException(nameof(redirectUri));
        }

        public Uri RedirectUri { get; set; }
    }
}