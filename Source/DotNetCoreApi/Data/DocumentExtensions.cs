namespace DotNetCoreApi.Data
{
    using System;

    internal static class DocumentExtensions
    {
        public static DocumentObject<T> Wrap<T>(this T obj, Func<Guid> idSelector)
        {
            return obj.Wrap(() => idSelector().ToString());
        }

        public static DocumentObject<T> Wrap<T>(this T obj, Func<string> idSelector)
        {
            return new DocumentObject<T>
            {
                Id = idSelector(),
                Document = obj
            };
        }

        public static T Unwrap<T>(this DocumentObject<T> obj)
        {
            return obj.Document;
        }
    }
}