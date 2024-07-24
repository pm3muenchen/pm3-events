using Newtonsoft.Json;
using PM3Events.Core.Services.Interfaces;

namespace PM3Events.Core.Services
{
    public class EventInMemoryService : IEventService
    {
        private IList<Event>? _events { get; set; }

        public async Task<Event?> GetEventByIdAsync(string id)
        {
            await SeedEventsDataAsync();
            return _events?.First(e => e.Id == id);
        }

        public async Task<IEnumerable<Event>> GetEventsAsync()
        {
            await SeedEventsDataAsync();
            return _events;
        }

        private async Task SeedEventsDataAsync()
        {
            if (_events == null)
            {
                var eventsJson = await File.ReadAllTextAsync(@"Data" + Path.DirectorySeparatorChar + "events-inmemory.json");
                _events = JsonConvert.DeserializeObject<List<Event>>(eventsJson);
            }            
        }
    }
}
