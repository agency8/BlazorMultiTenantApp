global using Blazored.LocalStorage;
global using System.Security.Claims;
global using BlazorMultiTenant.Client.Helpers;
global using BlazorMultiTenant.Shared.Models;
global using BlazorMultiTenant.Shared.Dtos;
global using BlazorMultiTenant.Shared.Enums;
global using BlazorMultiTenant.Client.Services.AuthService;
global using BlazorMultiTenant.Client.Services.HeroService;
global using BlazorMultiTenant.Client.Services.TenantManager;
global using Microsoft.AspNetCore.Components.Authorization;
global using System.Net.Http.Json;

using BlazorMultiTenant.Client;
using BlazorMultiTenant.Shared.Helpers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IHeroService, HeroService>();
builder.Services.AddScoped<ITenantManager, TenantManager>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore(config =>
{
	config.AddPolicy(Policies.IsSuperUser, Policies.IsSuperUserPolicy());
	config.AddPolicy(Policies.IsAdmin, Policies.IsAdminPolicy());
	config.AddPolicy(Policies.IsUser, Policies.IsUserPolicy());
});
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

await builder.Build().RunAsync();
