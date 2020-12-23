using System;
using System.Collections.Generic;
using System.Net.Http;
using RemoteConfigurationProvider.Domain.Contracts;

namespace RemoteConfigurationProvider.Domain
{
    public abstract class RemoteFileProvider : IRemoteFileProvider
    {
        protected RemoteFileProvider(string path)
        {
            Path = path;
        }

        public string Path { get; }

        public IDictionary<string, string> ReadFile()
        {
            var response = GetFile();
            ValidateResult(response);
            return response.IsSuccessStatusCode
                ? ReadContent(response)
                : new Dictionary<string, string>();
        }

        private void ValidateResult(HttpResponseMessage responseMessage)
        {
            if (responseMessage.IsSuccessStatusCode == false)
                throw new Exception(responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }
        
        protected abstract HttpResponseMessage GetFile();
        protected abstract IDictionary<string, string> ReadContent(HttpResponseMessage httpResponseMessage);
    }
}