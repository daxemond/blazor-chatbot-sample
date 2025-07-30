using BlazorChatbot.Components;
using BlazorChatbot.Models;
using BlazorChatbot.Services;
using BlazorBootstrap;
var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SecurityOption>(options => builder.Configuration.GetSection("Security").Bind(options));

DataApiOption dataApiProfile = new DataApiOption();
builder.Configuration.GetSection("DataApi").Bind(dataApiProfile);
builder.Services.AddHttpClient("DataApi", client =>
{
    client.BaseAddress = new Uri(dataApiProfile.Url);
});

builder.Services.AddScoped<IChatService,ChatService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSingleton<ToastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
