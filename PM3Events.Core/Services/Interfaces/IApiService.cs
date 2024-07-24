
namespace PM3Events.Core.Services.Interfaces
{
    public interface IApiService
    {
        public string BaseUrl { get; }
        public Dictionary<string, string> Paths { get; }

        public HttpClient CreateClient();
    }
}
