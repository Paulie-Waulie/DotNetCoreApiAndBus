namespace DotNetCoreApi.Configuration
{
    using System.Linq;
    using Exceptions;
    using Microsoft.Extensions.Configuration;

    public static class ConfigurationExtensions
    {
        public static void BindOrThrow(this IConfiguration configuration, string key, object instance)
        {
            if (configuration.GetChildren().Any(x => x.Key.Equals(key)))
            {
                configuration.GetSection(key).Bind(instance);
            }
            else
            {
                throw new ConfigurationSectionMissingException(key);
            }
        }
    }
}