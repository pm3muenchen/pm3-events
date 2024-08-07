using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using PM3Events.Core.Services;

namespace PM3Events.Core.Extensions
{
    public static class LocalStorageAccessorExtensions
    {
        public static IServiceCollection AddLocalStorageAccessor(this IServiceCollection services, string jsFilePath)
        {
            return services.AddScoped(s => 
                new LocalStorageAccessor(s.GetRequiredService<IJSRuntime>(), jsFilePath));
        }
    }
}
