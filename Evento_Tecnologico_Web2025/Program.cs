using Blazored.Toast;
using Evento_Tecnologico_Web2025;
using Evento_Tecnologico_Web2025.Components;
using Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// GRR: HTTPCLIENT
builder.Services.AddHttpClient("", config =>
{
    config.BaseAddress = new Uri("https://localhost:7243/");
    config.DefaultRequestHeaders.Clear();
});

// GRR: FACTORY HTTPCLIENTS
builder.Services.AddScoped<IHttpClientFactoryService, HttpClientFactoryService>();

// GRR: INJECT IOC SERVICE (tus servicios personalizados)
builder.Services.AddService();

builder.Services.AddBlazoredToast();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
