namespace PM3Events.Core.Services.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetEventsAsync();

        Task<Event> GetEventByIdAsync(string id);
    }
}
