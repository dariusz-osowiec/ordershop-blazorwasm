using Blazored.LocalStorage;
using BlazorStrap;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OrderShopWeb.Interfaces;
using OrderShopWeb.Services;
using OrderShopWeb;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazorStrap();

//Obsługa LocalStorage.
builder.Services.AddBlazoredLocalStorage();

//Dodanie usługi opcji serializacji JSON.
builder.Services.AddSingleton<JsonSerializerOptionsService>();

//Dodanie usługi do zarządzania DB.
builder.Services.AddScoped<IDbOperable, HttpDbService>();

//Dodanie usługi do zarządzania koszykiem.
builder.Services.AddScoped<IBasketOperable, BasketService>();

//Dodanie usługi przerzucenia zamówienia z strony podsumowania na stronę potwierdzenia.
builder.Services.AddSingleton<OrderService>();

//Dodanie usługi wysłania maila do sprzedawcy.
builder.Services.AddTransient<SendMailContentService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var app = builder.Build();

await app.RunAsync();