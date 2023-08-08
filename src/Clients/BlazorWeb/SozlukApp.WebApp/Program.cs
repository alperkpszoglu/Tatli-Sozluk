using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SozlukApp.WebApp;
using SozlukApp.WebApp.Infrastructure.Services;
using SozlukApp.WebApp.Infrastructure.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddHttpClient("SozlukClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:5001");
}); // TODO AuthTokenHandler will be here

builder.Services.AddScoped(sp =>
{
    var clientFactory = sp.GetRequiredService<IHttpClientFactory>();
    return clientFactory.CreateClient("SozlukClient");
});

builder.Services.AddTransient<IVoteService, VoteService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


await builder.Build().RunAsync();
