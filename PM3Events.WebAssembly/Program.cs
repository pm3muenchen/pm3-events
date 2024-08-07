using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PM3Events.WebAssembly;
using PM3Events.Core;
using PM3Events.Core.Services.Interfaces;
using PM3Events.Core.Services;
using PM3Events.Core.Extensions;
using Radzen;
using PM3Events.WebAssembly.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.Configure<ApiSettings>(options =>
    builder.Configuration.GetSection("ApiSettings").Bind(options));

builder.Services.AddHttpClient();
if (builder.HostEnvironment.IsDevelopment())
{
    builder.Services.AddHttpClient(nameof(EventLocalApiService), client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
    builder.Services.AddScoped<IEventService, EventLocalApiService>();
}
else
{
    builder.Services.AddScoped<IEventService, EventApiService>();
}
builder.Services.AddScoped<BrowserService>();
builder.Services.AddLocalStorageAccessor("/scripts/jsLocalStorageAccessor.js");
builder.Services.AddRadzenComponents();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

await builder.Build().RunAsync();
