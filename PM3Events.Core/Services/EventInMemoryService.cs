using Newtonsoft.Json;
using PM3Events.Core.Services.Interfaces;
using System.Reflection;

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
                var currentDir = Path.GetFullPath(Directory.GetCurrentDirectory());
                var filePath = Path.GetFullPath(currentDir + ".." + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "events-inmemory.json");
                var eventsJson = await File.ReadAllTextAsync(filePath);
                _events = JsonConvert.DeserializeObject<List<Event>>(eventsJson);
            }            
        }
    }
}
