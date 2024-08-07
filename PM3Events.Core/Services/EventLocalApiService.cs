using Microsoft.AspNetCore.Http;
using PM3Events.Core.Services.Interfaces;
using System.Net.Http.Json;

namespace PM3Events.Core.Services
{
    public class EventLocalApiService : IEventService, IApiService
    {
        public string BaseUrl => "";

        public Dictionary<string, string> Paths => new Dictionary<string, string>();
        private IList<Event> _events;
        private readonly IHttpClientFactory _clientFactory;

        public EventLocalApiService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public HttpClient CreateClient()
        {
            var client = _clientFactory.CreateClient(nameof(EventLocalApiService));
            return client;
        }

        public async Task<Event?> GetEventByIdAsync(string id)
        {
            if (_events is null)
            {
                await FeedLocalData();
            }
            var maxRange = _events?.Count ?? -1;

            if (maxRange < 0)
            {
                return default;
            }

            var result = _events[getRandomRange(maxRange)];
            result.Id = id;
            return result;
        }

        public async Task<IEnumerable<Event>> GetEventsAsync()
        {
            if (_events is null)
            {
                await FeedLocalData();
            }
            return _events;
        }

        private async Task FeedLocalData()
        {
            var client = CreateClient();
            _events = await client.GetFromJsonAsync<List<Event>>("sample-data/events-inmemory.json") ?? new List<Event>();
        }

        private int getRandomRange(int maxRange)
        {
            var random = new Random();
            return random.Next(maxRange);
        }
    }
}
