using Microsoft.Extensions.Configuration;
using RemoteConfigurationProvider.Domain.Contracts;

namespace RemoteConfigurationProvider.Domain
{
    public static class ConfigurationProviderExtensions
    {
        public static IConfigurationBuilder AddRemoteJsonFile(this IConfigurationBuilder builder, IRemoteFileProvider remoteFileProvider) =>
            builder.Add(new RemoteConfigurationSource(remoteFileProvider));
    }
}