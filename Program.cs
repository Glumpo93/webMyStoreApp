using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using webMyStoreApp.Authentication;
using webMyStoreApp.Components;
using webMyStoreApp.Components.Services;

var builder = WebApplication.CreateBuilder(args);

// Añade servicios a la colección de servicios
builder.Services.AddAuthentication();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddAuthorizationCore();        

builder.Services.AddScoped<AuthenticationStateProvider, UserCompanyAuthenticationStateProvider>();
builder.Services.AddScoped<UserAccountService>();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<UserSession>();
builder.Services.AddTransient<SchemeService>();

var app = builder.Build();

// Configura el pipeline de solicitudes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
