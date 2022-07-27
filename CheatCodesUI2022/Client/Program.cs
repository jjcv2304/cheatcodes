using CheatCodesUI2022.Client;
using CheatCodesUI2022.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var localUri = new Uri("https://localhost:44326/");

void RegisterTypedClient<TClient, TImplementation>(Uri apiBaseUrl)
    where TClient : class where TImplementation : class, TClient
{
    builder.Services.AddHttpClient<TClient, TImplementation>(client =>
    {
        client.BaseAddress = apiBaseUrl;
    });
}

RegisterTypedClient<ICategoriesDataService, CategoriesDataService>(localUri);

await builder.Build().RunAsync();
