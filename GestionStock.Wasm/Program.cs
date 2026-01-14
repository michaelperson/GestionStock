using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GestionStock.Wasm;
using GestionStock.Infrastructure;
using GestionStock.Domain.Interfaces;
using GestionStock.Wasm.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddSingleton<IStateService, StateService>();
builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped(sp => new HttpClient {
     BaseAddress = new Uri("https://demogestionstockapi-cgdrb9bpa5d3dfht.westeurope-01.azurewebsites.net/") });

await builder.Build().RunAsync();
