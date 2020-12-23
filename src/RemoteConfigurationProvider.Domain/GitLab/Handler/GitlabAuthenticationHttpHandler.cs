using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RemoteConfigurationProvider.Domain.GitLab.Handler
{
    internal class GitlabAuthenticationHttpHandler : HttpClientHandler
    {
        private readonly string _accessToken;

        private GitlabAuthenticationHttpHandler(string accessToken)
        {
            _accessToken = accessToken;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Private-Token", _accessToken);
            return base.SendAsync(request, cancellationToken);
        }

        public static GitlabAuthenticationHttpHandler Handler(string accessToken) => new GitlabAuthenticationHttpHandler(accessToken);
    }
}