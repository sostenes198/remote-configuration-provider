using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using RemoteConfigurationProvider.Domain.GitLab.Dto;
using RemoteConfigurationProvider.Domain.GitLab.Handler;
using RemoteConfigurationProvider.Domain.Helpers;

namespace RemoteConfigurationProvider.Domain.GitLab
{
    public class RemoteFileProviderGitLab : RemoteFileProvider
    {
        private readonly HttpClient _httpClient;

        public RemoteFileProviderGitLab(string path, string accessToken)
            : base(path)
        {
            _httpClient = new HttpClient(GitlabAuthenticationHttpHandler.Handler(accessToken));
        }

        public RemoteFileProviderGitLab(string path)
            : base(path)
        {
            _httpClient = new HttpClient();
        }

        protected override HttpResponseMessage GetFile() => _httpClient.GetAsync(Path).GetAwaiter().GetResult();

        protected override IDictionary<string, string> ReadContent(HttpResponseMessage httpResponseMessage)
        {
            var result = JsonConvert.DeserializeObject<GitLabResult>(httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            return ParseHelper.Parse(new MemoryStream(Convert.FromBase64String(result.Content)));
        }
    }
}