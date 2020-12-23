using System.Collections.Generic;

namespace RemoteConfigurationProvider.Domain.Contracts
{
    public interface IRemoteFileProvider
    {
        string Path { get; }
        IDictionary<string, string> ReadFile();
    }
}