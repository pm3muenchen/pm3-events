using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace PM3Events.Api.Extensions
{
    public static class HttpRequestExtensions
    {
        public static async Task<T> GetParameterValueInBodyAsync<T>(this HttpRequest httpRequest, string paramName)
        {
            using TextReader reader = new StreamReader(httpRequest.Body);
            var text = await reader.ReadToEndAsync();
            if (text.Length > 0)
            {
                try
                {
                    var json = JsonSerializer.Deserialize<JsonElement>(text);
                    if (json.TryGetProperty(paramName, out JsonElement parameterElement))
                    {                        
                        return parameterElement.Deserialize<T>();
                    }
                }
                catch (JsonException parseException)
                {
                    throw;
                }
            }
            return default;
        }
    }
}
