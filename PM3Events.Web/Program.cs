using PM3Events.Core;
using PM3Events.Core.Services;
using PM3Events.Core.Services.Interfaces;
using PM3Events.Web.Components;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.Configure<ApiSettings>(options =>
    builder.Configuration.GetSection("ApiSettings").Bind(options));

builder.Services.AddHttpClient();
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddScoped<IEventService, EventInMemoryService>();
}
else
{
    builder.Services.AddScoped<IEventService, EventApiService>();
}
builder.Services.AddRadzenComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
