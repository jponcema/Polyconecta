using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PolyConecta.Presentation;
using PolyConecta.Presentation.Hubs;
using PolyConecta.Presentation.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSignalR();

builder.Services.AddScoped<UiAppShellState>();
builder.Services.AddScoped<UiViewState>();
builder.Services.AddScoped<OperationalFlowState>();

var app = builder.Build();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapHub<ChatterHub>("/hubs/chatter");

app.Run();
