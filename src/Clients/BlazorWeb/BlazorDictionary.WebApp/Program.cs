using BlazorDictionary.WebApp;
using BlazorDictionary.WebApp.Infrastructure.Auth;
using BlazorDictionary.WebApp.Infrastructure.Services;
using BlazorDictionary.WebApp.Infrastructure.Services.Interfaces;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Configuration["address"]; config den url adresini almak istersek
builder.Services.AddHttpClient("WebApiClient", client =>
 {
     client.BaseAddress = new Uri("https://localhost:5001");
 }); //TODO AuthTokenHandler will be here

builder.Services.AddScoped(serviceProvider =>
{
    var clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

    return clientFactory.CreateClient("WebApiClient");
});

builder.Services.AddTransient<IVoteService, VoteService>();

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddTransient<IEntryService, EntryService>();
builder.Services.AddTransient<IVoteService, VoteService>();
builder.Services.AddTransient<IFavService, FavService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IIdentityService, IdentityService>();

builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
