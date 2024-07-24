using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.WebUtilities;
using PM3Events.Core.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;

namespace PM3Events.Core.Services
{
    public class EventApiService : IApiService, IEventService
    {
        public string BaseUrl => _baseUrl;

        public Dictionary<string, string> Paths => _paths;

        private readonly string _baseUrl;
        private readonly Dictionary<string, string> _paths = new Dictionary<string, string>();

        private readonly IHttpClientFactory _clientFactory;

        public EventApiService(IOptions<ApiSettings> apiSettingsOptions, IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;

            var apiSettings = apiSettingsOptions.Value;
            _baseUrl = apiSettings.BaseUrl;
            _paths.Add("EventsList", apiSettings.ListEvents);
            _paths.Add("EventById", apiSettings.EventById);
        }

        public async Task<Event?> GetEventByIdAsync(string id)
        {
            var client = CreateClient();

            if (!Paths.ContainsKey("EventById"))
            {
                return default;
            }
            var url = new Uri(QueryHelpers.AddQueryString(Paths["EventById"], new Dictionary<string, string> { { "id", id } }));
            using var response = await client.GetAsync(url);
            return await response.Content.ReadFromJsonAsync<Event>() ?? default;
        }

        public async Task<IEnumerable<Event>> GetEventsAsync()
        {
            var client = CreateClient();

            if (!Paths.ContainsKey("EventsList"))
            {
                return new List<Event>();
            }

            return await client.GetFromJsonAsync<List<Event>>(Paths["EventsList"]) ?? new List<Event>();
        }

        public HttpClient CreateClient()
        {
            var client = _clientFactory.CreateClient(nameof(EventApiService));
            client.BaseAddress = new Uri(BaseUrl);
            return client;
        }

        private string GetPathUrl(string pathKey)
        {
            return Paths.ContainsKey(pathKey) ? $"{BaseUrl}/{Paths["EventsList"]}" : string.Empty;
        }
    }
}
