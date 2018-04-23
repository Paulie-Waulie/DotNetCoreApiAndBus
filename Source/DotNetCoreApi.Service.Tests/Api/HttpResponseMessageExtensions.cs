namespace DotNetCoreApi.Service.Tests.Api
{
    using System.Net.Http;
    using Newtonsoft.Json;

    internal static class HttpResponseMessageExtensions
    {
        internal static T ReadAs<T>(this HttpContent httpContent)
        {
            var json = httpContent.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}