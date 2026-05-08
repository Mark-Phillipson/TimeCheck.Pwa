using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TimeCheck.Pwa;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
// TimeCheck PWA services
builder.Services.AddScoped<TimeCheck.Pwa.Services.ITtsService, TimeCheck.Pwa.Services.TtsService>();
builder.Services.AddScoped<TimeCheck.Pwa.Services.ISettingsService, TimeCheck.Pwa.Services.BrowserSettingsService>();
builder.Services.AddScoped<TimeCheck.Pwa.Services.ITimeCheckService, TimeCheck.Pwa.Services.TimeCheckService>();
builder.Services.AddScoped<TimeCheck.Pwa.Services.IEncouragementService, TimeCheck.Pwa.Services.EncouragementService>();

await builder.Build().RunAsync();
