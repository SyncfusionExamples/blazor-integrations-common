using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace Client.Services
{
    public class AuthMessageHandler : DelegatingHandler
    {
        private readonly ILocalStorageService storage;
        public AuthMessageHandler(ILocalStorageService storage) => this.storage = storage;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
        {
            var token = await storage.GetItemAsync<string>("jwtToken");
            if (!string.IsNullOrWhiteSpace(token))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, ct);
        }
    }
}
