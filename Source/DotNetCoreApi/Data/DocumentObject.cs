namespace DotNetCoreApi.Data
{
    using Microsoft.Azure.Documents;
    using Newtonsoft.Json;

    internal class DocumentObject<T> : Document
    {
        [JsonProperty("Document")]
        public T Document { get; set; }
    }
}
