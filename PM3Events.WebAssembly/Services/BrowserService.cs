using Microsoft.JSInterop;
using PM3Events.WebAssembly.Models;

namespace PM3Events.WebAssembly.Services
{
    internal class BrowserService
    {
        private readonly IJSRuntime _js;

        public BrowserService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task<BrowserDimension> GetDimensions()
        {
            return await _js.InvokeAsync<BrowserDimension>("getDimensions");
        }
    }
}
