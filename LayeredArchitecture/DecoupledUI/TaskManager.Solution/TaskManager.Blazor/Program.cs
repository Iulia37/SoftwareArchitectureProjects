using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TaskManager.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7119/") });

builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<UserService>();

builder.Services.AddBlazoredSessionStorage();

await builder.Build().RunAsync();
