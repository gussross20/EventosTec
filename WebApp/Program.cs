using Blazored.Toast;
using WebApp;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Service;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// GRR: HTTPCLIENT
builder.Services.AddHttpClient("", config =>
{
    config.BaseAddress = new Uri("https://localhost:7243/");
    config.DefaultRequestHeaders.Clear();
});
// GRR: FACTORY HTTPCLIENTS
builder.Services.AddScoped<IHttpClientFactoryService, HttpClientFactoryService>();
// GRR: INJECT IOC SERVICE
builder.Services.AddService();
builder.Services.AddBlazoredToast();

await builder.Build().RunAsync();
