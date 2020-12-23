using Microsoft.Extensions.Configuration;
using RemoteConfigurationProvider.Domain.Contracts;

namespace RemoteConfigurationProvider.Domain
{
    public class RemoteConfigurationSource : IConfigurationSource
    {
        public readonly IRemoteFileProvider Provider;

        public RemoteConfigurationSource(IRemoteFileProvider provider)
        {
            Provider = provider;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder) => new RemoteConfigurationProvider(this);
    }
}