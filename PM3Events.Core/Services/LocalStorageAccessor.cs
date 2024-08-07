
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace PM3Events.Core.Services
{
    public class LocalStorageAccessor : IAsyncDisposable
    {
        private Lazy<IJSObjectReference> _accessorJsModule = new();
        private Lazy<IJSObjectReference> _accessorJsRef = new();
        private readonly IJSRuntime _jsRuntime;
        private readonly string _jsFilePath;

        public LocalStorageAccessor(IJSRuntime jsRuntime, string jsFilePath)
        {
            _jsRuntime = jsRuntime;
            _jsFilePath = jsFilePath;
        }

        public async Task<T?> GetValueAsync<T>(string key)
        {
            await WaitForReference();
            var resultStr = await _accessorJsRef.Value.InvokeAsync<string>("get", key);

            if (resultStr is null)
            {
                return default;
            }

            var result = JsonConvert.DeserializeObject<T>(resultStr);

            return result;
        }

        public async Task SetValueAsync<T>(string key, T value)
        {
            await WaitForReference();
            var jsonStr = JsonConvert.SerializeObject(value);
            await _accessorJsRef.Value.InvokeVoidAsync("set", key, jsonStr);
        }

        public async Task Clear()
        {
            await WaitForReference();
            await _accessorJsRef.Value.InvokeVoidAsync("clear");
        }

        public async Task RemoveAsync(string key)
        {
            await WaitForReference();
            await _accessorJsRef.Value.InvokeVoidAsync("remove", key);
        }

        private async Task WaitForReference()
        {
            if (_accessorJsModule.IsValueCreated is false)
            {
                _accessorJsModule = new(await _jsRuntime.InvokeAsync<IJSObjectReference>("import", _jsFilePath));
                _accessorJsRef = new(await _accessorJsModule.Value.InvokeAsync<IJSObjectReference>("LocalStorageAccessorInstance"));
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_accessorJsModule.IsValueCreated)
            {
                await _accessorJsModule.Value.DisposeAsync();
                await _accessorJsRef.Value.DisposeAsync();
            }
        }
    }
}
