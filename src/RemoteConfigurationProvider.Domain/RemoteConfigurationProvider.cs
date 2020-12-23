namespace RemoteConfigurationProvider.Domain
{
    public class RemoteConfigurationProvider : Microsoft.Extensions.Configuration.ConfigurationProvider
    {
        private readonly RemoteConfigurationSource _source;

        public RemoteConfigurationProvider(RemoteConfigurationSource source)
        {
            _source = source;
        }

        public override void Load()
        {
            Data = _source.Provider.ReadFile();
        }
    }
}