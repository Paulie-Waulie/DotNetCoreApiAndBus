namespace DotNetCoreApi.Configuration
{
    using System;
    using System.Linq;
    using Exceptions;
    using Microsoft.Extensions.Configuration;

    internal static class ConfigurationExtensions
    {
        public static T GetSectionOrThrow<T>(this IConfiguration configuration) where T : new()
        {
            return configuration.GetSectionOrThrow<T>(typeof(T).Name);
        }

        public static T GetSectionOrThrow<T>(this IConfiguration configuration, string key) where T : new()
        {
            T instance = Activator.CreateInstance<T>();
            configuration.BindOrThrow(key, instance);
            return instance;
        }

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